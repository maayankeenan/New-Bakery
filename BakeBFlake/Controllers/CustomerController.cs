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

        public ActionResult Index(string id = null, string firstName = null, string lastName = null, string address = null)
        {
            ViewBag.Message = "Customers List";

            var model = Repository.DAL.CustomerDAL.SelectByCriteria(id, firstName, lastName, address);
            //var model = getUsers();

            return View(model);
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

            Repository.DAL.CustomerDAL.UpdateCustomer(customer);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            //var user1 = new Customer();
            //user1.ID = "123";
            //user1.Name = "Ido";
            //user1.LastName = "Ganzer";
            //user1.Address = "Bla bla 30";
            //user1.Phone = "5543253";
            //user1.Password = "abcde";
            //user1.Prefered = true;
            //user1.IsAdmin = true;

            //var model = user1;
            var model = Repository.DAL.CustomerDAL.GetCustomer(id);

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            Repository.DAL.CustomerDAL.DeleteCustomer(id);
            return RedirectToAction("Index");
        }

    }
}
