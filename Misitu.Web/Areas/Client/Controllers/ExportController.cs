using Abp.Runtime.Validation;
using Misitu.Applicants.Dto.ExportImport;
using Misitu.Applicants.Interface;
using Misitu.FinancialYears;
using Misitu.Registration;
using Misitu.Species;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Areas.Client.Controllers
{

    
    [DisableValidation]
    public class ExportController : Controller
    {
        private readonly IExportService _exportService;
        private readonly ISpecieCategoryAppService _specieCategoryAppService;
        private readonly ISpecieAppService _specieAppService;
        private readonly IStationAppService _stationAppService;
        private readonly IApplicantService _applicantService;
        private readonly IUserAppService _userAppService;
        private readonly IDealerAppService _dealerAppService;
        private readonly IFinancialYearAppService _financialYearAppService;


        public ExportController(
            IExportService exportService,
            ISpecieCategoryAppService specieCategoryAppService,
            ISpecieAppService specieAppService,
            IStationAppService stationAppService,
            IApplicantService applicantService,
            IUserAppService userAppService,
            IDealerAppService dealerAppService,
            IFinancialYearAppService financialYearAppService
            )
        {
            _exportService = exportService;
            _specieCategoryAppService = specieCategoryAppService;
            _specieAppService = specieAppService;
            _stationAppService = stationAppService;
            _applicantService = applicantService;
            _userAppService = userAppService;
            _dealerAppService = dealerAppService;
            _financialYearAppService = financialYearAppService;
        }

        // GET: Client/Export

        public ActionResult Index()
        {
            var permits  = _exportService.getExportsByApplicantId(_userAppService.GetLoggedInUser().ApplicantId);
            return View(permits);
        }

        // GET: Client/Export/Details/5

        public ActionResult Details(int id)
        {
            var permit = _exportService.getExportDetailById(id);
            ViewBag.Species = _exportService.getExportSpeciesByExportDetailId(id);
            return View(permit);
        }

        // GET: Client/Export/Create

        public ActionResult Create()
        {
            if (_dealerAppService.IsRegistered(_userAppService.GetLoggedInUser().ApplicantId,_financialYearAppService.GetActiveFinancialYear()))
            {
                ViewBag.Applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
                ViewBag.StationId = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.SpecieCategoryId = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                return View();
            }else
            {
                TempData["danger"] = string.Format(@"You must have registrartion certificate first, apply for registration!");
                return RedirectToAction("Index", "Dashboard");
            }
        }

        // POST: Client/Export/Create

        [HttpPost]
        public ActionResult Create(CreateExportDetail Input)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    int exportDetailId = _exportService.CreateAndReturnId(Input);
                    return RedirectToAction("addSpecie",new { Id = exportDetailId});
                }else
                {
                    ViewBag.Applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
                    ViewBag.StationId = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    ViewBag.SpecieCategoryId = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    return View(Input);
                }               
            }
            catch
            {
                ViewBag.Applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
                ViewBag.StationId = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.SpecieCategoryId = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                return View();
            }
        }

        // GET: Client/Export/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Export/Edit/5

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

        // GET: Client/Export/addSpecie/5

        public ActionResult addSpecie(int id)
        {
            ViewBag.ExportDetail = _exportService.getExportDetailById(id);
            ViewBag.Species = _exportService.getExportSpeciesByExportDetailId(id);
            ViewBag.SpecieId = _specieAppService.GetSpecies().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.EnglishName});
            return View();
        }

        // POST: Client/Export/addSpecie/5
        [HttpPost]
        public ActionResult addSpecie(CreateExportSpecie input)
        {
            try
            {
                // TODO: Add  logic 

                if (ModelState.IsValid)
                {
                    _exportService.AddSpecies(input);

                    ViewBag.ExportDetail = _exportService.getExportDetailById(input.ExportDetailId);
                    ViewBag.SpecieId = _specieAppService.GetSpecies().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.EnglishName });

                    ModelState.AddModelError("Success", "Specie Added successfully!");
                    return View();
                }
                else
                {
                    ViewBag.ExportDetail = _exportService.getExportDetailById(input.ExportDetailId);
                    ViewBag.SpecieId = _specieAppService.GetSpecies().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.EnglishName });
                    return View(input);
                }
               
            }
            catch
            {
                ViewBag.ExportDetail = _exportService.getExportDetailById(input.ExportDetailId);
                ViewBag.SpecieId = _specieAppService.GetSpecies().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.EnglishName });
                return View(input);
            }
        }

        // GET: Client/Export/addAttachments/5
        public ActionResult addAttachments(int id)
        {
            ViewBag.ExportDetail = _exportService.getExportDetailById(id);
            return View();
        }

       [HttpPost]
       public ActionResult addAttachments(
		    HttpPostedFileBase BrelaRegistrationCert,
		    HttpPostedFileBase LicenceCert,
			HttpPostedFileBase TaxClearanceCert,
			HttpPostedFileBase EnquiryOrder,
			HttpPostedFileBase ExportReturns,
			HttpPostedFileBase ForestProduceRegCert,
			HttpPostedFileBase AutholizedLetter,
			HttpPostedFileBase SawMillerContract,
            HttpPostedFileBase MouCert,
            int ExportDetailId
           )
        {
            try
            {
                var attachments = new CreateExportAttachment();

                attachments.ExportDetailId = ExportDetailId;

                if (BrelaRegistrationCert != null && BrelaRegistrationCert.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(BrelaRegistrationCert.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.BrelaRegistrationCert = _savedpath;
                    BrelaRegistrationCert.SaveAs(_path);
                }
                if (LicenceCert != null && LicenceCert.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(LicenceCert.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.LicenceCert = _savedpath;
                    LicenceCert.SaveAs(_path);
                }
                if (TaxClearanceCert != null && TaxClearanceCert.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(TaxClearanceCert.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.TaxClearanceCert = _savedpath;
                    TaxClearanceCert.SaveAs(_path);
                }
                if (EnquiryOrder != null && EnquiryOrder.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(EnquiryOrder.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.EnquiryOrder = _savedpath;
                    EnquiryOrder.SaveAs(_path);
                }
                if (ExportReturns != null && ExportReturns.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(ExportReturns.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.ExportReturns = _savedpath;
                    ExportReturns.SaveAs(_path);
                }
                if (ForestProduceRegCert != null && ForestProduceRegCert.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(ForestProduceRegCert.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.ForestProduceRegCert = _savedpath;
                    ForestProduceRegCert.SaveAs(_path);
                }
                if (AutholizedLetter != null && AutholizedLetter.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(AutholizedLetter.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.AutholizedLetter = _savedpath;
                    AutholizedLetter.SaveAs(_path);
                }
                if (SawMillerContract != null && SawMillerContract.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(SawMillerContract.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.SawMillerContract = _savedpath;
                    SawMillerContract.SaveAs(_path);
                }
                if (MouCert != null && MouCert.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(MouCert.FileName);
                    string _savedpath = Guid.NewGuid() + "-" + _FileName;
                    string _path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), _savedpath);
                    attachments.MouCert = _savedpath;
                    MouCert.SaveAs(_path);
                }
                _exportService.AddAttachments(attachments);
                ModelState.AddModelError("Success", "Files Uploaded Successfully!!");
                return RedirectToAction("Index","Dashboard");
            }
            catch(Exception ex)
            {
                ViewBag.ExportDetail = _exportService.getExportDetailById(ExportDetailId);
                ModelState.AddModelError("danger", ex.Message);
                return View();
            }
        }
    }
}
