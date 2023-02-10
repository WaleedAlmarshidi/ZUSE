using System;

namespace ZUSE.Shared.Models
{
    public class UserInitiatedSession
    {
        public string topic { get; set; } = null!;
        public string order_reference { get; set; } = null!;
        public string created_at { get; set; } = null!;
        public int delivery_status { get; set; }
        public string? push_notification_subsicribtion { get; set; }
        public string? phone { get; set; }
        public string browser_id { get; set; } = null!;
        public bool isCalled { get; set; } = false;
    }
}

