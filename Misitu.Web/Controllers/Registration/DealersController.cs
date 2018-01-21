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
using Misitu.Billing.Dto;
using Misitu.Billing;

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
        private readonly IDealerActivityAppService _dealerActivityAppService;
        private readonly IBillAppService _billAppService;
        private readonly IBillItemAppService _billItemAppService;

        public DealersController(
            IDealerAppService dealerAppService,
              IUserAppService userAppService,
            IStationAppService stationAppService, 
            FinancialYearAppService financialYEarAppService,
            ICandidateAppService candidateAppService,
            IDealerActivityAppService dealerActivityAppService,
            IBillAppService billAppService,
            IBillItemAppService billItemAppService

            )
        {
            _dealerAppService = dealerAppService;
            _financialYearAppService = financialYEarAppService;
            _stationAppService = stationAppService;
            _candidateAppService = candidateAppService;
            _userAppService = userAppService;
            _dealerActivityAppService = dealerActivityAppService;
            _billAppService = billAppService;
            _billItemAppService = billItemAppService;
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
        
        //online Registration Applications
            
        public ActionResult OnlineRegApplications()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var userInfo = _userAppService.GetLoggedInUser();
            var dealers = _dealerAppService.GetRegApplicationByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear);
            return View(dealers);
        }

        //Denied applications for  Registration per station

        public ActionResult RejectedRegApplications()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var userInfo = _userAppService.GetLoggedInUser();
            var dealers = _dealerAppService.GetDeniedRegApplicationByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear);
            return View(dealers);
        }

        //Confirm registration application

        public ActionResult ApproveRegistration(int id)
        {
            double TotalBillAmount = 0;
            try
            {
               int dealerId =  _dealerAppService.ApproveRegistration(id);

               var dealer = _dealerAppService.GetDealer(dealerId);
               var DealerActivities = _dealerActivityAppService.GetDealerActivities(dealer);

                foreach (var activity in DealerActivities) {
                    TotalBillAmount = TotalBillAmount + activity.Activity.Fee + activity.Activity.RegistrationFee; 
                }

                DateTime billExpireDate = DateTime.Now;
                billExpireDate = billExpireDate.AddDays(30);

                var RegistrationBill = new CreateBillInput{
                    ApplicantId = dealer.ApplicantId,
                    StationId = dealer.StationId,
                    FinancialYearId  = dealer.FinancialYearId,
                    BillAmount = TotalBillAmount,
                    EquvAmont = TotalBillAmount,
                    Description = "Application For Registration Fee",
                    ExpiredDate = billExpireDate,
                    Currency = "TZS"
                };

                int bill = _billAppService.CreateBill(RegistrationBill);// create bill

                foreach (var activity in DealerActivities) /// add Bill Items
                {
                    CreateBillItemInput item = new CreateBillItemInput
                    {
                        BillId = bill,
                        ActivityId = activity.ActivityId,
                        Description = activity.Activity.Description,
                        Loyality = activity.Activity.Fee + activity.Activity.RegistrationFee,
                        EquvAmont = activity.Activity.Fee + activity.Activity.RegistrationFee,
                    };

                    _billItemAppService.CreateBillItem(item);
                }

                TempData["success"] = string.Format(@"The application has been approved  successfully!");
                return RedirectToAction("OnlineRegApplications");
            }
            catch
            {
                TempData["danger"] = string.Format(@"Failed to approve the selected application!");
                return RedirectToAction("OnlineRegApplications");
            }
        }

        //Get: Deny registration application
        public ActionResult DenyRegistration(int id)
        {
           var dealer =  _dealerAppService.GetDealer(id);
            var DealerActivities = _dealerActivityAppService.GetDealerActivities(dealer);
            ViewBag.DealerActivities = DealerActivities;

            return View(dealer);
        }

        //Post: deny registration
        [HttpPost]      
        public ActionResult DenyRegistration(DealerDto input)
        {
            var dealer = _dealerAppService.GetDealer(input.Id);
            var DealerActivities = _dealerActivityAppService.GetDealerActivities(dealer);
          
            if (input.Remark != null && dealer != null)
            {
                _dealerAppService.DenyRegistration(input);
                TempData["success"] = string.Format(@"The application has been rejected!");
                return RedirectToAction("OnlineRegApplications");
            }
            else
            {
                ViewBag.DealerActivities = DealerActivities;
                TempData["danger"] = string.Format(@"Failed to deny the selected application, remarks field should not be empty!");
                return View(dealer);
            }         
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
