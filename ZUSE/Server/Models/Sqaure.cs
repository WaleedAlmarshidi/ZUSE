using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using ZUSE.Shared.Models;
using System.Globalization;

namespace ZUSE.Server.Models
{
    public class Sqaure
    {
        public static async Task<options> GetCategsListAsync()
        {
            var httpClient = new HttpClient();
            var orderRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri("https://connect.squareup.com/v2/catalog/list"),
                Method = HttpMethod.Get,
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer EAAAEaqTVwQAxzuf1B8ZNkc9ygJ25aPtrNsZvQ5WLX-98iD8nGZlL_N_kZy4mfdo" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
            };
            HttpResponseMessage responseMessage = await httpClient.SendAsync(orderRequestMessage);
            var catalogs = await responseMessage.Content.ReadFromJsonAsync<Catalogs>();
            var categs = catalogs.objects.Where(c => c.category_data is not null).ToList();
            var options = new options();
            foreach (var item in categs)
            {
                options.data.Add(new ProductCategory { name = item.category_data.name });
            }
            return options;
        }
    }
    public class Catalogs
    {
        public List<CatalogObject> objects { get; set; }
    }
    public class SqaureNotification
	{
		public string merchant_id { get; set; }
        [JsonConverter(typeof(DefaultDateTimeConverter))]
        public DateTime created_at { get; set; }
        public NotificationDate data { get; set; }
    }
    public class NotificationDate
    {
        public string id { get; set; }
        public EventObject @object { get; set; }
    }
    public class EventObject
    {
        public OrderDetails order_created { get; set; }
    }
    public class OrderDetails
    {
        public string order_id { get; set; }
        public string location_id { get; set; }
        public string state { get; set; }
    }
    public class SqaureOrder
    {
        public Order order { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
        public string? ticket_name { get; set; }
        public List<Line> line_Items { get; set; }
    }

    public class Line
    {
        public int quantity { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public string variation_name { get; set; }
        public string catalog_object_id { get; set; }
    }
    public class Catalog
    {
        public CatalogObject @object { get; set; }
        public List<RelatedItems> related_objects { get; set; }
    }

    public class CatalogObject
    {
        public CategoryData category_data { get; set; }
    }

    public class RelatedItems
    {
        public ItemData item_data { get; set; }
        public CategoryData category_data { get; set; }
    }

    public class CategoryData
    {
        public string name { get; set; }
    }

    public class ItemData
    {
        public string category_id { get; set; }
    }
}