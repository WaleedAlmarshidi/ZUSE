using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZUSE.Client.Models
{
    public class Order
    {
        public string OrderReference { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string OrderDate { get; set; }
        public string RawOrderDate { get; set; }
    }
}
