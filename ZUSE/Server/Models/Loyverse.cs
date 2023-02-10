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
		[JsonConverter(typeof(LoyverseDateTimeConverter))]
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

public class LoyverseDateTimeConverter : JsonConverter<DateTime>
    {

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //Console.WriteLine("in Read datetime : " + reader.GetString());
            return DateTime.ParseExact(reader.GetString(),"yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            //Don't implement this unless you're going to use the custom converter for serialization too
            throw new NotImplementedException();
        }
    }
    public class LineModifier
    {
		public string name { get; set; }
		public string option { get; set; }
	}
}

