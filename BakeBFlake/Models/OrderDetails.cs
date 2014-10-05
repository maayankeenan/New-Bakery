using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeBFlake.Models
{
    public class OrderDetails
    {
        public Int32 Id { get; set; }
        public Int32 OrderId { get; set; }
        public Int32 PasteryId { get; set; }
        public String PasteryName { get; set; }
        public Decimal Price { get; set; }
        public Int32 Amount { get; set; }
    }
}