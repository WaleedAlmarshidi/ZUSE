using System;
namespace ZUSE.Client.Models
{
    public class ToUserPayload
    {

        public string order_reference { get; set; } = null!;
        public string closed_at { get; set; } = null!;
        public string browser_id { get; set; } = null!;
        //public ICollection<CalledOrder> calledOrders { get; set; }
    }
    public class CalledOrder
    {
        public string order_reference { get; set; } = null!;
        public DateTime closed_at { get; set; }
        public string browser_id { get; set; } = null!;
    }
}

