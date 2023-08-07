using System;
using System.Net;
using System.Text.Json;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Mappers
{
	public class LoyverseMapper
    {
		public LoyverseMapper()
		{
		}

        public Session MapFoodicsNotification(FoodicsOrderNotification notification)
        {
            throw new NotImplementedException();
        }

        public async Task<Session> MapLoyverseNotificationAsync(ZUSEClient serviceProvider, LoyverseNotification loyverseNotification)
        {
            var httpClient = new HttpClient();
            var productsCollection = new List<ProductCollection>();
            HttpResponseMessage response;
            loyverseItem loyverseItem;
            loyverseCategory loyverseCategory;
            var receipt = loyverseNotification.receipts[0];
            HttpRequestMessage itemRequestMessage, categoryRequestMessage;
            foreach (var item in receipt.line_items)
            {
                itemRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://api.loyverse.com/v1.0/items/" + item.item_id),
                    Method = HttpMethod.Get,
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {serviceProvider.access_token}" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };
                response = await httpClient.SendAsync(itemRequestMessage);
                loyverseItem = await response.Content.ReadFromJsonAsync<loyverseItem>();

                if (loyverseItem is null)
                    return null;

                categoryRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://api.loyverse.com/v1.0/categories/{loyverseItem.category_id}"),
                    Method = HttpMethod.Get,
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {serviceProvider.access_token}" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };
                response = await httpClient.SendAsync(categoryRequestMessage);

                loyverseCategory = await response.Content.ReadFromJsonAsync<loyverseCategory>();

                var categoryName = loyverseCategory.name is null ? "" : loyverseCategory.name;

                productsCollection.Add(new ProductCollection
                {
                    product = new SingleProduct { name = item.item_name, category = new ProductCategory { name = categoryName }, },
                    quantity = (int)item.quantity,
                    stage = 0,
                    kitchen_notes = item.line_note
                });
            }
            var ZUSESession = new Session
            {
                business_reference = loyverseNotification.merchant_id,
                id = receipt.receipt_number,
                branch_reference = receipt.store_id,
                order_number = receipt.receipt_number.Split('-')[1],
                kitchen_notes = receipt.note,
                created_at = loyverseNotification.created_at.ToUniversalTime(),
                products = JsonSerializer.Serialize(productsCollection),
                type = 2,
                source = 3,
                delivery_status = 0
            };
            return ZUSESession;
        }

        public Session MapOdooNotification(OdooOrderNotification orderNotification)
        {
            return new();
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
    internal class loyverseCategory
    {
        public string name { get; set; }
    }
    internal class loyverseItem
    {
        public string? category_id { get; set; }
    }

}

