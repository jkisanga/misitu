using Misitu.FinancialYears;
using Misitu.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
  
    public class LicenseController : MisituControllerBase
    {

        private readonly LicenseAppService _licenseAppService;
        private readonly IFinancialYearAppService _financialYearAppService;

        public LicenseController(
                  IFinancialYearAppService financialYearAppService,
                  LicenseAppService licenseAppService
                  )
        {

            _financialYearAppService = financialYearAppService;
            _licenseAppService = licenseAppService;
        }

        // GET:  License/Dashboard

        public ActionResult Dashboard()
        {
            return View();
        }
        // GET: License
        public ActionResult Index()
        {
            var Fyear = _financialYearAppService.GetActiveFinancialYear();
            var Licenses = _licenseAppService.GetLicenses(Fyear);
            return View(Licenses);
        }

        // GET: License
        public ActionResult Pending()
        {
            var Fyear = _financialYearAppService.GetActiveFinancialYear();
            var Licenses = _licenseAppService.GetPendingLicenses(Fyear);
            return View(Licenses);
        }

        // GET: License/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: License/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: License/Create
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

        // GET: License/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: License/Edit/5
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

        // GET: License/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: License/Delete/5
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
