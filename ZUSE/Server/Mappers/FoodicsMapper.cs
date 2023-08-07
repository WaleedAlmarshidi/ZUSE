using System;
using System.Text.Json;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Mappers
{
    public class FoodicsMapper
    {
        public FoodicsMapper()
        {
        }
        private productMarks GetProductMarks(int status)
        {
            switch (status)
            {
                case 6:
                    return productMarks.removed;
            }
            return productMarks.normal;
        }
        public Session MapFoodicsNotification(FoodicsOrderNotification orderNotification)
        {
            var comparer = new CollectionsComparer();
            var groupes = orderNotification.order.products?.GroupBy(pc => pc, comparer);

            var products = new List<ProductCollection>();
            if (groupes is not null)
                foreach (var item in groupes)
                {
                    var collection = item.First();
                    products.Add(
                        new ProductCollection
                        {
                            product = new SingleProduct
                            {
                                name = collection.product.name,
                                name_localized = collection.product.name_localized,
                                category = collection.product.category
                            },
                            stage = collection.stage,
                            options = collection.options,
                            kitchen_notes = item.Key.kitchen_notes,
                            quantity = item.Sum(i => i.quantity)
                        }
                    );
                }
            var orderNumber = orderNotification.order.number.ToString();
            var userSession = new Session
            {
                id = orderNotification.order.id,
                created_at = orderNotification.order.created_at,
                order_number = orderNumber,
                order_reference = orderNotification.order.reference?.ToString(),
                event_type = orderNotification.@event.EndsWith("created") ? eventType.orderCreated : eventType.orderUpdated,
                table_name = orderNotification.order.table?.name,
                status = orderNotification.order.status,
                type = orderNotification.order.type,
                source = orderNotification.order.source,
                delivery_status = orderNotification.order.delivery_status.GetValueOrDefault(),
                branch_reference = orderNotification.order.branch.id,
                products = JsonSerializer.Serialize(products),
                combos = JsonSerializer.Serialize(orderNotification.order.combos),
                business_reference = orderNotification.business.reference.ToString()
            };

            if (orderNotification.order.kitchen_notes is not null)
                userSession.kitchen_notes = orderNotification.order.kitchen_notes.ToString();

            if (orderNotification.order.source == 2 && !string.IsNullOrEmpty(orderNotification.order.meta?.external_number))
            {
                var externalNumberComplete = orderNotification.order.meta.external_number;
                //var externalNumberSplitted = externalNumberComplete.Split(' ');
                //string externalNumber = externalNumberSplitted.LastOrDefault();

                if (externalNumberComplete.StartsWith("APP"))
                {
                    userSession.source = 1;
                }
                else
                {
                    userSession.order_reference = externalNumberComplete.Replace(" - ", " ");
                }
            }
            else if (orderNotification.order.source == 1)
            {
                if(userSession.kitchen_notes is not null)
                {
                    var splittedKitchenNotes = userSession.kitchen_notes?.Split(' ');

                    if(splittedKitchenNotes?.Length > 0)
                    {
                        var externalNumber = splittedKitchenNotes.Last();
                        var deliveryAppName = splittedKitchenNotes.First();
                        if(!string.IsNullOrEmpty( deliveryAppName ))
                            if (isADeliveryAppPaymentMethod(deliveryAppName, "hun") || isADeliveryAppPaymentMethod(deliveryAppName, "هنق"))
                            {
                                if (int.TryParse(externalNumber, out _))
                                    userSession.order_reference = $"hungerstation {externalNumber}";
                                else
                                    userSession.order_reference = $"hungerstation {userSession.order_number}";
                                userSession.source = 2;
                            }
                            else if (isADeliveryAppPaymentMethod(deliveryAppName, "che") || isADeliveryAppPaymentMethod(deliveryAppName, "شفز"))
                            {
                                if (int.TryParse(externalNumber, out _))
                                    userSession.order_reference = $"thechefz {externalNumber}";
                                else
                                    userSession.order_reference = $"thechefz {userSession.order_number}";
                                userSession.source = 2;
                            }
                            else if (isADeliveryAppPaymentMethod(deliveryAppName, "mrs") || isADeliveryAppPaymentMethod(deliveryAppName, "مرس"))
                            {
                                if (int.TryParse(externalNumber, out _))
                                    userSession.order_reference = $"mrsool {externalNumber}";
                                else
                                    userSession.order_reference = $"mrsool {userSession.order_number}";
                                userSession.source = 2;
                            }
                            else if (isADeliveryAppPaymentMethod(deliveryAppName, "jah") || isADeliveryAppPaymentMethod(deliveryAppName, "جاهز"))
                            {
                                if (int.TryParse(externalNumber, out _))
                                    userSession.order_reference = $"Jahez {externalNumber}";
                                else
                                    userSession.order_reference = $"Jahez {userSession.order_number}";
                                userSession.source = 2;
                            }
                    }
                    else if (splittedKitchenNotes?.Length == 0)
                        userSession.source = 1;

                }
                
            }
            //else if (orderNotification.order.source == 1)
            //{
            //    var paymentMethods = orderNotification.order.payments;
            //    foreach (var paymentMethod in paymentMethods)
            //    {
            //        if (paymentMethod.payment_method.type == PaymentType.Card || paymentMethod.payment_method.type == PaymentType.Cash)
            //            continue;

            //        var paymentMethodName = paymentMethod.payment_method.name;
            //        var splittedKitchenNotes = userSession.kitchen_notes?.Split(' ');

            //        if (splittedKitchenNotes is not null && splittedKitchenNotes.Any())
            //        {
            //            var externalNumber = splittedKitchenNotes.First();
            //            userSession.source = 2;
            //            if (isADeliveryAppPaymentMethod(paymentMethodName, "hung") || isADeliveryAppPaymentMethod(paymentMethodName, "هنق"))
            //            {

            //                if (int.TryParse(externalNumber, out _))
            //                    userSession.order_reference = $"hungerstation {splittedKitchenNotes.First()}";
            //                else
            //                    userSession.order_reference = $"hungerstation {userSession.order_number}";
            //                break;
            //                //    userSession.order_reference = $"hungerstation {splittedKitchenNotes.Last()}";
            //                //else
            //                //userSession.order_reference = $"hungerstation {orderNumber}";
            //            }
            //            else if (isADeliveryAppPaymentMethod(paymentMethodName, "chef") || isADeliveryAppPaymentMethod(paymentMethodName, "شفز"))
            //            {
            //                if (int.TryParse(externalNumber, out _))
            //                    userSession.order_reference = $"thechefz {splittedKitchenNotes.First()}";
            //                else
            //                    userSession.order_reference = $"thechefz {userSession.order_number}";
            //                break;
            //                //else
            //                //    userSession.order_reference = $"thechefz {orderNumber}";
            //            }
            //            else if (isADeliveryAppPaymentMethod(paymentMethodName, "mrs") || isADeliveryAppPaymentMethod(paymentMethodName, "مرس"))
            //            {
            //                //if(int.TryParse(splittedKitchenNotes.Last(), out _))
            //                if (int.TryParse(externalNumber, out _))
            //                    userSession.order_reference = $"mrsool {splittedKitchenNotes.First()}";
            //                else
            //                    userSession.order_reference = $"mrsool {userSession.order_number}";
            //                break;
            //                //else
            //                //    userSession.order_reference = $"thechefz {orderNumber}";
            //            }
            //            else if (isADeliveryAppPaymentMethod(paymentMethodName, "jah") || isADeliveryAppPaymentMethod(paymentMethodName, "جاهز"))
            //            {
            //                //if(int.TryParse(splittedKitchenNotes.Last(), out _))
            //                if (int.TryParse(externalNumber, out _))
            //                    userSession.order_reference = $"Jahez {splittedKitchenNotes.First()}";
            //                else
            //                    userSession.order_reference = $"Jahez {userSession.order_number}";
            //                break;
            //                //else
            //                //    userSession.order_reference = $"thechefz {orderNumber}";
            //            }
            //        }
            //        else if (splittedKitchenNotes?.Length == 0)
            //            userSession.source = 1;
            //    }
            //}

            if (orderNotification.order.customer is not null)
                    userSession.customer = JsonSerializer.Serialize(orderNotification.order.customer);

                return userSession;
        }
        private bool isADeliveryAppPaymentMethod(string paymentMethodName, string deliveryAppName)
        {
            return paymentMethodName.Contains(deliveryAppName, StringComparison.InvariantCultureIgnoreCase);
        }
        public Task<Session> MapLoyverseNotificationAsync(ZUSEClient serviceProvider, LoyverseNotification loyverseNotification)
        {
            throw new NotImplementedException();
        }

        public Session MapOdooNotification(OdooOrderNotification orderNotification)
        {
            Console.WriteLine("MapFoodicsNotification");
            return new Session();
        }

        public async Task<Session> MapSqaureNotification(SqaureNotification sqaureNotification)
        {
            throw new NotImplementedException();
        }

        public Task<Session> MapSqaureNotificationAsync(SqaureNotification sqaureNotification)
        {
            throw new NotImplementedException();
        }
    }
}

