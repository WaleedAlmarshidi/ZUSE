using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Net;
using ZUSE.Shared.Models;
using System.Globalization;

namespace ZUSE.Server.Models
{
	public class Loyverse
	{
		public static async Task<options> GetCategsListAsync(ZUSEClient servceProvider)
		{
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(servceProvider.pos_categories_fetch_url),
                Headers = {
                        { HttpRequestHeader.Authorization.ToString(), $"Bearer {servceProvider.access_token}" },
                        { HttpRequestHeader.Accept.ToString(), "application/json" },
                        { HttpRequestHeader.ContentType.ToString(), "application/json" }
                    }
            };
			var client = new HttpClient();
            var result = await client.SendAsync(requestMessage);
            var newResponse = await result?.Content?.ReadFromJsonAsync<LoyverseCategsList>();
			var options = new options();
			options.data.AddRange(newResponse.categories);
			return options;
        }
	}

    internal class LoyverseCategsList
    {
		public List<ProductCategory> categories { get; set; }
	}

    public class LoyverseNotification
	{
		public string merchant_id { get; set; }
		[JsonConverter(typeof(DefaultDateTimeConverter))]
		public DateTime created_at { get; set; }
		public List<receipt> receipts { get; set; }
	}

    public class receipt
    {
		public string receipt_number { get; set; }
		//public string order { get; set; }
		public string? note { get; set; }
		public string store_id { get; set; }
		public ICollection<LineItem> line_items { get; set; }
	}

    public class LineItem
    {
		public string item_name { get; set; }
		public string item_id { get; set; }
		public int quantity { get; set; }
		public string? line_note { get; set; }
		public string? category { get; set; }
		public ICollection<LineModifier> line_modifiers { get; set; }

    }
    public class LineModifier
    {
		public string name { get; set; }
		public string option { get; set; }
	}
}

