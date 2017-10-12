using Abp.Runtime.Validation;
using Misitu.FinancialYears;
using Misitu.Layout.Dto;
using Misitu.Registration;
using Misitu.Registration.Dto;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Misitu.Web.Controllers.Registration
{
    [DisableValidation]
    public class DealersController : Controller
    {

        private readonly IDealerAppService _dealerAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IStationAppService _stationAppService;
        private readonly ICandidateAppService _candidateAppService;
        private readonly IUserAppService _userAppService;

        public DealersController(
            IDealerAppService dealerAppService,
              IUserAppService userAppService,
            IStationAppService stationAppService, 
            FinancialYearAppService financialYEarAppService,
            ICandidateAppService candidateAppService
            )
        {
            _dealerAppService = dealerAppService;
            _financialYearAppService = financialYEarAppService;
            _stationAppService = stationAppService;
            _candidateAppService = candidateAppService;
            _userAppService = userAppService;
        }

        public ActionResult Dashboard()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var userInfo = _userAppService.GetLoggedInUser();

            var Dashboard = new RegistrationDashboard
            {
                candidates = _candidateAppService.GetTotalCandidatesByStationId(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                pending = _dealerAppService.GetTotalPendingDealerByStationId(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                dealers = _dealerAppService.GetTotalDealerByStationId(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                estimatedVolume = _candidateAppService.GetTotalAppliedVolumeByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                TotalCollection = _dealerAppService.GetTotalDealerFeesByStation(_stationAppService.GetStation(userInfo.StationId),finacialYear).Sum(),
                dealersPerMonth = _dealerAppService.GetTotalMonthDealerByStationId(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                pendingPerMonth = _dealerAppService.GetTotalMonthPendingDealerByStationId(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                CollectionPerMonth = _dealerAppService.GetTotalMonthDealerFeesByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear).Sum()
            };
            
            return View(Dashboard);
        }
        // GET: Dealers

        public ActionResult Index()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var dealers = _dealerAppService.GetDealers(finacialYear);
            return View(dealers);
        }

        // GET:Registered/ Dealers

        public ActionResult Registered()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var dealers = _dealerAppService.GetRegisteredDealers(finacialYear);
            return View(dealers);
        }

        // GET: Dealers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dealers/Create
        public ActionResult Create()
        {
            var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.StationId = stations;
            return View();
        }

        // POST: Dealers/Create
        [HttpPost]
        public  ActionResult Create(FormCollection form, CreateDealerInput input, HttpPostedFileBase tin_file, HttpPostedFileBase bl_file)
        {
            if (ModelState.IsValid)
            {
                if (tin_file != null && bl_file != null)
                {
                    if (tin_file.ContentLength > 0 && bl_file.ContentLength > 0)
                    {
                        var tin_fileName = input.TIN;
                        var bl_fileName = input.BusinessLicense;

                        var tin_path = Path.Combine(Server.MapPath("~/App_Data/Documents"), tin_fileName);
                        var bl_path = Path.Combine(Server.MapPath("~/App_Data/Documents"), bl_fileName);

                        tin_file.SaveAs(tin_path);
                        bl_file.SaveAs(bl_path);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please attach require files");
                    }
                }

                    var user = _userAppService.GetLoggedInUser();
                    input.StationId = user.StationId;//get user logged in station
                    int Id = _dealerAppService.CreateDealer(input);

                //update selected candidate if exists

                if (form.AllKeys.Contains("CandidateId") && !string.IsNullOrEmpty(form.Get("CandidateId").ToString()))
                {
                    var candidate = _candidateAppService.GetCandidate(Convert.ToInt32(form.Get("CandidateId")));
                    _candidateAppService.RegisterCandidate(candidate);
                }

                return RedirectToAction("Create","DealerActivities",new { id = Id });
               

            }
            else
            {
                var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.StationId = stations;
                return View();
            }
        }

        // GET: Dealers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dealers/Edit/5
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

        // GET: Dealers/download/5
     
        public ActionResult RegCertViewer(int id)
        {
            
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;

                 reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RegCert.rdlc";

                ReportParameter Id = new ReportParameter("Id", id.ToString());
                reportViewer.LocalReport.SetParameters(new ReportParameter[] { Id });
                reportViewer.LocalReport.DataSources.Clear();

              
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RegistrationCert", _dealerAppService.PrintDealer(id)));

                reportViewer.LocalReport.Refresh();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.Width = 1200;
                reportViewer.Height = 500;
                reportViewer.ShowPrintButton = true;
                reportViewer.ZoomMode = ZoomMode.FullPage;


            ViewBag.rptRegCert = reportViewer;

            return View();
           
        }
    }
}
