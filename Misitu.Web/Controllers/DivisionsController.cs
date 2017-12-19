using Abp.Runtime.Validation;
using Misitu.Divisions;
using Misitu.Divisions.Dto;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class DivisionsController : MisituControllerBase
    {

        private readonly IStationAppService _stationAppService;
        private readonly IDivisionAppService _divisionAppService;
     
        public DivisionsController(IStationAppService stationAppService, IDivisionAppService divisionAppService)
        {
            _stationAppService = stationAppService;
            _divisionAppService = divisionAppService;

        }
        // GET: Divisions
        public ActionResult Index()
        {
            var divisions = _divisionAppService.GetDivisions();
            return View(divisions);
        }

        // GET: Divisions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Divisions/Create
        public ActionResult Create()
        {
            var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.StationId = stations;
            return View();
        }

        // POST: Divisions/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateDivisionInput input)
        {
            if (ModelState.IsValid)
            {
                await _divisionAppService.CreateDivision(input);
                return RedirectToAction("Index");

            }
            else
            {
                var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.StationId = stations;
                return View();
            }
        }

        // GET: Divisions/Edit/5
        public ActionResult Edit(int id)
        {
            var division = _divisionAppService.GetDivision(id);
            var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.StationId = stations;
            return View(division);
        }

        // POST: Divisions/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, DivisionDto input)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {

                await _divisionAppService.UpdateDivision(input);
                return RedirectToAction("Index");

            }
            else
            {
                var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.StationId = stations;
                return View(input);
            }
        }

        // GET: Divisions/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var division = _divisionAppService.GetDivision(id);
            await _divisionAppService.DeleteDivisionAsync(division);
            return RedirectToAction("Index");
        }


    }
}
