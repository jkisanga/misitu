using Misitu.Zones;
using Misitu.Zones.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class ZonesController : MisituControllerBase
    {
        private readonly IZoneAppService _zoneAppService;

        public ZonesController(IZoneAppService zoneAppService)
        {
            _zoneAppService = zoneAppService;
        }

        public ActionResult Index()
        {
            var zones = _zoneAppService.GetZones();
            return View(zones);
        }
     
        // GET: Zone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zone/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateZoneInput input)
        {
                      
                // TODO: Add insert logic here
               
                if (ModelState.IsValid)
                {
                    await _zoneAppService.CreateZone(input);
                    return RedirectToAction("Index");

                }else
                {
                    return View(input);
                }           
   
        }
       

        // GET: Zone/Edit/5
        public ActionResult Edit(int id)
        {
            var zone = _zoneAppService.GetZone(id);
            return View(zone);
        }

        // POST: Zone/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id,  ZoneDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    
                    await _zoneAppService.UpdateZone(input);
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
            var zone = _zoneAppService.GetZone(id);
            await _zoneAppService.DeleteZoneAsync(zone);

            return RedirectToAction("Index");
        }

     
    }
}
