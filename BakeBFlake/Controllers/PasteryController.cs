using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeBFlake.Controllers
{
    public class PasteryController : Controller
    {

        public ActionResult View(int id)
        {
            // get item from repo by id
            var model = Repository.DAL.PastryDAL.GetPatry(id);
            //var model = new Pastery() { ID = 4, Name = "Choclate Cake", Price = 9.99, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" };
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            // get item from repo by id
            //var model = new Pastery() { ID = 4, Name = "Choclate Cake", Price = 9.99, Type = Repository.Enum.PastryType.Cupcakes, ImageLink = "http://www.localfranchiseopportunities.net/images/food-franchise-opportunities/food-franchise-opportunities-wyoming.jpg" };
            var model = Repository.DAL.PastryDAL.SelectByCriteria(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var model = new Pastery();
            try
            {
                TryUpdateModel(model, collection);
                int id = Repository.DAL.PastryDAL.AddNewPastry(model);
                return RedirectToAction("View", new { id = id });

            }
            catch (Exception e)
            {
                return RedirectToAction("Create");
            }
            
        }

        [HttpPost]
        public ActionResult Save(FormCollection collection)
        {
            var model = new Pastery();
            try
            {
                UpdateModel(model);
                Repository.DAL.PastryDAL.UpdatePastry(model);
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("View", new { id = model.ID });
        }

    }
}
