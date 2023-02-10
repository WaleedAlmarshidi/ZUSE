using System;
namespace ZUSE.Client.Models
{
    public class ToTvMsg
    {
        public string order_reference { get; set; } = null!;
        public int? delivery_status { get; set; }
        public string? tv_text_to_speach { get; set; }
    }
}

