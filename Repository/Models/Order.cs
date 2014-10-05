using Repository.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Order
    {
        public Int32 ID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DelieveryDate { get; set; }
        public Double TotalPrice { get; set; }
        public string Comments { get; set; }
        public OrderStatus Status { get; set; }
        public string CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }

        public Order (Int32 OrderID, DateTime OrderDate, DateTime DeliveryDate, int TotalAmount, string Comments, OrderStatus Status, string CustomerID)
        {
            this.ID = OrderID;
            this.OrderDate = OrderDate;
            this.DelieveryDate = DeliveryDate;
            this.TotalPrice = TotalAmount;
            this.Comments = Comments;
            this.Status = Status;
            this.CustomerID = CustomerID;
        }

        public Order()
        {

        }
    }
}
