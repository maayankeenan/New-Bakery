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
            var model = Repository.DAL.PastryDAL.SelectByCriteria(id);
            //var model = new Pastery() { ID = 4, Name = "Choclate Cake", Price = 9.99};
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            // get item from repo by id
            //var model = new Pastery() { ID = 4, Name = "Choclate Cake", Price = 9.99 };
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
                Repository.DAL.PastryDAL.AddNewPastry(model);
            }
            catch (Exception e)
            {
            }
            // save item in repo
            
            return RedirectToAction("View", new { id = model.ID });
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
