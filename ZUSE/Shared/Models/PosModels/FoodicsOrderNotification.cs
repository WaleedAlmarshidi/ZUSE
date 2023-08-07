using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.FileIO;
using ZUSE.Shared.Models;

namespace ZUSE.Shared.Models
{
    public class Business
    {
        public string? name { get; set; }
        public int reference { get; set; }
    }
    public class FoodicsBranch
    {
        public string id { get; set; }
        public string reference { get; set; }
    }
    public class FoodicsLocalNetworkOrder
    {
        public LocalNetWorkOrder order { get; set; }
        public Source source { get; set; }
    }

    public class Source
    {
        public string branch_id { get; set; }
        public string business_reference { get; set; }
    }
    public class LocalNetworkFoodicsTableSection
    {
        public string name { get; set; }
    }
    public class LocalNetworkFoodicsTable
    {
        public LocalNetworkFoodicsTableSection section { get; set; }
        public string name { get; set; }
    }
    public class LocalNetWorkOrder
    {
        public string uuid { get; set; }
        public User user { get; set; }
        public int status { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime opened_at { get; set; }
        public int type { get; set; }
        public int number { get; set; }
        public string? notes { get; set; }
        public LocalNetworkFoodicsTable? table { get; set; }
        public List<LocalProductCollection> products { get; set; }
    }

    public class User
    {
        public string name { get; set; }
    }

    public class LocalProductCollection
    {
        public int status { get; set; }
        public string? notes { get; set; }
        public double quantity { get; set; }
        public SingleProduct product { get; set; }
        public List<LocalSingleProductOptions>? options { get; set; }
    }

    public class LocalSingleProductOptions
    {
        public LocalOption option { get; set; }
        public int quantity { get; set; }
    }

    public class LocalOption
    {
        public string name { get; set; }
        public string? name_localized { get; set; }
    }

    public class Customer
    {
        /// <summary>
        /// ///
        /// </summary>
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public int? gender { get; set; }
        public string? birth_date { get; set; }
        public string? last_order_at { get; set; }
        public int? order_count { get; set; }
        public bool? isCalled { get; set; } = false;
    }
    public class OrderInfo
    {
        public string id { get; set; } = null!;
        public int number { get; set; }
        public int? reference { get; set; }
        public int status { get; set; }
        public int? delivery_status { get; set; }
        public int type { get; set; }
        public int source { get; set; }
        public Table? table { get; set; }
        public FoodicsBranch branch { get; set; } = null!;
        public ICollection<Payment> payments { get; set; }
        public ICollection<ProductCollection>? products { get; set; }
        public Customer? customer { get; set; }
        public ICollection<ComboCollection>? combos { get; set; }
        public object? kitchen_notes { get; set; }
        public object? customer_notes { get; set; }
        public OrderMeta? meta { get; set; }
        public string? opened_at { get; set; }
        public string? accepted_at { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? closed_at { get; set; }
    }

    public class Payment
    {
        public PaymentMethod payment_method { get; set; }
    }
    public enum PaymentType
    {
        Cash = 1,
        Card,
        Other,
        GiftCard,
        HouseAccount,
        Thirdparty = 7
    }
    public class PaymentMethod
    {
        public string name { get; set; }
        public PaymentType type { get; set; }
    }

    public class OrderMeta
    {
        public string? external_number { get; set; }
    }
    public class ComboCollection
    {
        public ComboSize combo_size { get; set; }
        public double quantity { get; set; }
        public List<ProductCollection> products { get; set; } = new();
    }

    public class ComboSize
    {
        public string name { get; set; }
        public ComboDetails combo { get; set; }
    }

    public class ComboDetails
    {
        public string name { get; set; }
    }

    public enum productMarks
    {
        normal,
        ready,
        added,
        removed
    }
    public class ProductCollection
    {
        public SingleProduct product { get; set; }
        public List<SingleProductOptions>? options { get; set; }
        public double quantity { get; set; } = 1;
        public string? kitchen_notes { get; set; }
        public productMarks? stage { get; set; } = productMarks.normal;

        public ProductCollectionUI? ui = new();
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ProductCollection x = (ProductCollection)obj;

            //return x.product.name.Equals(this?.product.name) && (x.kitchen_notes?.Equals(this.kitchen_notes)).GetValueOrDefault()
            //    &&
            //    (x.options?.All(xOp => (this.options?.Any(yOp => (yOp?.modifier_option?.name?.Equals(xOp?.modifier_option?.name)).GetValueOrDefault())).GetValueOrDefault())).GetValueOrDefault();
            var isSameOptions = true;
            if (this.options is not null || x.options is not null)
                if (this.options is not null && x.options is not null)
                    isSameOptions = (this.options?.All(xOp => (x.options?.Any(yOp => (yOp?.modifier_option?.name?.Equals(xOp?.modifier_option?.name)).GetValueOrDefault())).GetValueOrDefault())).GetValueOrDefault();
                else
                    isSameOptions = false;

            bool isSame = this.product.name.Equals(x?.product.name) && string.Equals(x.kitchen_notes, x.kitchen_notes)
                &&
                isSameOptions;

            return isSame;
        }
        //public override bool Equals(object? obj)
        //{
        //    //return base.Equals(obj);
        //    var y = obj as ProductCollection;
        //    return this.product.name.Equals(y?.product.name) && (this.kitchen_notes?.Equals(y.kitchen_notes)).GetValueOrDefault()
        //&&
        //(x.options?.All(xOp => (y.options?.Any(yOp => (yOp?.modifier_option?.name?.Equals(xOp?.modifier_option?.name)).GetValueOrDefault())).GetValueOrDefault())).GetValueOrDefault();
        //}
        //{
        //}
    }
    public class CollectionsComparer : IEqualityComparer<ProductCollection>
    {
        // compares collections by options, names, notes, quantity
        public bool Equals(ProductCollection? x, ProductCollection? y)
        {
            //return x.product.name.Equals(this?.product.name) && (x.kitchen_notes?.Equals(this.kitchen_notes)).GetValueOrDefault()
            //    &&
            //    (x.options?.All(xOp => (this.options?.Any(yOp => (yOp?.modifier_option?.name?.Equals(xOp?.modifier_option?.name)).GetValueOrDefault())).GetValueOrDefault())).GetValueOrDefault();
            var isSameOptions = true;
            if (y.options is not null || x.options is not null)
                if (y.options is not null && x.options is not null)
                    isSameOptions = (y.options?.All(xOp => (x.options?.Any(yOp => (yOp?.modifier_option?.name?.Equals(xOp?.modifier_option?.name)).GetValueOrDefault())).GetValueOrDefault())).GetValueOrDefault();
                else
                    isSameOptions = false;

            bool isSame = y.product.name.Equals(x?.product.name) && string.Equals(x.kitchen_notes, x.kitchen_notes)
                &&
                isSameOptions
                &&
                y.quantity == x.quantity;

            return isSame;
        }

        public int GetHashCode([DisallowNull] ProductCollection obj)
        {
            return base.GetHashCode();
        }
        bool IEqualityComparer<ProductCollection>.Equals(ProductCollection x, ProductCollection y)
        {
            return Equals(x, y);
        }

        int IEqualityComparer<ProductCollection>.GetHashCode(ProductCollection obj)
        {
            return GetHashCode(obj);
        }
    }
    public class ProductCollectionUI
    {
        public string orderDetail_styleClass { get; set; }
        public string orderDetailCompletion_color { get; set; }
        public string orderDetailPending_color { get; set; }
    }
    public class SingleProduct
    {
        public string name { get; set; }
        public ProductCategory category { get; set; }
        public string? name_localized { get; set; }
        //public string? kitchen_notes { get; set; }
    }

    public class options
    {
        public List<ProductCategory> data { get; set; } = new();
        public Link? links { get; set; }
    }

    public class Link
    {
        public string? next { get; set; }
    }

    public class Tables
    {
        public ICollection<ProductCategory> data { get; set; }
    }
    public class Sections
    {
        public ICollection<ProductCategory> data { get; set; }
    }
    public class Table
    {
        public string name { get; set; }
    }
    public class ProductCategory
    {
        public string name { get; set; }
        public bool? is_ready { get; set; }
    }

    public class SingleProductOptions
    {
        public SingleProductModifierOption? modifier_option { get; set; }

    }

    public class SingleProductModifierOption
    {
        public string? name { get; set; }
        public double? quantity { get; set; }
        public string? name_localized { get; set; }
    }
    public class FoodicsOrderNotification
    {
        public string @event { get; set; }
        public Business business { get; set; }
        public OrderInfo order { get; set; }
    }
}

