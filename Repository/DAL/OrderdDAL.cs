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
        public static int AddNewOrder(Order NewOrder)
        {
            using (PastryContext db = new PastryContext())
            {
                db.Orders.Add(NewOrder);
                db.SaveChanges();

                return NewOrder.ID;
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

        public static Order GetOrder(Int32 orderID)
        {
            using (PastryContext db = new PastryContext())
            {
                return (from ord in db.Orders
                        where ord.ID == orderID
                        select ord).FirstOrDefault();
            }
        }

        public static void DeleteOrder(Int32 orderIDToRemove)
        {
            using (PastryContext db = new PastryContext())
            {
                var order = GetOrder(orderIDToRemove);
                db.Entry(order).State = EntityState.Deleted;

                int num = db.SaveChanges();
            }
        }

        public static void MostOrderedPastery()
        {
            using (PastryContext db = new PastryContext())
            {
                //var s = db.OrderDetailes.Join(db.Pastries).GroupBy(gb => gb.PasteryId).Select(g => new { Item = g.Key, Quantity = g.Sum(i => i.TotalAmount) });
                //var query = (from orderedPastery in db.OrderDetailes
                //            group orderedPastery by {new orderedPastery.PasteryId} into ran 
                //            select new { Item = ran.sum(TotalAmount), ran.OrderDetails.PasteryId)}; 
                var query = (from op in db.OrderDetailes
                             join p in db.Pastries on op.PasteryId equals p.ID
                             select new { op.PasteryId, op.TotalAmount, p.Name } into x
                             group x by new { x.PasteryId, x.Name } into g
                             select new
                             {
                                 Name = g.Key.Name,
                                 Quantity = g.Sum(y => y.TotalAmount)
                             });

            }
        }

        public static List<Order> SelectByCriteria(int? OrderID = null, DateTime? OrderDate = null, DateTime? DeliveryDate = null, int? TotalAmount = null, string Comments = null, OrderStatus? Status = null, string CustomerID = null, int? pastryID = null)
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
                    //result = result.ToList().Where(p => (((ICollection<OrderDetails>)p.OrderDetails).ToList()).Where(a=>a.PasteryId==pastryID));
                    result = result.Where(p => (p.OrderDetails).Any(a => a.PasteryId == pastryID)).ToList();
                }


                return result.ToList();
            }
        }
    }
}
