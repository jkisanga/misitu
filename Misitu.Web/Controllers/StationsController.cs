using Abp.Runtime.Validation;
using Misitu.Regions;
using Misitu.Stations;
using Misitu.Stations.Dto;
using Misitu.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class StationsController : MisituControllerBase
    {
        private readonly IRegionAppService _regionAppService;
        private readonly IZoneAppService _zoneAppService;
        private readonly IStationAppService _stationAppService;


        public StationsController(
            IStationAppService stationAppService,
            IRegionAppService regionAppService, 
            IZoneAppService zoneAppService)
        {
            _stationAppService = stationAppService;
            _regionAppService = regionAppService;
            _zoneAppService = zoneAppService;

        }
        // GET: Stations
        public ActionResult Index()
        {
            var stations = _stationAppService.GetStations();
            return View(stations);

        }

        // GET: Stations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stations/Create
        public ActionResult Create()
        {
            var regions = _regionAppService.GetRegions().Select(c => new SelectListItem {Value = c.Id.ToString(), Text = c.Name });
            var zones = _zoneAppService.GetZones().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.RegionId = regions;
            ViewBag.ZoneId = zones;
            return View();
        }

        // POST: Stations/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateStationInput input)
        {
           
                if (ModelState.IsValid)
                {
                    await _stationAppService.CreateStation(input);
                    return RedirectToAction("Index");

                }
                else
                {
                    var regions = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    var zones = _zoneAppService.GetZones().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    ViewBag.RegionId = regions;
                    ViewBag.ZoneId = zones;
                    return View(input);
                }
            }
           
        

        // GET: Stations/Edit/5
        public ActionResult Edit(int id)
        {
            var station = _stationAppService.GetStation(id);
            var regions = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            var zones = _zoneAppService.GetZones().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.RegionId = regions;
            ViewBag.ZoneId = zones;
            return View(station);
        }

        // POST: Stations/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, StationDto input)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {

                await _stationAppService.UpdateStation(input);
                return RedirectToAction("Index");

            }
            else
            {
                var regions = _regionAppService.GetRegions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                var zones = _zoneAppService.GetZones().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.RegionId = regions;
                ViewBag.ZoneId = zones;
                return View(input);
            }
        }

        // GET: Stations/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var station = _stationAppService.GetStation(id);
            await _stationAppService.DeleteStationAsync(station);
            return RedirectToAction("Index");
        }

       
    }
}
