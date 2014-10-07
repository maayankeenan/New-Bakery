using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeBFlake.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            var cart = Session["Cart"];
            return View(cart);
        }

        public bool RemoveItem(Int32 id)
        {
            Order cart = (Order)Session["Cart"];
            var item = cart.OrderDetails.Find(x => x.ID == id);
            if (item != null)
            {
                cart.OrderDetails.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Checkout()
        {
            //save order to db and return order model
            var order = (Order)Session["Cart"];
            order.CustomerID = (Session["LoginUser"] as Customer).ID;

            try
            {
                Repository.DAL.OrderdDAL.AddNewOrder(order);
            }
            catch
            {

            }
            order.ID = 129302830;
            //empty cart
            Session["Cart"] = new Order();

            return View(order);
        }

    }
}
