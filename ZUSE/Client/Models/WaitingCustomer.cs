using System;
namespace ZUSE.Client.Models
{
    public class WaitingCustomer
    {
        public Order Order { get; set; } = null!;
        public static Interaction? SelectedLang;
    }
}

