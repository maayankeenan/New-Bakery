using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class OrderDetails
    {
        public Int32 ID { get; set; }
        public Int32 OrderID { get; set; }
        public Int32 PasteryId { get; set; }
        public Int32 TotalAmount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Pastery Pastery { get; set; }
    }
}
