using System;

namespace ZUSE.Shared.Models
{
    public class TvNotification
    {
        public string order_reference { get; set; } = null!;
        public int? delivery_status { get; set; }
        public DateTime created_at { get; set; }
    }
}

