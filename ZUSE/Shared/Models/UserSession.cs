using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ZUSE.Shared.Models
{
    public enum eventType
    {
        orderCreated,
        orderUpdated,
        orderClosed
    }
    public class Session
    {
        [Key]
        public string id { get; set; } = null!;
        [Key] 
        public string business_reference { get; set; } = null!;

        public string branch_reference { get; set; }
        public string? order_number { get; set; }
        public string? order_reference { get; set; }
        public eventType? event_type { get; set; }
        [Column(TypeName="DateTime")]
        public virtual DateTime created_at { get; set; }
        //public string? topic { get; set; }
        public int status { get; set; }
        public int type { get; set; }
        public int source { get; set; }
        public int delivery_status { get; set; }
        /// //////////
        /// </summary>
        public DateTime? closed_at { get; set; }
        public string? customer_notes { get; set; }
        public string? kitchen_notes { get; set; }
        public string? products { get; set; }
        public string? combos { get; set; }
        public string? products_kitchen_notes { get; set; }
        //public double? total_price { get; set; }
        //public double? total_cost { get; set; }
        public double? customer_satisfaction { get; set; }
        public string? customer { get; set; }
        public string? table_name { get; set; }
        //public Ui ui { get; set; }

        //public string? phone { get; set; }
        //public int? gender { get; set; }
        //public string? email { get; set; }
        //public string? name { get; set; }
        public string? browser_id { get; set; }
    }

    public class KdsSession : Session
    {
        //public override DateTime created_at
        //{
        //    get { return created_at.ToLocalTime(); }
        //    set { created_at = value; }
        //}
        public UI? ui = new();
        public bool isAllOrdersCompletedWithinCategoriesFilter { get; set; }
        public bool isAllOrdersCompleted { get; set; }

        [NotMapped]
        public List<ProductCollection>? productsCollection { get; set; }
        [NotMapped]
        public List<ComboCollection>? combosCollection { get; set; }
    }
    public class UI
    {
        public string gridSpan { get; set; }
        public string orderReferenceCompletion_color { get; set; }
        public string orderReferencePending_color { get; set; }
        public string orderReference_color { get; set; }
        public string orderTimeSpan { get; set; }
        public string orderCompletionTime { get; set; }
        public string timeStamp_color { get; set; }
        public string orderTypeImgSource { get; set; }
        public string orderTypeTitle{ get; set; }
        public TimeSpan orderCreationTimeDifference { get; set; }

        public bool orderCreationTimeAlertAlreadyRevealed { get; set; }
    }
    public class KdsSessionsCollection
    {
        public List<KdsSession> data { get; set; }
        public int sender_id { get; set; }
    }
    public class ProductsCombosSet
    {
        public string products { get; set; }
        public string combos { get; set; }
    }
}

