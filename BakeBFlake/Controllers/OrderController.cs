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
        public ActionResult Index(string customerId, int price, string comments)
        {
            ViewBag.Message = "Orders List";

            var orders = Repository.DAL.OrderdDAL.SelectByCriteria(null, null, null, price, comments, null, customerId, null);

            //var orders = new List<Order>();
            //var order1 = new Order(123, DateTime.Now, DateTime.Now, 654, "vfdsvs", Repository.Enum.OrderStatus.Accepted, "6532");
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
            var order = Repository.DAL.OrderdDAL.SelectByCriteria(id);
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
