using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class TransitPassController : Controller
    {
        // GET: TransitPass/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: TransitPass
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransitPass/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransitPass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransitPass/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransitPass/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransitPass/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransitPass/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransitPass/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
