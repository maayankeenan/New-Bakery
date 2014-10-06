using Repository.DAL;
using Repository.Enum;
using Repository.Models;
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

            ViewBag.Message = "Start your order right here";

            //var model = getProducts();
            var model = Repository.DAL.PastryDAL.GetAllPastries();

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
            List<Pastery> pasteries = new List<Pastery>();
            pasteries.Add(new Pastery() { Name = "Choclate Cake", Type = PastryType.Cakes, GlotanFree = false, Vegan = false, Comments = "Very Delicious", Price = 9.99, ImageLink = "http://www.sparkyhub.com/wp-content/uploads/2012/02/30+delicious-chocolate-cake-pictures-you-love-4.jpg?0c9e3b" });
            pasteries.Add(new Pastery() { Name = "Choclate Strewberry Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Comments = "Fresh Strewberry", Price = 5.49, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" });
            pasteries.Add(new Pastery() { Name = "Rye Bread", Type = PastryType.Breads, GlotanFree = false, Vegan = false, Price = 14.90, ImageLink = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcQDtD8t2IEb5veW4YmdjByl5NXU3WEyMlyrP01IU0MNrDgxnklL" });
            pasteries.Add(new Pastery() { Name = "Onion Bagel", Type = PastryType.Bagels, GlotanFree = false, Vegan = false, Comments = "Dry onion", Price = 3.49, ImageLink = "http://featherstonefoods.com/wp-content/uploads/1108.jpg" });
            pasteries.Add(new Pastery() { Name = "Red Velvet Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Comments = "Our signture cupcake", Price = 4.99, ImageLink = "http://images4.fanpop.com/image/photos/15400000/red-velvet-cupcakes-red-velvet-cupcakes-15404907-460-562.jpg" });
            pasteries.Add(new Pastery() { Name = "Apple Pie", Type = PastryType.Cakes, GlotanFree = true, Vegan = false, Comments = "The traditional taste", Price = 24.90, ImageLink = "http://img2.wikia.nocookie.net/__cb20130114141601/shipoffools/images/f/f7/Apple-pie.jpg" });
            pasteries.Add(new Pastery() { Name = "Oreo Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Price = 4.99, ImageLink = "http://1.bp.blogspot.com/-v304FcYagNk/TbQgccwSHzI/AAAAAAAAAFw/nrd0qURLr-I/s1600/oreo+cupcake.jpg" });
            pasteries.Add(new Pastery() { Name = "Seaseme Bagel", Type = PastryType.Bagels, GlotanFree = false, Vegan = false, Price = 3.99, ImageLink = "http://www.gourmetmeatman.com/siteimages/sesame%20bagel.jpg" });
            pasteries.Add(new Pastery() { Name = "Olive Bread", Type = PastryType.Breads, GlotanFree = false, Vegan = false, Comments = "Calamate Olieves", Price = 19.99, ImageLink = "http://www.acebakery.com/storage/products/OliveBoule.jpg" });
            pasteries.Add(new Pastery() { Name = "Carrot Cake", Type = PastryType.Cakes, GlotanFree = true, Vegan = true, Price = 39.99, ImageLink = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcS6csPHFf1EpBTZ7wvXuRN35ejVeLfeEA0oqPbvP9lkv8Byt1-J" });
            pasteries.Add(new Pastery() { Name = "Vanilla Cupcake", Type = PastryType.Cupcakes, GlotanFree = false, Vegan = false, Price = 4.99, ImageLink = "http://images.picturesdepot.com/photo/v/vanilla_cupcake-210820.jpg" });
            pasteries.Add(new Pastery() { Name = "Whole Wheat Bread", Type = PastryType.Breads, GlotanFree = false, Vegan = false, Comments = "Very nutritious", Price = 29.99, ImageLink = "http://www.womansday.com/cm/womansday/images/IQ/What-to-Look-for-When-Buying-Bread-mdn.jpg" });

            return pasteries;
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
            var model = Repository.DAL.PastryDAL.SelectByCriteria(null, name, (PastryType)Enum.Parse(typeof(PastryType), type), parsedPrice , null, vegan, glotanFree);
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
            if ((cust != null) && (cust.Password == password))
            {
                this.loginedUser = cust;
                return RedirectToAction("Index");
            }
            //if (id == "admin")
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
            //else if (id == "abc")
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

            return RedirectToAction("Index");
        }
    }
}
