using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Repository.Enum;

namespace Repository.DAL
{
    public class OrderdDAL
    {
        public static void AddNewOrder(Order NewOrder)
        {
            using (PastryContext db = new PastryContext())
            {
                db.Orders.Add(NewOrder);
                db.SaveChanges();
            }
        }

        public static List<Order> GetAllOrders()
        {
            using (PastryContext db = new PastryContext())
            {
                return db.Orders.ToList();
            }

        }

        public static void UpdateOrder(Order OrderToUpdate)
        {
            using (var db = new PastryContext())
            {
                db.Orders.AddOrUpdate(OrderToUpdate);
                int num = db.SaveChanges();
            }
        }

        public static Order GetOrder(Order order)
        {
            using (PastryContext db = new PastryContext())
            {
                return (from ord in db.Orders
                        where ord.ID == order.ID
                        select ord).FirstOrDefault();
            }
        }

        public static void DeleteOrder(Order orderToRemove)
        {
            using (PastryContext db = new PastryContext())
            {
                var order = GetOrder(orderToRemove);
                db.Entry(orderToRemove).State = EntityState.Deleted;

                int num = db.SaveChanges();
            }
        }

        public static List<Order> SelectByCriteria(int? OrderID = null, DateTime? OrderDate = null, DateTime? DeliveryDate = null, int? TotalAmount = null, string Comments = null, OrderStatus? Status = null, string CustomerID = null, int? pastryID=null)
        {
            using (PastryContext db = new PastryContext())
            {
                IList<Order> result = db.Orders.ToList();

                if (OrderID != null)
                {
                    result = result.Where(p => p.ID.Equals(OrderID)).ToList();
                }
                if (OrderDate != null)
                {
                    result = result.Where(p => p.OrderDate.Equals(OrderDate)).ToList();
                }
                if (DeliveryDate != null)
                {
                    result = result.Where(p => p.DelieveryDate.Equals(DeliveryDate)).ToList();
                }
                if (TotalAmount != null)
                {
                    result = result.Where(p => p.TotalPrice.Equals(TotalAmount)).ToList();
                }
                if (Comments != null)
                {
                    result = result.Where(p => p.Comments.Contains(Comments)).ToList();
                }
                if (Status != null)
                {
                    result = result.Where(p => p.Status == Status).ToList();
                }
                if (CustomerID != null)
                {
                    result = result.Where(p => p.CustomerID.Contains(CustomerID)).ToList();
                }
                if (pastryID != null)
                {
                    //result.ToList().ForEach(p => ((ICollection<OrderDetails>)p.OrderDetailes).ToList().ForEach(a=>a.pastryID == pastryID));
                }
                

                return result.ToList();
            }
        }

        public static void addToOrderDetails (OrderDetails a)
        {
            using (PastryContext db = new PastryContext())
            {
                db.OrderDetailes.Add(a);
                db.SaveChanges();
            }
        }

    }
}
