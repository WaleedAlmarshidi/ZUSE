using System;
namespace ZUSE.Server.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ClientDetails
    {
        public string accept_language { get; set; }
        public object browser_height { get; set; }
        public string browser_ip { get; set; }
        public object browser_width { get; set; }
        public object session_hash { get; set; }
        public string user_agent { get; set; }
    }

    public class CurrentSubtotalPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class CurrentTotalDiscountsSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class CurrentTotalPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class CurrentTotalTaxSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class Fulfillment
    {
        public long id { get; set; }
        public string admin_graphql_api_id { get; set; }
        public DateTime created_at { get; set; }
        public long location_id { get; set; }
        public string name { get; set; }
        public long order_id { get; set; }
        public OriginAddress origin_address { get; set; }
        public Receipt receipt { get; set; }
        public string service { get; set; }
        public object shipment_status { get; set; }
        public string status { get; set; }
        public object tracking_company { get; set; }
        public object tracking_number { get; set; }
        public List<object> tracking_numbers { get; set; }
        public object tracking_url { get; set; }
        public List<object> tracking_urls { get; set; }
        public DateTime updated_at { get; set; }
        public List<ShopifyLineItem> line_items { get; set; }
    }

    public class ShopifyLineItem
    {
        public long id { get; set; }
        public string admin_graphql_api_id { get; set; }
        public int fulfillable_quantity { get; set; }
        public string fulfillment_service { get; set; }
        public string fulfillment_status { get; set; }
        public bool gift_card { get; set; }
        public int grams { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public PriceSet price_set { get; set; }
        public bool product_exists { get; set; }
        public long product_id { get; set; }
        public List<object> properties { get; set; }
        public int quantity { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public bool taxable { get; set; }
        public string title { get; set; }
        public string total_discount { get; set; }
        public TotalDiscountSet total_discount_set { get; set; }
        public long variant_id { get; set; }
        public string variant_inventory_management { get; set; }
        public string variant_title { get; set; }
        public string vendor { get; set; }
        public List<object> tax_lines { get; set; }
        public List<object> duties { get; set; }
        public List<object> discount_allocations { get; set; }
    }

    public class OriginAddress
    {
    }

    public class PresentmentMoney
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class Receipt
    {
    }

    public class ShopifyNotification
    {
        public long id { get; set; }
        public string admin_graphql_api_id { get; set; }
        public int app_id { get; set; }
        public string browser_ip { get; set; }
        public bool buyer_accepts_marketing { get; set; }
        public object cancel_reason { get; set; }
        public object cancelled_at { get; set; }
        public object cart_token { get; set; }
        public long checkout_id { get; set; }
        public string checkout_token { get; set; }
        public ClientDetails client_details { get; set; }
        public DateTime closed_at { get; set; }
        public object company { get; set; }
        public bool confirmed { get; set; }
        public object contact_email { get; set; }
        public DateTime created_at { get; set; }
        public string currency { get; set; }
        public string current_subtotal_price { get; set; }
        public CurrentSubtotalPriceSet current_subtotal_price_set { get; set; }
        public string current_total_discounts { get; set; }
        public CurrentTotalDiscountsSet current_total_discounts_set { get; set; }
        public object current_total_duties_set { get; set; }
        public string current_total_price { get; set; }
        public CurrentTotalPriceSet current_total_price_set { get; set; }
        public string current_total_tax { get; set; }
        public CurrentTotalTaxSet current_total_tax_set { get; set; }
        public string customer_locale { get; set; }
        public int device_id { get; set; }
        public List<object> discount_codes { get; set; }
        public string email { get; set; }
        public bool estimated_taxes { get; set; }
        public string financial_status { get; set; }
        public string fulfillment_status { get; set; }
        public object gateway { get; set; }
        public string landing_site { get; set; }
        public object landing_site_ref { get; set; }
        public long location_id { get; set; }
        public object merchant_of_record_app_id { get; set; }
        public string name { get; set; }
        public object note { get; set; }
        public List<object> note_attributes { get; set; }
        public int number { get; set; }
        public int order_number { get; set; }
        public string order_status_url { get; set; }
        public object original_total_duties_set { get; set; }
        public List<object> payment_gateway_names { get; set; }
        public object phone { get; set; }
        public string presentment_currency { get; set; }
        public DateTime processed_at { get; set; }
        public string processing_method { get; set; }
        public object reference { get; set; }
        public object referring_site { get; set; }
        public string source_identifier { get; set; }
        public string source_name { get; set; }
        public object source_url { get; set; }
        public string subtotal_price { get; set; }
        public SubtotalPriceSet subtotal_price_set { get; set; }
        public string tags { get; set; }
        public List<object> tax_lines { get; set; }
        public bool taxes_included { get; set; }
        public bool test { get; set; }
        public string token { get; set; }
        public string total_discounts { get; set; }
        public TotalDiscountsSet total_discounts_set { get; set; }
        public string total_line_items_price { get; set; }
        public TotalLineItemsPriceSet total_line_items_price_set { get; set; }
        public string total_outstanding { get; set; }
        public string total_price { get; set; }
        public TotalPriceSet total_price_set { get; set; }
        public TotalShippingPriceSet total_shipping_price_set { get; set; }
        public string total_tax { get; set; }
        public TotalTaxSet total_tax_set { get; set; }
        public string total_tip_received { get; set; }
        public int total_weight { get; set; }
        public DateTime updated_at { get; set; }
        public long user_id { get; set; }
        public List<object> discount_applications { get; set; }
        public List<Fulfillment> fulfillments { get; set; }
        public List<ShopifyLineItem> line_items { get; set; }
        public object payment_terms { get; set; }
        public List<object> refunds { get; set; }
        public List<object> shipping_lines { get; set; }
    }

    public class ShopMoney
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class SubtotalPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class TotalDiscountSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class TotalDiscountsSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class TotalLineItemsPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class TotalPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class TotalShippingPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class TotalTaxSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }
}



