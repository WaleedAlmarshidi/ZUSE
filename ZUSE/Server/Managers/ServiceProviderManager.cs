using System;
using System.Text.Json;
using System.Net;
using ZUSE.Shared.Models;
using ZUSE.Server.Data;
using ZUSE.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZUSE.Server.Models;
//using static Google.Rpc.Context.AttributeContext.Types;

namespace ZUSE.Server.Managers
{
    public class ServiceProviderManager
    {
        public string topic;
        //public TathkaraServiceProvider instance { get; set; }

        public readonly int calledOrdersLimit = 10;
        private readonly Queue<CalledOrder> calledOrders;

        private readonly List<UserInitiatedSession> userInitiatedSessions;
        private readonly int userInitiatedOrdersCapacity = 20;

        public ServiceProviderManager()
        {
            //topic = serviceProvider.topic_id;
            //instance = serviceProvider;

            calledOrders = new();
            userInitiatedSessions = new();

        }
        public void PushUserInitiatedSession(UserInitiatedSession session)
        {
            var existingSession = GetAssosiatedUserInitiatedSession(session);
            if (existingSession is null)
            {
                Console.WriteLine("existingSession not found ---------- ");
                if (userInitiatedSessions.Count >= userInitiatedOrdersCapacity)
                    userInitiatedSessions.RemoveAt(0);
                userInitiatedSessions.Add(session);
                return;
            }
            if (session.phone is not null)
                existingSession.phone = session.phone;
            if (session.push_notification_subsicribtion is not null)
                existingSession.push_notification_subsicribtion = session.push_notification_subsicribtion;
        }

        public UserInitiatedSession GetAssosiatedUserInitiatedSession(UserInitiatedSession userSession)
        {
            return userInitiatedSessions.LastOrDefault(
                    session => session.browser_id.Equals(userSession.browser_id)
                );
        }


        public async Task PushClosedSession(Session userSession, ZUSEClient serviceProvider)
        {
            try
            {
                var topic = serviceProvider.topic;
                var order_reference = userSession.order_number;
                var delivery_status = userSession.delivery_status;


                if (delivery_status != 2)
                    return;
                //if (assosiatedSession is null)
                //    return;
                if (serviceProvider.is_mobile_notifier_provider)
                {
                    Customer customer = null;
                    if (userSession.customer is not null)
                        customer = JsonSerializer.Deserialize<Customer>(userSession.customer);


                    if (calledOrders.Where(order => order.order_reference.Equals(order_reference)).SingleOrDefault() is null)
                        calledOrders.Enqueue(new CalledOrder
                        {
                            order_reference = order_reference,
                            closed_at = userSession.closed_at.GetValueOrDefault(),
                            browser_id = userSession.browser_id
                        });

                    if (calledOrders.Count == calledOrdersLimit - 1)
                    {
                        calledOrders.Dequeue();
                    }

                    await UserCommunicationPipe.Publish(
                                serviceProvider.topic + "/users",
                                msg: JsonSerializer.Serialize(calledOrders),
                                retain: true
                            );
                    if (customer is not null)
                        if(!customer.isCalled.GetValueOrDefault())
                            if (customer.phone is not null)
                            {
                                //customer.isCalled = true;
                                await SendWhatsAppNotification(serviceProvider, customer.phone, userSession.order_number);
                                //await SendSmsAsync(customer.phone,
                                //    userSession.order_number,
                                //    serviceProvider.name,
                                //    serviceProvider.external_notification_special_msg);
                            }
                    DeleteAssosiatedUserInitiatedSession(userSession);
                }

                await PutOrderDeliveryStatus(userSession.id, delivery_status);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in PushClosedSession: \n" + e.Message + "\nstack trace: " + e.StackTrace);
            }
        }

        private void DeleteAssosiatedUserInitiatedSession(Session userSession)
        {
            var existingSession = GetAssosiatedUserInitiatedSession(userSession);
            userInitiatedSessions.Remove(existingSession);
        }

        private UserInitiatedSession GetAssosiatedUserInitiatedSession(Session userSession)
        {
            return userInitiatedSessions.LastOrDefault(
                    session => session.browser_id.Equals(userSession.browser_id)
                );
        }

        private async Task SendWhatsAppNotification(ZUSEClient serviceProvider, string phone, string orderNumber)
        {
            var uri = new Uri("https://apis.unifonic.com/v1/messages");

            using var httpClient = new HttpClient();
            {
                var body = JsonSerializer.Serialize(
                        new Whatsapp_body
                        {
                            recipient = new recipient { contact = $"+{phone}", channel = "whatsapp" },
                            content = new content
                            {
                                type = "template",
                                name = "order_is_ready",
                                language = new language { code = "ar"},
                                components = new List<Component>
                                {
                                    new Component
                                    {
                                        type = "body",
                                        parameters = new List<parameter>
                                        {
                                            new parameter
                                            {
                                                type = "text",
                                                text = orderNumber
                                            },
                                            new parameter
                                            {
                                                type = "text",
                                                text = serviceProvider.name
                                            },
                                            new parameter
                                            {
                                                type = "text",
                                                text = string.IsNullOrEmpty( serviceProvider.external_notification_special_msg) ? " " : serviceProvider.external_notification_special_msg
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    );
                Console.WriteLine("\n\n\nbody:\n" + body + "\n\n\n");
                var requestMessage = new HttpRequestMessage
                {
                    Content = new StringContent(body),
                    RequestUri = uri,
                    Method = HttpMethod.Post,
                    Headers = {
                        { "PublicId", "98dcfe56-9761-4521-b955-2faf00413089"},
                        {"Secret", "MlnYrunworGMiIkwb44u4z7e0X20c75lEz7p2PPXcgq5Wb3ziURf43TvuV399V6TZvatpv9CZGl" }
                    }
                };
                using var response = await httpClient.SendAsync(requestMessage);
                {
                    string responseHeaders = response.Headers.ToString();
                    string responseData = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("Status " + (int)response.StatusCode);
                }
            }
        }

        private async Task SendSmsAsync(string? phone, string order_reference,
            string business_name, string? external_notification_special_msg)
        {
            //Common testing requirement. If you are consuming an API in a sandbox/test region
            //uncomment this line of code ONLY for non production uses.
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //Be sure to run "Install-Package Microsoft.Net.Http" from your nuget command line.
            Console.WriteLine("SendSmsAsync --------");
            var baseAddress = new Uri("https://www.msegat.com");

            using var httpClient = new HttpClient { BaseAddress = baseAddress };
            {
                var smsContent = JsonSerializer.Serialize(
                        new
                        {
                            userSender = "Tathkara",
                            userName = "Tathkara",
                            numbers = phone,
                            apiKey = "a03b19d2b04fb50860dd5583393ec9f0",
                            msg = $"طبلك رقم {order_reference} لدى {business_name} جاهز للإستلام، تفضل الى مقدم الخدمة {external_notification_special_msg}",
                            msgEncoding = "UTF8"
                        }
                    );
                using (var content = new StringContent(
                    smsContent
                    , System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("/gw/sendsms.php", content))
                    {
                        string responseHeaders = response.Headers.ToString();
                        string responseData = await response.Content.ReadAsStringAsync();

                        Console.WriteLine("Status " + (int)response.StatusCode);
                    }
                }
            }
        }

        private async Task PutOrderDeliveryStatus(string id, int delivery_status)
        {
            string content = JsonSerializer.Serialize(
                    new
                    {
                        delivery_status = delivery_status
                    }
                );
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://api-sandbox.foodics.com/v5/orders/" + id),
                Headers = {
            { HttpRequestHeader.Authorization.ToString(), "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjM1ZGU5NmJkMWZlNTZjZjAwMWY4NGYxOTBiYzc0NGRlOWE0OWM1MWMxYTMyYjZmOGYzODNlM2QxN2QwZGZhZmM2Zjc5MzlhYjZkMDUyODMzIn0.eyJhdWQiOiI4ZjllYjNmNi02ZWZhLTRmZWYtODk1ZS1kMWJjNDRiYTQ4MWQiLCJqdGkiOiIzNWRlOTZiZDFmZTU2Y2YwMDFmODRmMTkwYmM3NDRkZTlhNDljNTFjMWEzMmI2ZjhmMzgzZTNkMTdkMGRmYWZjNmY3OTM5YWI2ZDA1MjgzMyIsImlhdCI6MTY1MzIwMDI4NiwibmJmIjoxNjUzMjAwMjg2LCJleHAiOjE4MTA5NjY2ODYsInN1YiI6Ijk2NWI5MTNmLTkxNWEtNDQ0Ni1iNzFiLTc4YjBiZTE2MjliNyIsInNjb3BlcyI6WyJnZW5lcmFsLnJlYWQiLCJvcmRlcnMubGlzdCJdLCJidXNpbmVzcyI6Ijk2NWI5MTNmLWE2NmQtNDYxMS1hZjE0LTFmMWYxNDE0YjQ4ZCIsInJlZmVyZW5jZSI6IjUwMjg1MSJ9.pj6HtlnWEqudVCsWIHIevHCXNsd3-lIroM2RE2siFJzyan_Cm1W5Zns803n0KNRMCcGkHPThF5nWelYfUbYX97-Cov2C4KFwnjMrj3xXBh-BOH8qQH3nOcqxHXaXbrfSgJuW3IiQj18JG0lnQL6xJ5qD9lJgrxM-KAFiu4igJ5Nsbr1xmNvq-p9dWfYlDEFKh8cEkyXtx5foF3k-pOV07mBVpmQE9jvizHGUvN-8jWtSyelBCenFSn6xzAVlv0VB6Fxson6t576HMm9XHe0a3LMslGqsjzDHr_n1VsP2lvI0qZSXsffoRQBlvokviZIvfL4vefNvZ-0hQVD1sK6PKWtalS5rRMgsaCL31xgVM57_q9Qo5O49BpwaSg_3o5QsLt7383oLwp-AevijYEGj4b3rEt0hFL-QUxizWDkyp3-Nb46uD1zqnqyJ496F1fiI3UiuTIMJuJHrmmfn5XL8GCXGwxxst2kAOv-Rz729fjLsaIkkHRlzBV-ZDCtGMWub7DmKGR-EHWZF19JmLJt5ZkjMXM3aBrTGuMLyh0912AEgZvsehwOG9XuwOrHi6Hwu8IrFMiDAjsrDoY4C5U4pb5b4SQt7JxxMx5Sdkey982diAppTPWEH79LuCHbYfS_KmmJ8koHq_BFxodY7__ttojXfSgHkPyJ81qDZvUX6GVk" },
            { HttpRequestHeader.Accept.ToString(), "application/json" },
            { HttpRequestHeader.ContentType.ToString(), "application/json" }
            },
                Content = new StringContent(content)
            };
            // todo replace token with actual one
            await client.SendAsync(httpRequestMessage).Result.Content.ReadAsStringAsync();
        }

        internal async Task PushNewOrderToKds(ZUSE_dbContext dbContext, Session newSession, ZUSEClient serviceProvider)
        {
            try
            {
                if (newSession.delivery_status == 2)
                    return;
                var dateTimeLowerBound = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(70));
                var last100UndeliveredOrders = dbContext.sessions.Where(
                            session =>
                                session.created_at > dateTimeLowerBound
                                &&
                                session.business_reference.Equals(serviceProvider.reference_id)
                                &&
                                session.branch_reference.Equals(serviceProvider.branch_id)
                                &&
                                session.delivery_status != 2
                            ).OrderByDescending(s => s.created_at).Take(100).ToList();

                last100UndeliveredOrders.RemoveAll(s => s.id.Equals(newSession.id));

                if (newSession.created_at > dateTimeLowerBound && newSession.delivery_status != 2)
                    last100UndeliveredOrders.Add(newSession);

                await UpdateKdsQueue(last100UndeliveredOrders, serviceProvider);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in PushNewOrderToKds: " + e.Message + "\nstack trace: " + e.StackTrace);
            }

        }
        internal async Task UpdateKdsQueue(List<Session> last100BusinessInitiatedOrders, ZUSEClient serviceProvider)
        {
            await UserCommunicationPipe.Publish(
                    topic: serviceProvider.topic + "/kds",
                    JsonSerializer.Serialize(
                        new SessionsCollection
                        {
                            data = last100BusinessInitiatedOrders,
                            sender_id = 0
                        }
                    )
                    , retain: true
                );
        }
        internal void AttachWithUserInitiatedSession(Session managedSession)
        {
            //Console.WriteLine("in AttachWithUserInitiatedSession");
            //foreach (var item in userInitiatedSessions)
            //{
            //    Console.WriteLine("orders not ordered by date: " + item.created_at);
            //}
            if (managedSession.browser_id is not null)
            {
                Console.WriteLine("managedSession already attached ######## to : " + managedSession.browser_id);
                var attachedUserInitiatedSession = GetAssosiatedUserInitiatedSession(managedSession);
                if (attachedUserInitiatedSession is null)
                    return;

                var customerInfo = new Customer
                {
                    phone = attachedUserInitiatedSession.phone
                };
                managedSession.customer = JsonSerializer.Serialize(customerInfo);
                return;
            }

            var lastSessionWithSameOrderReference = userInitiatedSessions.LastOrDefault(
                    sessoin =>
                        managedSession.order_number.Equals(sessoin.order_reference)
                        ||
                        managedSession.order_number.Substring(Math.Max(0, managedSession.order_number.Length - 2)).Equals(sessoin.order_reference)
                );

            if (lastSessionWithSameOrderReference is null || lastSessionWithSameOrderReference.isCalled)
                return;

            Console.WriteLine("AttachWithUserInitiatedSession found : " +
                lastSessionWithSameOrderReference.browser_id + "order reference : " + lastSessionWithSameOrderReference.order_reference + "\n\n\n");

            managedSession.browser_id = lastSessionWithSameOrderReference.browser_id;

            var customer = new Customer();

            if (managedSession.customer is null)
            {
                if (lastSessionWithSameOrderReference.phone is not null)
                {
                    customer.phone = lastSessionWithSameOrderReference.phone;
                    lastSessionWithSameOrderReference.isCalled = true;
                }
            }
            //else
            //{
            //    var existingCustomer = JsonSerializer.Deserialize<Customer>(managedSession.customer);

            //    existingCustomer.phone = lastSessionWithSameOrderReference.phone;
            //}
            managedSession.customer = JsonSerializer.Serialize(customer);
            Console.WriteLine("customer after updating : " + managedSession.customer);
        }

        public async Task UpdateTvSession(ZUSE_dbContext dbContext, Session userSession, ZUSEClient serviceProvider)
        {
            var dateTimeLowerBound = DateTime.UtcNow.Subtract(TimeSpan.FromHours(1));
            var last11UndeliveredOrders = dbContext.sessions.Where(
                        session =>
                            session.created_at > dateTimeLowerBound
                            &&
                            session.business_reference.Equals(serviceProvider.reference_id)
                            &&
                            session.branch_reference.Equals(serviceProvider.branch_id)
                        ).ToList();

            if (userSession.created_at > dateTimeLowerBound &&
                    last11UndeliveredOrders.Find(n => n.order_number.Equals(userSession.order_number)) is null)
                last11UndeliveredOrders.Add(userSession);

            await UserCommunicationPipe.Publish(
                    serviceProvider.topic + "/tv",
                    JsonSerializer.Serialize(
                         last11UndeliveredOrders
                    )
                    , retain: true
            );
        }

        internal class ServiceProviderSession
        {
            public string topic { get; set; } = null!;
            public string last_order_calling_date { get; set; }
            public Queue<CalledOrder> calledOrders { get; set; }

            public void Dequeue()
            {
                calledOrders.Dequeue();
            }
        }

        public class CalledOrder
        {
            public string order_reference { get; set; }
            public DateTime closed_at { get; set; }
            public string browser_id { get; set; }
        }

    }
}
