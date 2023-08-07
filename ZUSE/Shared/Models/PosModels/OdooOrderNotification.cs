using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ZUSE.Shared.Models.PosModels
{
    public class OdooOrderNotification
    {
        public string company_name { get; set; }
        public string branch_name { get; set; }
        public OdooOrderDetails data { get; set; }
    }
    public class OdooOrderDetails
    {
        public string receipt_number { get; set; }
        public string order_ref { get; set; }
        public string note { get; set; }
        public int order_type { get; set; }
        public string? kitchen_notes { get; set; }
        public string? table { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime date { get; set; }
        public string customer { get; set; }
        public List<OdooSingleProduct> products_details { get; set; }
    }

    public class OdooSingleProduct
    {
        public double qty { get; set; }
        public string full_product_name { get; set; }
        public string category { get; set; }
        public string? kitchen_notes { get; set; }
    }
}

