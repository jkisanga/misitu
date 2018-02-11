using System.Net;
using System.Web.Mvc;
using Misitu.Applicants;
using Misitu.FinancialYears;
using Misitu.Applicants.Dto;
using Misitu.RefTables.Interface;
using Abp.Runtime.Validation;
using Misitu.Applicants.ForestProduce;
using Misitu.Applicants.Interface;
using Misitu.Users;
using Misitu.Stations;
using System.Linq;
using Misitu.Species;
using Misitu.Registration;

namespace Misitu.Web.Areas.Client.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly IFinancialYearAppService financialYear;
        private readonly IApplicantService applicant;
        private readonly IRefApplicationTypeAppService refApplicationTypeAppService;
        private readonly IUserAppService _userAppService;
        private readonly IStationAppService _stationAppService;
        private readonly ISpecieCategoryAppService _specieCategoryAppService;
        private readonly IDealerAppService _dealerAppService;


        public ApplicationController(IFinancialYearAppService financialYear, IApplicantService applicant, 
            IRefApplicationTypeAppService refApplicationTypeAppService,
             IUserAppService userAppService,
             IDealerAppService dealerAppService,
             ISpecieCategoryAppService specieCategoryAppService,
            IStationAppService stationAppService
            )
        {
            this.financialYear = financialYear;
            this.applicant = applicant;
            this.refApplicationTypeAppService = refApplicationTypeAppService;
            _userAppService = userAppService;
            _specieCategoryAppService = specieCategoryAppService;
            _stationAppService = stationAppService;
            _dealerAppService = dealerAppService;

        }

        [HttpGet]
        public ActionResult ForestProduceRegistration()
        {
            if(_dealerAppService.IsRegistered(_userAppService.GetLoggedInUser().ApplicantId, this.financialYear.GetActiveFinancialYear()))
            {
                ViewBag.Applicant = this.applicant.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
                ViewBag.FinancialYearId = this.financialYear.GetActiveFinancialYear().Id;
                ViewBag.DistrictId = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
                return View();
            }else
            {
                TempData["danger"] = string.Format(@"You must have registrartion certificate first, apply for registration!");
                return RedirectToAction("Index", "Dashboard");
            }
           
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableValidation]
        public  ActionResult ForestProduceRegistration(CreateForestProduceRegistration input)
        {

            if (ModelState.IsValid)
            {
               var ForestProductRegistrationId = this.applicant.CreateForestProduceRegistrationAsync(input);

                return RedirectToAction("ForestProduceAppliedForest", "Application", new { Id = ForestProductRegistrationId });
            }
            ViewBag.Applicant = this.applicant.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
            ViewBag.FinancialYearId = this.financialYear.GetActiveFinancialYear().Id;
            ViewBag.DistrictId = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
            return View(input);
        }

        public ActionResult ForestProduceAppliedForest(int Id)
        {
            ViewBag.ForestProduceRegistration = this.applicant.GetForestProduceRegistrationById(Id);
            ViewBag.StationId = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.FinancialYearId = this.financialYear.GetActiveFinancialYear().Id;
            return View();
        }

        [HttpPost]
        [DisableValidation]
        [ValidateAntiForgeryToken]
        public ActionResult ForestProduceAppliedForest(CreateForestProduceAppliedForest input)
        {
            if (ModelState.IsValid)
            {
                var AppliedForestId = this.applicant.CreateForestProduceAppliedForestAsync(input);

                return RedirectToAction("ForestProduceAppliedSpecieCategory", "Application", new { Id = input.ForestProduceRegistrationId });
            }
            ViewBag.ForestProduceRegistration = this.applicant.GetForestProduceRegistrationById(input.ForestProduceRegistrationId);
            ViewBag.StationId = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.FinancialYearId = this.financialYear.GetActiveFinancialYear().Id;
            return View(input);
        }


        public ActionResult ForestProduceAppliedSpecieCategory(int Id)
        {
            ViewBag.ForestProduceRegistration = this.applicant.GetForestProduceRegistrationById(Id);
            ViewBag.FinancialYearId = this.financialYear.GetActiveFinancialYear().Id;
            ViewBag.SpecieCategoryId = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View();
        }

        [HttpPost]
        [DisableValidation]
        [ValidateAntiForgeryToken]
        public ActionResult ForestProduceAppliedSpecieCategory(CreateForestProduceAppliedSpecieCategory input)
        {
            if (ModelState.IsValid)
            {
                var ForestProductRegistrationId = this.applicant.CreateForestProduceAppliedSpecieCategory(input);

                return RedirectToAction("Index", "Dashboard");
            }
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.ForestProduceRegistration = this.applicant.GetForestProduceRegistrationById(input.ForestProduceRegistrationId);
            ViewBag.FinancialYearId = this.financialYear.GetActiveFinancialYear().Id;
            ViewBag.SpecieCategoryId = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            return View(input);
        }
        // company/ individual application
        public ActionResult myApplications()
        {
            var applications = this.applicant.GetForestProduceRegistrationByApplicantId(_userAppService.GetLoggedInUser().ApplicantId);
            return View(applications);
        }

        // application details
        public ActionResult ApplicationDetails(int Id)
        {
            var forestProduceReg = this.applicant.GetForestProduceRegistrationById(Id);
            return View(forestProduceReg);
        }
    }
}
