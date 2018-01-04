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

            return RedirectToAction("CreateServiceCategory");
        }

        public ActionResult EditserviceCategory(int id)
        {

            var value = this.refServiceCategityAppService.GetObjectById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> EditserviceCategory(int id, RefServiceCategoryDto collection)
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
       
        public async Task<ActionResult> Deleteservicecategory(int id)
        {
            try
            {
                // TODO: Add delete logic here

                var obj = this.refServiceCategityAppService.GetObjectById(id);
                await this.refServiceCategityAppService.DeleteObjectAsync(obj);
                return RedirectToAction("CreateServiceCategory");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult CreateIdentityType()
        {
            ViewBag.values = this.refIdentityAppService.GetItemList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateIdentityType(CreateRefIdentityInput input)
        {
            await this.refIdentityAppService.CreateAsync(input);

             return RedirectToAction("CreateIdentityType");
        }

        public ActionResult EditIdentity(int id)
        {

            var value = this.refIdentityAppService.GetObjectById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> EditIdentity(int id, RefIdentityDto collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    await this.refIdentityAppService.UpdateObject(collection);

                    return RedirectToAction("CreateIdentityType");
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

        public async Task<ActionResult> DeleteIdentity(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var obj = this.refIdentityAppService.GetObjectById(id);
                await this.refIdentityAppService.DeleteObjectAsync(obj);
                return RedirectToAction("CreateIdentityType");
            }
            catch
            {
                return RedirectToAction("CreateIdentityType");
            }
        }




        public ActionResult CreateApplicationType()
        {

            ViewBag.values = this.refApplicationTypeAppService.GetRefApplicationTypes();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateApplicationType(CreateRefApplicationInput input)
        {
            await this.refApplicationTypeAppService.CreateApplicationTypeAsync(input);

            return RedirectToAction("CreateApplicationType");
        }

        public ActionResult EditApplicationType(int id)
        {

            var value = this.refApplicationTypeAppService.GetApplicationTypeById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<ActionResult> EditApplicationType(int id, RefApplicationTypeDto collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    await this.refApplicationTypeAppService.UpdateApplicationType(collection);

                    return RedirectToAction("CreateApplicationType");
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

        public async Task<ActionResult> DeleteApplicationType(int id)
        {
            try
            {
                // TODO: Add delete logic here
                var obj = this.refApplicationTypeAppService.GetApplicationTypeById(id);
                await this.refApplicationTypeAppService.DeleteApplicationAsync(obj);
                return RedirectToAction("CreateApplicationType");
            }
            catch
            {
                return RedirectToAction("CreateApplicationType");
            }
        }
    }
}
