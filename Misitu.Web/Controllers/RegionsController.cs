using Misitu.Regions;
using Misitu.Regions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class RegionsController : MisituControllerBase
    {
        private readonly IRegionAppService _regionAppService;

        public RegionsController(IRegionAppService zoneAppService)
        {
            _regionAppService = zoneAppService;
        }

        public ActionResult Index()
        {
            var Regions = _regionAppService.GetRegions();
            return View(Regions);
        }

        // GET: Zone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zone/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateRegionInput input)
        {

            // TODO: Add insert logic here

            if (ModelState.IsValid)
            {
                await _regionAppService.CreateRegion(input);
                return RedirectToAction("Index");

            }
            else
            {
                return View(input);
            }

        }


        // GET: Zone/Edit/5
        public ActionResult Edit(int id)
        {
            var zone = _regionAppService.GetRegion(id);
            return View(zone);
        }

        // POST: Zone/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RegionDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    await _regionAppService.UpdateRegion(input);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(input);
                }

            }
            catch
            {
                return View(input);
            }
        }

        // GET: Zone/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var zone = _regionAppService.GetRegion(id);
            await _regionAppService.DeleteRegionAsync(zone);

            return RedirectToAction("Index");
        }

    }
}
