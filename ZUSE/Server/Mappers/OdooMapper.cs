using System;
using System.Text.Json;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Mappers
{
    public class OdooMapper
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
        public async Task<Session> MapSqaureNotification(SqaureNotification sqaureNotification)
        {
            throw new NotImplementedException();
        }
        public Session MapOdooNotification(OdooOrderNotification orderNotification)
        {
            var productsCollection = new List<ProductCollection>();
            foreach (var item in orderNotification.data.products_details)
            {
                string res = new string(item.full_product_name.SkipWhile(c => c != '(')
                           .Skip(1)
                           .TakeWhile(c => c != ')')
                           .ToArray()).Trim();
                if(res.Length != 0)
                    item.full_product_name = item.full_product_name.Substring(0, item.full_product_name.Length - res.Length - 2).Trim();
                var optionsSeperated = res?.Split(',');
                var options = new List<SingleProductOptions>();
                foreach (var option in optionsSeperated)
                {
                    if(!string.IsNullOrEmpty(option))
                        options.Add(new SingleProductOptions { modifier_option = new SingleProductModifierOption { name = option.Trim() } });
                }
                productsCollection.Add(new ProductCollection
                {
                    product = new SingleProduct { name = item.full_product_name, category = new ProductCategory { name = item.category }, },
                    quantity = (int)item.qty,
                    stage = 0,
                    options = options,
                    kitchen_notes =  string.IsNullOrEmpty(item.kitchen_notes) ? "" : item.kitchen_notes
                });
            }
            //var id_splitted = orderNotification.data.receipt_number.Split('-').Last();
            var createdAt = orderNotification.data.date; // initial value

            if (DateTime.UtcNow.Subtract(orderNotification.data.date).TotalSeconds < -3)
                createdAt = DateTime.UtcNow;

            var mappedSession = new Session
            {
                id = orderNotification.data.receipt_number,
                created_at = createdAt,
                order_number = orderNotification.data.receipt_number.Split('-').Last(),
                //order_reference = orderNotification.order.reference,
                table_name = orderNotification.data.table,
                type = orderNotification.data.order_type,
                status = 1,
                source = 1,
                delivery_status = 0,
                customer = "",
                branch_reference = orderNotification.branch_name,
                products = JsonSerializer.Serialize(productsCollection),
                business_reference = orderNotification.company_name,
                kitchen_notes = orderNotification.data.kitchen_notes.Equals(string.Empty) ? null : orderNotification.data.kitchen_notes
            };
            if (!string.IsNullOrEmpty(orderNotification.data.customer))
                mappedSession.customer = JsonSerializer.Serialize(new Customer { name = orderNotification.data.customer });
            return mappedSession;
        }

        public Task<Session> MapSqaureNotificationAsync(SqaureNotification sqaureNotification)
        {
            throw new NotImplementedException();
        }
    }
}

