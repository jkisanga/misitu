using Abp.Runtime.Validation;
using Misitu.FinancialYears;
using Misitu.PlotScalling;
using Misitu.PlotScalling.Dto;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.PlotScalling
{
    [DisableValidation]

    public class HarvestingPlanController : MisituControllerBase
    {

        private readonly IUserAppService _userAppService;
        private readonly IHarvestingPlanAppService _harvestingPlanAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IStationAppService _stationAppService;

        public HarvestingPlanController(
            IUserAppService userAppService,
            IHarvestingPlanAppService harvestingPlanAppService,
            IFinancialYearAppService financialYearAppService,
            IStationAppService stationAppService
            )
        {
            _userAppService = userAppService;
            _harvestingPlanAppService = harvestingPlanAppService;
            _financialYearAppService = financialYearAppService;
            _stationAppService = stationAppService;
        }

        // GET: HarvestingPlan
        public ActionResult Index()
        {          

            var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.StationId = stations;


            return View();
        }


        // GET: HarvestingPlan/Details/5
        public ActionResult Lists()
        {
            var fYaer = _financialYearAppService.GetActiveFinancialYear();
            var harvestingPlans = _harvestingPlanAppService.GetHarvestingPlans(fYaer);

            return PartialView("_Lists", harvestingPlans);
        }

        // GET: HarvestingPlan/Details/5

        public ActionResult Station()
        {
            var userInfo = _userAppService.GetLoggedInUser();
            var fYear = _financialYearAppService.GetActiveFinancialYear();
            var harvestingPlan = _harvestingPlanAppService.GetHarvestingPlanByStation(fYear,userInfo.StationId);
            return PartialView("_station",harvestingPlan);
        }

        // POST: HarvestingPlan/Create
        [HttpPost]
        public ActionResult Create(CreateHarvestingPlanInput input, HttpPostedFileBase path)
        {

            CheckModelState();

            try
            {

                if (path.ContentLength  > 0)
                {
                    var FileName = path.FileName;                    
                    var FilePath = Path.Combine(Server.MapPath("~/App_Data/Documents"), FileName);

                    input.Path = FilePath;

                    _harvestingPlanAppService.CreateHarvestingPlan(input);

                    path.SaveAs(FilePath);
             
                }
                else
                {
                    ModelState.AddModelError("", "Please attach require paths");
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: HarvestingPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        public async Task<ActionResult> Delete(int id)
        {
            var plan = _harvestingPlanAppService.GetHarvestingPlan(id);
            await _harvestingPlanAppService.DeleteHarvestingPlanAsync(plan);

            return RedirectToAction("Index");
        }

        public FileResult Download(int id)
        {
            var plan = _harvestingPlanAppService.GetHarvestingPlan(id);

            return File(plan.Path, "application / force - download", Path.GetFileName(plan.Path));
        }
    }
}
