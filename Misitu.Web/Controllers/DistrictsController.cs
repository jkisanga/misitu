using Abp.Runtime.Validation;
using Misitu.Districts;
using Misitu.Districts.Dto;
using Misitu.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class DistrictsController : Controller
    {
        private readonly IRegionAppService _regionAppService;
        private readonly IDistrictAppService _districtAppService;

        public DistrictsController(IRegionAppService regionAppService, IDistrictAppService districtAppService)
        {        
            _regionAppService = regionAppService;
            _districtAppService = districtAppService;
        }

        // GET: Districts
        public ActionResult Index()
        {
            var districts = _districtAppService.GetDistricts();
            return View(districts);

        }

        // GET: Districts/RegionId
        public ActionResult GetDistrictsByRegionId(int id)
        {
            var districts = _districtAppService.GetDistrictsByRegionId(id).Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        // GET: Districts/Create
        public ActionResult Create()
        {
            ViewBag.RegionId = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View();
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(CreateDistrictInput input)
        {
            if (ModelState.IsValid)
            {
                _districtAppService.CreateDistrict(input);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.RegionId = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                return View(input);
            }
        }

        // GET: District/Edit/5
        public ActionResult Edit(int id)
        {
            var district = _districtAppService.GetDistrict(id);
            ViewBag.RegionId = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View(district);
        }

        // POST: Stations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DistrictDto input)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                _districtAppService.UpdateDistrict(input);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.RegionId = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                return View(input);
            }
        }

        // GET: District/Delete/5
        public ActionResult Delete(int id)
        {
            var district = _districtAppService.GetDistrict(id);
             _districtAppService.DeleteDistrict(district);
            return RedirectToAction("Index");
        }
    }
}
