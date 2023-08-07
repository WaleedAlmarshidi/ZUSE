using System;
using System.Net;
using System.Text.Json;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared.Models;
using ZUSE.Shared.Models.PosModels;

namespace ZUSE.Server.Mappers
{
    public class SqaureMapper
    {
        public async Task<Session> MapSqaureNotification(SqaureNotification sqaureNotification)
        {
            var httpClient = new HttpClient();
            var orderRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://connect.squareup.com/v2/orders/{sqaureNotification.data.@object.order_created.order_id}"),
                Method = HttpMethod.Get,
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer EAAAEaqTVwQAxzuf1B8ZNkc9ygJ25aPtrNsZvQ5WLX-98iD8nGZlL_N_kZy4mfdo" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
            };
            HttpResponseMessage response;
            response = await httpClient.SendAsync(orderRequestMessage);
            var sqaureOrder = await response.Content.ReadFromJsonAsync<SqaureOrder>();

            var collections = new List<ProductCollection>();

            foreach (var item in sqaureOrder.order.line_Items)
            {
                var itemDataRequestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://connect.squareup.com/v2/catalog/object/{item.catalog_object_id}?include_related_objects=true"),
                    Method = HttpMethod.Get,
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer EAAAEaqTVwQAxzuf1B8ZNkc9ygJ25aPtrNsZvQ5WLX-98iD8nGZlL_N_kZy4mfdo" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
                };
                HttpResponseMessage itemDataResponse;
                itemDataResponse = await httpClient.SendAsync(itemDataRequestMessage);
                
                var itemData = await itemDataResponse.Content.ReadFromJsonAsync<Catalog>();

                string category = string.Empty;

                if(itemData.related_objects is not null)
                {
                    var CategoryRequestMessage = new HttpRequestMessage
                    {
                        RequestUri = new Uri($"https://connect.squareup.com/v2/catalog/object/{itemData.related_objects[0].item_data.category_id}?include_related_objects=true"),
                        Method = HttpMethod.Get,
                        Headers = {
                            { HttpRequestHeader.Authorization.ToString(), $"Bearer EAAAEaqTVwQAxzuf1B8ZNkc9ygJ25aPtrNsZvQ5WLX-98iD8nGZlL_N_kZy4mfdo" },
                            { HttpRequestHeader.Accept.ToString(), "application/json" },
                            { HttpRequestHeader.ContentType.ToString(), "application/json" }
                        }
                    };
                    HttpResponseMessage categoryResponse = await httpClient.SendAsync(CategoryRequestMessage);

                    string x = await categoryResponse.Content.ReadAsStringAsync(); 
                    var catalogCategory = await categoryResponse.Content.ReadFromJsonAsync<Catalog>();
                    category = catalogCategory.@object.category_data.name;
                }

                var variationsSplitted = item.variation_name.Split(',');
                var options = new List<SingleProductOptions>();
                foreach (var option in variationsSplitted)
                {
                    options.Add(new SingleProductOptions { modifier_option = new SingleProductModifierOption { name = option.Trim() } });
                }
                collections.Add(
                    new ProductCollection
                    {
                        product = new SingleProduct
                        {
                            name = item.name,
                            category = new ProductCategory
                            {
                                name = category
                            }
                        },
                        options = options,
                        quantity = item.quantity,
                        kitchen_notes = item.note
                    });
            }
            var session = new Session
            {
                business_reference = sqaureNotification.merchant_id,
                branch_reference = sqaureNotification.data.@object.order_created.location_id,
                products = JsonSerializer.Serialize(collections),
                id = sqaureNotification.data.@object.order_created.order_id,
                created_at = sqaureNotification.created_at.ToUniversalTime(),
                delivery_status = 0,
                order_number = sqaureOrder.order.ticket_name is null ? "123" : sqaureOrder.order.ticket_name,
                status = 1,
                source = 1,
                type = 2,
                kitchen_notes = string.Empty
            };
            return session;
        }
    }
}
