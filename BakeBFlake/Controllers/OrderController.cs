using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeBFlake.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index(bool? isPrefered = null, string id = null, string price = null, string customerId = null, string comments = null)
        {
            ViewBag.Message = "Orders List";

            var orders = new List<Order>();

            int? intId = null;
            bool errorId = false;
            if (id != null)
            {
                try
                {
                    intId = int.Parse(id);
                }
                catch (Exception ex)
                {
                    errorId = true;
                }
            }

            int? intPrice = null;
            bool errorPrice = false;
            if (id != null)
            {
                try
                {
                    intPrice = int.Parse(price);
                }
                catch (Exception ex)
                {
                    errorPrice = true;
                }
            }

            if (!errorPrice && !errorId)
            {

                if (Session["LoginUser"] != null)
                {
                    var cust = Session["LoginUser"] as Customer;
                    if (cust.IsAdmin)
                    {
                        orders = Repository.DAL.OrderdDAL.SelectByCriteria(intId, isPrefered, null, null, intPrice, comments, null, customerId, null);
                    }
                    else
                    {
                        orders = Repository.DAL.OrderdDAL.SelectByCriteria(intId, isPrefered, null, null, intPrice, comments, null, cust.ID, null);
                    }
                }
            }

            //var orders = new List<Order>();
            //var order1 = new Order(DateTime.Now, DateTime.Now, 654, "vfdsvs", Repository.Enum.OrderStatus.Accepted, "6532");
            //orders.Add(order1);

            return View(orders);
        }

        public ActionResult Delete(int id)
        {
            Repository.DAL.OrderdDAL.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        public ActionResult View(int id)
        {
            // get order from repo
            //var orderDetails = new List<OrderDetails>();
            //orderDetails.Add(new OrderDetails() { ID = 1, PasteryId = 2, Pastery = new Pastery() { Name = "Choclate Cake", Price = 19.9, ID = 1 }, TotalAmount = 1 });
            //orderDetails.Add(new OrderDetails() { ID = 1, PasteryId = 2, Pastery = new Pastery() { Name = "Onion Bagel", Price = 19.9, ID = 1 }, TotalAmount = 1 });
            //orderDetails.Add(new OrderDetails() { ID = 1, PasteryId = 2, Pastery = new Pastery() { Name = "Rye Bread", Price = 19.9, ID = 1 }, TotalAmount = 1 });
            //var order = new Order(){ID = 1, OrderDate = new DateTime(2014, 10, 3), Status= Repository.Enum.OrderStatus.InProgress, TotalPrice = 89.90, OrderDetails = orderDetails};
            var order = Repository.DAL.OrderdDAL.SelectByCriteria(id);
            ViewBag.EditOrder = false;
            return View(order);
        }

        public ActionResult Edit(int id)
        {
            // get order from repo
            //var orderDetails = new List<OrderDetails>();
            //orderDetails.Add(new OrderDetails() { ID = 1, PasteryId = 2, Pastery = new Pastery() { Name = "Choclate Cake", Price = 19.9, ID = 1 }, TotalAmount = 1 });
            //orderDetails.Add(new OrderDetails() { ID = 1, PasteryId = 2, Pastery = new Pastery() { Name = "Onion Bagel", Price = 19.9, ID = 1 }, TotalAmount = 1 });
            //orderDetails.Add(new OrderDetails() { ID = 1, PasteryId = 2, Pastery = new Pastery() { Name = "Rye Bread", Price = 19.9, ID = 1 }, TotalAmount = 1 });
            //var order = new Order() { ID = 1, OrderDate = new DateTime(2014, 10, 3), Status = Repository.Enum.OrderStatus.InProgress, TotalPrice = 89.90, OrderDetails = orderDetails };
            var order = Repository.DAL.OrderdDAL.SelectByCriteria(id)[0];
            ViewBag.EditOrder = true;
            Session["CurrentOrder"] = order;
            return View(order);
        }

        public bool RemoveItem(int id)
        {
            var order = (Order)Session["CurrentOrder"];
            var item = order.OrderDetails.Find(x => x.ID == id);
            order.OrderDetails.Remove(item);
            return true;
        }

        [HttpPost]
        public ActionResult Save(FormCollection collection)
        {
            var model = (Order)Session["CurrentOrder"];
            try
            {
                TryUpdateModel(model);
                Repository.DAL.OrderdDAL.UpdateOrder(model);
            }
            catch (Exception e)
            {

            }
            Session["CurrentOrder"] = null;
            return RedirectToAction("View", new { id = model.ID });
        }

    }
}
