using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public static class OrderDetailsDAL
    {
        public static void DeleteRelatedOrderDetails(int orderDetail)
        {
            List<OrderDetails> allDetails = GetAllRelatedDetails(orderDetail);

            foreach (OrderDetails ord in allDetails)
            {
                DeleteOrderDetails(ord);
            }
        }

        public static void DeleteOrderDetails(OrderDetails orderDetails)
        {

            Contexts.DB.Entry(orderDetails).State = EntityState.Deleted;

            int num = Contexts.DB.SaveChanges();
        }

        public static OrderDetails GetOrderDetails(Int32 orderDetailsID)
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            {
                return (from ord in Contexts.DB.OrderDetailes
                        where ord.ID == orderDetailsID
                        select ord).FirstOrDefault();
            }
        }

        public static List<OrderDetails> GetAllRelatedDetails(int OrderID)
        {
            //using (PastryContext Contexts.DB = new PastryContext())
            {
                IList<OrderDetails> result = Contexts.DB.OrderDetailes.ToList();

                result = result.Where(p => p.OrderID.Equals(OrderID)).ToList();

                return result.ToList();
            }
        }
    }
}
