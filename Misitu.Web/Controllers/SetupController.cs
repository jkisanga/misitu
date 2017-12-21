using Misitu.RefTables.Dto;
using Misitu.RefTables.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class SetupController : Controller
    {
        private readonly IRefServiceCategoryAppService refServiceCategityAppService;
        private readonly IRefIdentityAppService refIdentityAppService;
        private readonly IRefApplicationTypeAppService refApplicationTypeAppService;

        public SetupController(IRefServiceCategoryAppService refServiceCategityAppService, IRefIdentityAppService refIdentityAppService, IRefApplicationTypeAppService refApplicationTypeAppService)
        {
            this.refServiceCategityAppService = refServiceCategityAppService;
            this.refIdentityAppService = refIdentityAppService;
            this.refApplicationTypeAppService = refApplicationTypeAppService;
        }



        // GET: Setup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Setup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Setup/Create
        public ActionResult CreateServiceCategory()
        {
            ViewBag.values = this.refServiceCategityAppService.GetItemList();

            return View();
        }

        // POST: Setup/Create
        [HttpPost]
        public async Task<ActionResult> CreateServiceCategory(CreateRefServiceCategoryInput input)
        {
            await this.refServiceCategityAppService.CreateAsync(input);

            return View();
        }

        // GET: Setup/Edit/5
        public ActionResult EditserviceCategory(int id)
        {

            var value = this.refServiceCategityAppService.GetObjectById(id);
            return View(value);
        }

        // POST: Setup/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RefServiceCategoryDto collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    await this.refServiceCategityAppService.UpdateObject(collection);
                    return RedirectToAction("CreateServiceCategory");
                }
                else
                {
                    return View(collection);
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Setup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Setup/Delete/5
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
