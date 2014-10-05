﻿using Repository.DAL;
using Repository.Enum;
using Repository.Models;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace BakeBFlake.Controllers
{
    public class HomeController : Controller
    {
        public Customer loginedUser
        {
            get
            {
                return Session["LoginUser"] as Customer;
            }
            set
            {
                Session["LoginUser"] = value;
            }
        }

        public ActionResult Index()
        {
            //using (PastryContext context = new PastryContext())
            //{
            //    //context.Database.Connection.Open();

            //    var Pastries = new List<Pastery>
            //    {
            //        new Pastery { ID = 0,   Name = "בורקס חלומי", 
            //            Type = PastryType.Bagels ,Vegan=false, GlotanFree=false  },
            //        new Pastery { ID = 1,   Name = "לחם אלוהי",
            //            Type = PastryType.Breads ,Vegan=false, GlotanFree=false  },
            //        new Pastery { ID = 2,   Name = "בורקס אלוהי",
            //            Type = PastryType.Breads ,Vegan=false, GlotanFree=false  },
                
            //    };

            //    Pastries.ForEach(s => context.Pastries.AddOrUpdate(p => p.ID, s));
            //    context.SaveChanges();

            //    var Customers = new List<Customer>
            //    {
            //        new Customer {ID="305631491", Name="Ran", LastName="Asulin", Address="Amishav 66 Tel Aviv", Phone="050-3233758", Prefered=true, Password="123", IsAdmin=false},
            //        new Customer {ID="305631490", Name="May", LastName="Asulin", Address="Amishav 66 Tel Aviv", Phone="050-3233758", Prefered=true, Password="123", IsAdmin=true},
            //    };

            //    Customers.ForEach(s => context.Customers.AddOrUpdate(p => p.ID, s));
            //    context.SaveChanges();

            //    var Orders = new List<Order>
            //    {
            //        new Order { ID=1, OrderDate=DateTime.Today.Date, DelieveryDate=DateTime.Today.Date.AddDays(1), Status=OrderStatus.Accepted ,CustomerID="305631491"},
            //    };

            //    Orders.ForEach(s => context.Orders.AddOrUpdate(p => p.ID, s));
            //    context.SaveChanges();




            //    var OrdersDetailes = new List<OrderDetails>
            //    {
            //        new OrderDetails {ID=0, OrderID=1, PasteryId= 2, TotalAmount=2},
            //    };

            //    OrdersDetailes.ForEach(s => context.OrderDetailes.AddOrUpdate(p => p.ID, s));
            //    context.SaveChanges();

            //    var Branches = new List<Branch>
            //    {
            //        new Branch {ID=0, Name="Ran's Pastry", X=5.0, Y=3.0},
            //        new Branch {ID=1, Name="Rani's Pastry", X=5.0, Y=3.0},
            //    };

            //    Branches.ForEach(s => context.Branches.AddOrUpdate(p => p.ID, s));
            //    context.SaveChanges();
            //}


            ViewBag.Message = "Start your order right here";

            //var model = getProducts();
            var model = Repository.DAL.PastryDAL.GetAllPastries();

            OrderdDAL.MostOrderedPastery();
            //PastryDAL.AddNewPastry(new Pastery("Vanilla Cupcakes", "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg", PastryType.Cupcakes, 9, string.Empty, false, false));
            //CustomerDAL.AddNewCustomer(new Customer("987654321", "Maayan", "Keenan", "66 Amishav Street Tel Aviv", "050-3233758", true,"123", true));
            //OrderdDAL.AddNewOrder(new Order(DateTime.Today, DateTime.Today.AddDays(5),0,string.Empty, OrderStatus.Accepted,"123456789"));


            //Init cart
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new Order(){OrderDate = new DateTime(), OrderDetails = new System.Collections.Generic.List<OrderDetails>()};
                Session["ItemsInCart"] = 0;
            }
            if (Session["IsAdmin"] == null)
            {
                Session["IsAdmin"] = false;
            }

            return View(model);
        }

        private System.Collections.Generic.List<Pastery> getProducts()
        {
            var model = new System.Collections.Generic.List<Pastery>();

            model.Add(new Pastery() { ID = 1, Name = "Whole Wheat Bread", Price = 9.99, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });
            model.Add(new Pastery() { ID = 2, Name = "Rye Bread", Price = 9.99, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });
            model.Add(new Pastery() { ID = 3, Name = "Vanilla Cupcakes", Price = 9.99, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });
            model.Add(new Pastery() { ID = 4, Name = "Choclate Cake", Price = 9.99, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });
            model.Add(new Pastery() { ID = 5, Name = "Onion Bagel", Price = 9.99, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });

            return model;
        }

        public PartialViewResult Search(string name, string type, string price, bool? glotanFree, bool? vegan)
        {
            //var model = new System.Collections.Generic.List<Pastery>();
            //if (name != null)
            //{
            //    model.Add(new Pastery() { ID = 1, Name = "Whole Wheat Bread", Price = 9.99 });
            //    model.Add(new Pastery() { ID = 2, Name = "Rye Bread", Price = 9.99 });
            //}
            //else
            //{
            //    model.Add(new Pastery() { ID = 1, Name = "Whole Wheat Bread", Price = 9.99 });
            //    model.Add(new Pastery() { ID = 2, Name = "Rye Bread", Price = 9.99 });
            //    model.Add(new Pastery() { ID = 3, Name = "Vanilla Cupcakes", Price = 9.99 });
            //    model.Add(new Pastery() { ID = 4, Name = "Choclate Cake", Price = 9.99 });
            //    model.Add(new Pastery() { ID = 5, Name = "Onion Bagel", Price = 9.99 });
            //}

            double? parsedPrice = null;
            if (price != null)
            {
                parsedPrice = Double.Parse(price);
            }
            var model = Repository.DAL.PastryDAL.SelectByCriteria(null, name, (PastryType)Enum.Parse(typeof(PastryType), type),parsedPrice , price, vegan, glotanFree);
            return PartialView(model);
        }

        public Boolean AddToCart(Int32 pasteryId, string pasteryName, string price)
        {
            Order cart = (Order)Session["Cart"];
            
            OrderDetails cartItem = cart.OrderDetails.Find(x => x.PasteryId == pasteryId);
            if (cartItem != null)
            {
                ++cartItem.TotalAmount;
            }
            else
            {
                cartItem = new OrderDetails() { PasteryId = pasteryId, OrderID = cart.ID, TotalAmount = 1, Pastery = new Pastery() { ID=pasteryId, Name=pasteryName, Price=Double.Parse(price)} };
                cart.OrderDetails.Add(cartItem);
            }
            Session["ItemsInCart"] = (int)Session["ItemsInCart"] + 1;
            return true;
        }

        public ActionResult About()
        {
            var branches = Repository.DAL.BranchDAL.allBranches();
            //var branches = new List<Branch>();

            //var branch1 = new Branch();
            //branch1.Name = "Ramat gan - Zabutinsky branch";
            //branch1.Phone = "03-845739";
            //branch1.X = 32.084609;
            //branch1.Y = 34.809756;
            //branches.Add(branch1);

            //var branch2 = new Branch();
            //branch2.Name = "Hamedina square branch";
            //branch2.Phone = "03-842579";
            //branch2.X = 32.085955;
            //branch2.Y = 34.788577;
            //branches.Add(branch2);

            //var branch3 = new Branch();
            //branch3.Name = "Rabin square branch";
            //branch3.Phone = "03-3643679";
            //branch3.X = 32.080555;
            //branch3.Y = 34.781432;
            //branches.Add(branch3);

            var images = getInstagramImages(); 
            ViewBag.instagramImages = images;

            return View(branches);
        }

        private List<string> getInstagramImages()
        {
            var images = new List<string>();
            var request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/1517817377/media/recent/?access_token=1517817377.a143fe1.6b09b5e4b72b458a99e556696e5d445a");

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                                dynamic json = System.Web.Helpers.Json.Decode(responseValue);
                                foreach (var item in json.data)
                                {
                                    images.Add(item.images.thumbnail.url);
                                }
                            }
                    }
                }
            }

            return images;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            Repository.DAL.CustomerDAL.AddNewCustomer(customer);
            this.loginedUser = customer;

            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            loginedUser = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(string id, string password)
        {
            var cust = Repository.DAL.CustomerDAL.GetCustomer(id);
            if (cust.Password == password)
            {
                return RedirectToAction("Index");
            }
            //if(username == "admin")
            //{
            //    var user1 = new Customer();
            //    user1.ID = "999";
            //    user1.Name = "admin";
            //    user1.LastName = "hamelech";
            //    user1.Address = "Bla bla 56";
            //    user1.Phone = "5547853";
            //    user1.Password = "123";
            //    user1.Prefered = true;
            //    user1.IsAdmin = true;

            //    this.loginedUser = user1;
            //    Session["IsAdmin"] = true;
            //}
            //else if (username == "abc")
            //{
            //    var user1 = new Customer();
            //    user1.ID = "999";
            //    user1.Name = "abc";
            //    user1.LastName = "tipesh";
            //    user1.Address = "Bla bla 56";
            //    user1.Phone = "5547853";
            //    user1.Password = "321";
            //    user1.Prefered = false;
            //    user1.IsAdmin = false;

            //    this.loginedUser = user1;
            //}
            else
            {
                ViewBag.Message = "Access denied, Bad Id or password";
                return View("Login");
            }

            //return RedirectToAction("Index");
        }
    }
}
