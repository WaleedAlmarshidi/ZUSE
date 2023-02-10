using System;
using System.Text.Json;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Mappers
{
    public class FoodicsMapper : ISessionsMapper
    {
        public FoodicsMapper()
        {
        }
        public Session MapFoodicsNotification(FoodicsOrderNotification orderNotification)
        {
            var userSession = new Session
            {
                id = orderNotification.order.id,
                created_at = orderNotification.order.created_at,
                order_number = orderNotification.order.number.ToString(),
                //order_reference = orderNotification.order.reference,
                event_type = orderNotification.@event.EndsWith("created") ? eventType.orderCreated : eventType.orderUpdated,
                table_name = orderNotification.order.table?.name,
                status = orderNotification.order.status,
                type = orderNotification.order.type,
                source = orderNotification.order.source,
                delivery_status = orderNotification.order.delivery_status.GetValueOrDefault(),
                branch_reference = orderNotification.order.branch.id,
                products = JsonSerializer.Serialize(orderNotification.order.products),
                combos = JsonSerializer.Serialize(orderNotification.order.combos),
                business_reference = orderNotification.business.reference.ToString()
            };
            //NotificationData.kitchen_notes = NotificationData.order.kitchen_notes.ToString();
            if (orderNotification.order.kitchen_notes is not null)
                userSession.kitchen_notes = orderNotification.order.kitchen_notes.ToString();

            if (orderNotification.order.customer is not null)
                userSession.customer = JsonSerializer.Serialize(orderNotification.order.customer);

            return userSession;
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
    }
}

