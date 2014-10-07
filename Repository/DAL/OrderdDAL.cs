using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Repository.Enum;
using BakeBFlake.Controllers;

namespace Repository.DAL
{
    public class OrderdDAL
    {

        public static void initData()
        {
            var Orders = new List<Order>
                {
                    new Order(){CustomerID="305631491", Status=OrderStatus.Accepted, OrderDate=DateTime.Today.AddDays(-1), DelieveryDate=DateTime.Today.AddDays(2), OrderDetails=new List<OrderDetails>(){new OrderDetails(){PasteryId=3, TotalAmount=9}}},
                    new Order(){CustomerID="304848370", Status=OrderStatus.Accepted, OrderDate=DateTime.Today.AddDays(-1), DelieveryDate=DateTime.Today.AddDays(2), OrderDetails=new List<OrderDetails>(){new OrderDetails(){PasteryId=4, TotalAmount=5}}},
                    new Order(){CustomerID="456734564", Status=OrderStatus.InProgress, OrderDate=DateTime.Today.AddDays(-1), DelieveryDate=DateTime.Today.AddDays(2), OrderDetails=new List<OrderDetails>(){new OrderDetails(){PasteryId=10, TotalAmount=5}}},
                    new Order(){CustomerID="987654321", Status=OrderStatus.Accepted, OrderDate=DateTime.Today.AddDays(-1), DelieveryDate=DateTime.Today.AddDays(2), OrderDetails=new List<OrderDetails>(){new OrderDetails(){PasteryId=15, TotalAmount=20}}},
                    new Order(){CustomerID="657586747", Status=OrderStatus.Accepted, OrderDate=DateTime.Today.AddDays(-1), DelieveryDate=DateTime.Today.AddDays(2), OrderDetails=new List<OrderDetails>(){new OrderDetails(){PasteryId=7, TotalAmount=36}}}
                };

            foreach (var ord in Orders)
            {
                AddNewOrder(ord);
            }
        }

        public static int AddNewOrder(Order NewOrder)
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            {
                Contexts.DB.Orders.Add(NewOrder);
                Contexts.DB.SaveChanges();

                return NewOrder.ID;
            }
        }

        public static List<Order> GetAllOrders()
        {
            //\\//using (PastryContext Contexts.DB = new PastryContext())
            {
                return Contexts.DB.Orders.ToList();
            }
        }

        public static void UpdateOrder(Order OrderToUpdate)
        {
            //\\//using (PastryContext Contexts.DB = new PastryContext())
            {
                Contexts.DB.Orders.AddOrUpdate(OrderToUpdate);
                int num = Contexts.DB.SaveChanges();
            }
        }

        public static Order GetOrder(Int32 orderID)
        {
            //\\//using (PastryContext Contexts.DB = new PastryContext())
            {
                return (from ord in Contexts.DB.Orders
                        where ord.ID == orderID
                        select ord).FirstOrDefault();
            }
        }

        public static void DeleteAllRelatedOrders(ICollection<Order> allUserOrders)
        {
            foreach (Order o in allUserOrders)
            {
                o.OrderDetails.Clear();
                DeleteOrder(o.ID);
            }
        }

        public static void DeleteAllRelatedDetails(ICollection<OrderDetails> allDetails)
        {
            List<OrderDetails> details = allDetails.ToList();

            foreach (OrderDetails ord in details)
            {
                OrderDetailsDAL.DeleteOrderDetails(ord);
            }
        }

        public static void DeleteOrder(Int32 orderIDToRemove)
        {
            ////using (PastryContext Contexts.DB = new PastryContext())
            {
                var order = GetOrder(orderIDToRemove);
                Contexts.DB.Entry(order).State = EntityState.Deleted;

                int num = Contexts.DB.SaveChanges();
            }
        }
        public static List<myClass> MostOrderedPastery()
        {
            using (PastryContext db = new PastryContext())
            {
                //var s = db.OrderDetailes.Join(db.Pastries).GroupBy(gb => gb.PasteryId).Select(g => new { Item = g.Key, Quantity = g.Sum(i => i.TotalAmount) });
                //var query = (from orderedPastery in db.OrderDetailes
                //            group orderedPastery by {new orderedPastery.PasteryId} into ran 
                //            select new { Item = ran.sum(TotalAmount), ran.OrderDetails.PasteryId)}; 
                var query = (from op in db.OrderDetailes
                             join p in db.Pastries on op.PasteryId equals p.ID
                             select new { op.PasteryId, op.TotalAmount, p.Name, p.ImageLink } into x
                             group x by new { x.PasteryId, x.Name, x.ImageLink } into g
                             select new myClass
                             {
                                 Name = g.Key.Name,
                                 Image = g.Key.ImageLink,
                                 ID = g.Key.PasteryId,
                                 Quantity = g.Sum(y => y.TotalAmount)
                             });


                return query.ToList<myClass>();//.ToArray();
            }
        }

        public static List<Order> SelectByCriteria(int? OrderID = null, bool? isPrefered = null, DateTime? OrderDate = null, DateTime? DeliveryDate = null, int? TotalAmount = null, string Comments = null, OrderStatus? Status = null, string CustomerID = null, int? pastryID = null)
        {
            ////using (PastryContext Contexts.DB = new PastryContext())
            {
                IList<Order> result = Contexts.DB.Orders.ToList();

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
                    result = result.Where(p => p.TotalPrice < TotalAmount).ToList();
                }
                if (Comments != null && Comments != "")
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
                if (isPrefered != null)
                {
                    result = result.Where(p => p.Customer.Prefered == isPrefered).ToList();
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
