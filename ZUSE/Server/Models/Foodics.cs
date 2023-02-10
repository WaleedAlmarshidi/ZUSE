using System;
namespace ZUSE.Server.Models
{
    class ResponseToFoodicsPing
    {
        public ResponseOfFoodicsLocalConnectionPing data { get; set; }
        public string debug { get; set; } = "respondToPing";
    }
    class ResponseOfFoodicsLocalConnectionPing
    {
        public Source source { get; set; }
        public ResponseTarget target { get; set; }
    }
    public class RequestOfFoodicsLocalConnectionPing
    {
        public Source source { get; set; }
        public RequestTarget target { get; set; }
    }
    public class ResponseTarget : RequestTarget
    {
        public string branch_id { get; set; }
        public string app_version { get; set; }
        public string system_version { get; set; }
        public string business_reference { get; set; }
    }
    public class RequestTarget
    {
        public string ip_address { get; set; }
        public string uuid { get; set; }
        public string id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
    }

    public class Source
    {
        public string branch_id { get; set; }
        public string app_version { get; set; }
        public string system_version { get; set; }
        public string business_reference { get; set; }
        public string id { get; set; }
        public int type { get; set; }
        public string ip_address { get; set; }
        public string name { get; set; }
    }
}

