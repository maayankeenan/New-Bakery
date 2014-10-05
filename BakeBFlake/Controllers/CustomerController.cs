using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeBFlake.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index(string id, string firstName, string lastName, string address)
        {
            ViewBag.Message = "Customers List";

            return View(getUsers());
        }

        private List<Customer> getUsers()
        {
            var user1 = new Customer();
            user1.ID = "123";
            user1.Name = "Ido";
            user1.LastName = "Ganzer";
            user1.Address = "Bla bla 30";
            user1.Phone = "5543253";
            user1.Password = "abcde";
            user1.Prefered = true;
            user1.IsAdmin = true;

            var user2 = new Customer();
            user2.ID = "456";
            user2.Name = "Ido2";
            user2.LastName = "Ganzer2";
            user2.Address = "Bla bla 32";
            user2.Phone = "56785467";
            user2.Password = "fghjf";
            user2.Prefered = false;
            user1.IsAdmin = false;

            var users = new List<Customer>();
            users.Add(user1);
            users.Add(user2);

            return users;
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            ViewBag.Message = "Customers List";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var user1 = new Customer();
            user1.ID = "123";
            user1.Name = "Ido";
            user1.LastName = "Ganzer";
            user1.Address = "Bla bla 30";
            user1.Phone = "5543253";
            user1.Password = "abcde";
            user1.Prefered = true;
            user1.IsAdmin = true;

            return View(user1);
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

    }
}
