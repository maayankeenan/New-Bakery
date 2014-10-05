using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeBFlake.Models
{
    public class Order
    {
        public Int32 Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Int32 CustomerId { get; set; }
        public DateTime DelieveryDate { get; set; }
        public Double TotalPrice { get; set; }
        public String Comments { get; set; }
        public String Status { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
   
    }
}