using System;
using System.Text.Json;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Mappers
{
    public class OdooMapper : ISessionsMapper
    {
        public OdooMapper()
        {
        }

        public Session MapFoodicsNotification(FoodicsOrderNotification notification)
        {
            Console.WriteLine("MapFoodicsNotification");
            return new Session();
        }
        public Task<Session> MapLoyverseNotificationAsync(ZUSEClient serviceProvider, LoyverseNotification loyverseNotification)
        {
            throw new NotImplementedException();
        }

        public Session MapOdooNotification(OdooOrderNotification orderNotification)
        {
            var productsCollection = new List<ProductCollection>();
            foreach (var item in orderNotification.data.products_details)
            {
                productsCollection.Add(new ProductCollection
                {
                    product = new SingleProduct { name = item.full_product_name, category = new ProductCategory { name = item.category }, },
                    quantity = (int)item.qty,
                    stage = 0,
                    kitchen_notes =  item.kitchen_notes.Equals(string.Empty) ? null : item.kitchen_notes
                });
            }
            //var id_splitted = orderNotification.data.receipt_number.Split('-').Last();

            var mappedSession = new Session
            {
                id = orderNotification.data.receipt_number,
                created_at = orderNotification.data.date,
                order_number = orderNotification.data.receipt_number.Split('-').Last(),
                //order_reference = orderNotification.order.reference,
                status = 1,
                type = orderNotification.data.order_type,
                source = 1,
                delivery_status = 0,
                branch_reference = orderNotification.branch_name,
                products = JsonSerializer.Serialize(productsCollection),
                business_reference = orderNotification.company_name,
                kitchen_notes = orderNotification.data.kitchen_notes.Equals(string.Empty) ? null : orderNotification.data.kitchen_notes
            };
            return mappedSession;
        }
    }
}

