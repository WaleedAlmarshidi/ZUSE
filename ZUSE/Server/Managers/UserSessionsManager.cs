﻿using System;
using ZUSE.Server.Managers;
using System.Text.Json;
using ZUSE.Shared.Models;
using ZUSE.Server.Data;
using ZUSE.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace ZUSE.Server.Managers
{
    public class UserSessionsManager : IUsersManager
    {
        private readonly static Dictionary<string, ServiceProviderManager> listenedServiceProviders = new();
        private readonly ZUSE_dbContext dbContext;

        public async Task ListenToNewServiceProvider(string topic)
        {
            await UserCommunicationPipe.Subsicribe(topic);
        }
        private bool IsOrderCompletedStatus(int status, int delivery_status)
        {
            return delivery_status == 2 || status == 4;
        }
        public UserSessionsManager(ZUSE_dbContext dbContext)
        {
            this.dbContext = dbContext;

            //foreach (var serviceProvider in dbContext.TathkaraServiceProvider)
            //{
            //    ListenToNewServiceProvider(serviceProvider.topic);
            //}

            UserCommunicationPipe.AddMsgListener(this.CommunicationMessageListener);
            //listenedServiceProviders.Add(new());
        }
        private ZUSEClient? GetServiceProvider(ZUSE_dbContext dbContext, string business_reference, string branch_reference)
        {
            if (business_reference is null || branch_reference is null)
                return null;

            return dbContext.ZUSEClients.Where(
                    sp => sp.branch_id.Equals(branch_reference) &&
                        sp.reference_id.Equals(business_reference)
                ).SingleOrDefault();
        }

        public async Task<IResult> PostOrUpdateSession(Session session)
        {
            var products = JsonSerializer.Deserialize<List<ProductCollection>>(session.products);

            var serviceProvider = GetServiceProvider(dbContext, session.business_reference, session.branch_reference);
            if (serviceProvider is null) // service provider not registerd
            {
                Console.WriteLine("serviceProvider not found");
                return Results.Problem("service provider not found");
            }

            try
            {
                var existingSession = dbContext.sessions.Find(session.id, session.business_reference);
                if (existingSession is null)
                {
                    AddControlOMSFunctionalityIfAsked(serviceProvider, session);
                    
                    await AddWebHookInitiatedSession
                            (
                                dbContext,
                                session,
                                serviceProvider
                            );
                    await dbContext.sessions.AddAsync(session);
                }
                else
                {
                    existingSession.products = session.products;
                    await AddWebHookInitiatedSession
                            (
                                dbContext,
                                existingSession,
                                serviceProvider
                            );
                    AddControlOMSFunctionalityIfAsked(serviceProvider, session);
                }
                await dbContext.SaveChangesAsync();
            }
            catch (InvalidOperationException e)
            {
                return Results.Ok("session already exists, updated but not added");
            }
            //await dbContext.SaveChangesAsync();
            return Results.Ok();
        }
        private List<ProductCollection> SerializeProductsToProductsCollection(Session session)
        {
            var productCollections = JsonSerializer.Deserialize<List<ProductCollection>>(session.products);
            return productCollections;
        }
        private string MarkUpdatedCollectionsAsUpdated(Session newSession, Session existingSession)
        {
            var nspCollections = SerializeProductsToProductsCollection(newSession);
            var espCollections = SerializeProductsToProductsCollection(existingSession);

            if (nspCollections is null || espCollections is null)
                return string.Empty;

            espCollections.ForEach(
                    esp =>
                    {
                        var espName = esp.product.name;
                        if (esp.stage != productMarks.removed)
                        {
                            var newSessionProduct = nspCollections.Any(
                                nsp => nsp.product.name.Equals(espName) &&
                                    esp.quantity == nsp.quantity
                            );
                            if (!newSessionProduct)
                            {
                                esp.stage = productMarks.removed;
                            }
                        }
                    }
                );
            // checking every new product
            nspCollections?.ForEach(
                    nsp =>
                    {
                        var nspName = nsp.product.name;
                        var existingSessionProduct = espCollections.Any(
                                esp => esp.product.name.Equals(nspName) &&
                                    esp.quantity == nsp.quantity
                            );

                        if (!existingSessionProduct)
                        {
                            nsp.stage = productMarks.added;
                            espCollections.Insert(0, nsp);
                        }
                        //else
                        //{
                        //    if (CountSessionProducts(existingSession) < CountSessionProducts(newSession))
                        //    {
                        //        nsp.stage = productMarks.added;
                        //        espCollections.Insert(0, nsp);
                        //    }
                        //}
                    }
                );

            return JsonSerializer.Serialize(espCollections);
            //if(CountSessionProducts(existingSession) <= CountSessionProducts(newSession))
        }
        public async Task<IResult> EditClosedOrder(Session session)
        {
            var serviceProvider = GetServiceProvider(dbContext, session.business_reference, session.branch_reference);
            if (serviceProvider is null) // service provider not registerd
            {
                Console.WriteLine("serviceProvider not found");
                return Results.Problem("service provider not found");
            }

            try
            {
                var comparer = new CollectionsComparer();
                var existingSession = dbContext.sessions.Find(session.id, session.business_reference);
                if (existingSession is null)
                {
                    AddControlOMSFunctionalityIfAsked(serviceProvider, session);
                    await AddWebHookInitiatedSession
                            (
                                dbContext,
                                session,
                                serviceProvider
                            );
                    await dbContext.sessions.AddAsync(session);
                }
                else
                {
                        var nspCollections = SerializeProductsToProductsCollection(session);
                        var espCollections = SerializeProductsToProductsCollection(existingSession);
                        if(nspCollections.All(i => i.stage == productMarks.removed))
                        {
                            var removedCollections = new List<ProductCollection>();
                            espCollections.ForEach(esp =>
                            {
                                var numOfDeletedCollections = nspCollections.Where(i => i.Equals(esp)).Sum(i => i.quantity);
                                esp.quantity -= numOfDeletedCollections;
                                if (esp.quantity <= 0)
                                    removedCollections.Add(esp);
                            });
                            foreach (var copy in removedCollections)
                            {
                                espCollections.RemoveAll(i => i.Equals(copy));
                            }
                            //var numOfDeletedCollections = nspCollections.Where(i => i.Equals(nsp) && i.stage == productMarks.removed).Sum(i => i.quantity);
                            //espCollections.RemoveAll(
                            //        e =>
                            //            nspCollections.Contains(e) && e.quantity == 1
                            //    );
                            
                        }
                        else
                        {
                                //espCollections.RemoveAll(
                                //        e =>
                                //            nspCollections.Contains(e)

                                //    );
                                espCollections.AddRange(
                                        nspCollections.Where(n => !espCollections.Contains(n, comparer = new CollectionsComparer()))
                                    );
                        }
                            //if(nspCollections.All(i => i.stage == productMarks.normal))
                            //    espCollections.
                        //nspCollections.ForEach(
                        //        nsp =>
                        //        {
                                    //var numOfDeletedCollections = nspCollections.Where(i => i.Equals(nsp) && i.stage == productMarks.removed).Sum(i => i.quantity);

                                    //if(existingCollection.)
                                    //existingCollection.quantity -= numOfDeletedCollections;
                                    //if (existingCollection.quantity < 0)
                                    //    espCollections.Remove(existingCollection);

                            //    }
                            //);
                        existingSession.products = JsonSerializer.Serialize(espCollections);
                        AddControlOMSFunctionalityIfAsked(serviceProvider, existingSession, espCollections);
                        await AddWebHookInitiatedSession
                            (
                                dbContext,
                                existingSession,
                                serviceProvider
                            );
                }
                //else
                //{
                //    //existingSession.products = session.products;
                //    var nspCollections = SerializeProductsToProductsCollection(session);
                //    var espCollections = SerializeProductsToProductsCollection(existingSession);
                //    nspCollections.ForEach(
                //            y =>
                //            {
                //                var esp = espCollections.Where(
                //                        x =>
                //                            x.product.name.Equals(y?.product.name) && string.Equals(x.kitchen_notes, y.kitchen_notes)
                //                            &&
                //                            (x.options?.All(xOp => (y.options?.Any(yOp => (yOp?.modifier_option?.name?.Equals(xOp?.modifier_option?.name)).GetValueOrDefault())).GetValueOrDefault())).GetValueOrDefault()

                //                    ).SingleOrDefault();
                //                if(esp is not null)
                //                {
                //                    esp.quantity -= y.quantity;
                //                    if (esp?.quantity < 1)
                //                    {
                //                        //esp.stage = productMarks.removed;
                //                        espCollections.Remove(esp);
                //                    }
                //                }
                //            }
                //        );
                //    existingSession.products = JsonSerializer.Serialize(espCollections);
                //    AddControlOMSFunctionalityIfAsked(serviceProvider, existingSession, espCollections);
                //    await AddWebHookInitiatedSession
                //            (
                //                dbContext,
                //                existingSession,
                //                serviceProvider
                //            );
                //}
                await dbContext.SaveChangesAsync();
            }
            catch (InvalidOperationException e)
            {
                return Results.Ok("session already exists, updated but not added");
            }
            //await dbContext.SaveChangesAsync();
            return Results.Ok(session.order_reference);
        }
        public async Task<IResult> PostNewSession(Session userSession)
        {
            var serviceProvider = GetServiceProvider(dbContext, userSession.business_reference, userSession.branch_reference);
            if (serviceProvider is null) // service provider not registerd
            {
                Console.WriteLine("serviceProvider not found");
                return Results.Ok("service provider not found");
            }

            try
            {
                await dbContext.sessions.AddAsync(userSession);
                AddControlOMSFunctionalityIfAsked(serviceProvider, userSession);
                await dbContext.SaveChangesAsync();
                await AddWebHookInitiatedSession
                            (
                                dbContext,
                                userSession,
                                serviceProvider
                          );
            }
            catch(InvalidOperationException e)
            {
                return Results.Ok("session already exists");
            }
            return Results.Ok(userSession.order_reference);
        }
        public async Task<Session> GetExistingSession(Session session)
        {
            return await dbContext.sessions.FindAsync(session.id, session.business_reference);
        }
        public void ChangeOrderStatus(Session session, int status)
        {
            session.delivery_status = status;
        }
        public ZUSEClient GetTathkaraServiceProviderWithTopic(string topic)
        {
            return dbContext.ZUSEClients.Where(
                    ServiceProvider => ServiceProvider.topic.Equals(topic)
                ).SingleOrDefault();
        }
        private void AddControlOMSFunctionalityIfAsked(ZUSEClient serviceProvider, Session session, List<ProductCollection>? passedProducts = null)
        {
            if (serviceProvider.is_kds_order_completion_approval_needed)
            {
                var products = passedProducts is null ? JsonSerializer.Deserialize<List<ProductCollection>>(session.products) : passedProducts;
                var assemblyPointApprovalKey = "_approval";
                if (products?.Find(product => product.product.category.name.Equals(assemblyPointApprovalKey)) is null)
                    products?.Add(new ProductCollection()
                    {
                        product = new SingleProduct { name = "assembly point approval", category = new ProductCategory { name = assemblyPointApprovalKey } }
                    });
                session.products = JsonSerializer.Serialize(products);
            }
        }
        public List<Session> GetSessionsWithinTime(DateTime start, DateTime end, string topic)
        {
            var businessSessions = dbContext.sessions.Where(
                session =>
                        session.business_reference.Equals(topic)
                );
            return businessSessions.Where(
                    session =>
                        session.created_at >= start && session.created_at <= end
                        && session.delivery_status == null
                ).ToList();
        }
        private ServiceProviderManager FindAssosiatedManager(string topic)
        {
            var assosiatedServiceProviderManager = listenedServiceProviders.GetValueOrDefault(topic);
            if (assosiatedServiceProviderManager is null)
            {
                listenedServiceProviders.Add(
                        topic,
                        new()
                    );
                assosiatedServiceProviderManager = listenedServiceProviders.GetValueOrDefault(topic);
                assosiatedServiceProviderManager.topic = topic;  
            }
            return assosiatedServiceProviderManager;

        }
        public IResult AddNewServiceProvider(ZUSEClient serviceProvider)
        {
            try
            {
                dbContext.ZUSEClients.Add(serviceProvider);
                dbContext.SaveChanges();
                FindAssosiatedManager(serviceProvider.topic);
                ListenToNewServiceProvider(serviceProvider.topic);
            }
            catch (Exception e)
            {
                return Results.Ok("service provider already exists");
            }
            return Results.Accepted($"Client added succesfully ");

        }
        public async Task AddWebHookInitiatedSession(ZUSE_dbContext dbContext, Session managedSession, ZUSEClient serviceProvider)
        {
            //Console.WriteLine("in AddWebHookInitiatedSession ");
            var assosiatedServiceProviderManager = FindAssosiatedManager(serviceProvider.topic);
            //if (assosiatedServiceProviderManager is null)
            //{
            //    assosiatedServiceProviderManager = listenedServiceProviders.GetValueOrDefault(serviceProvider.topic);
            //}

            Console.WriteLine("AddWebHookInitiatedSession: ");

            if (managedSession.delivery_status == 2)
            {
                if(serviceProvider.is_mobile_notifier_provider)
                    assosiatedServiceProviderManager.AttachWithUserInitiatedSession(managedSession);
                await assosiatedServiceProviderManager.PushClosedSession(managedSession, serviceProvider);
                return;
            }

            if (serviceProvider.is_tv_provider)
                await assosiatedServiceProviderManager.UpdateTvSession(dbContext, managedSession, serviceProvider);

            if (serviceProvider.is_kds_provider)
                await assosiatedServiceProviderManager.PushNewOrderToKds(dbContext, managedSession, serviceProvider);

            if (serviceProvider.is_mobile_notifier_provider)
                assosiatedServiceProviderManager.AttachWithUserInitiatedSession(managedSession);
        }


        public async Task<IResult> PushClosedSession(Session session)
        {
            var existingSession = await GetExistingSession(session);

            if (existingSession is null)
            {
                Console.WriteLine("session with id : " + session.id + " not found in PutOrderDeliveryStatus controller");
                return Results.Problem("session not found");
            }
            existingSession.delivery_status = 2;
            existingSession.products = session.products;
            existingSession.closed_at = session.closed_at.GetValueOrDefault();
            var serviceProvider = GetServiceProvider(dbContext, existingSession.business_reference, existingSession.branch_reference);

            if (serviceProvider is null)
            {
                Console.WriteLine("service provider not found");
                return Results.Problem("service provider not found");
            }

            var assosiatedServiceProviderManager = FindAssosiatedManager(serviceProvider.topic);


            if (serviceProvider.is_mobile_notifier_provider)
                assosiatedServiceProviderManager.AttachWithUserInitiatedSession(existingSession);

            if(serviceProvider.is_tv_provider)
                await assosiatedServiceProviderManager.UpdateTvSession(dbContext, existingSession, serviceProvider);

            await assosiatedServiceProviderManager.PushClosedSession(existingSession, serviceProvider);
            //assosiatedServiceProviderManager.UpdateKdsQueue(serviceProvider);
            await dbContext.SaveChangesAsync();
            return Results.Ok();
        }

        public void PostNewUserInitiatedSession(UserInitiatedSession userInitiatedSession)
        {

            var assosiatedServiceProviderManager = FindAssosiatedManager(userInitiatedSession.topic);
            if (assosiatedServiceProviderManager is null)
            {
                return;
            }
            Console.WriteLine("\n\n\n\nphone: " + userInitiatedSession.phone + "\n\n\n");
            assosiatedServiceProviderManager.PushUserInitiatedSession(userInitiatedSession);
        }

        private void CommunicationMessageListener(string topic, string message)
        {

            var assosiatedServiceProviderManager = FindAssosiatedManager(topic);
            if (assosiatedServiceProviderManager is null)
            {
                return;
            }

            // todo Add push id
            var initiatedSession = JsonSerializer.Deserialize<UserInitiatedSession>(message);

            assosiatedServiceProviderManager.PushUserInitiatedSession(initiatedSession);
        }

    }
}

