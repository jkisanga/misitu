using Abp.Runtime.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using Misitu.Activities;
using Misitu.Applicants.Interface;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.FinancialYears;
using Misitu.Licensing;
using Misitu.RefTables.Interface;
using Misitu.Regions;
using Misitu.RevenueSources;
using Misitu.Species;
using Misitu.Stations;
using Misitu.TransitPasses;
using Misitu.TransitPasses.Dto;
using Misitu.TransitPasses.Interface;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class TransitPassController : MisituControllerBase
    {

        private readonly ITransitPass transitPass;
        private readonly IBillAppService billAppService;
        private readonly IApplicantService applicantService;
        private readonly IBillItemAppService billItemAppService;
        private readonly IActivityAppService activityAppService;
        private readonly IRevenueSourceAppService revenueSourceAppService;
        private readonly ILicenseAppService licenseAppService;
        private readonly IFinancialYearAppService financialYearAppService;
        private readonly IStationAppService stationAppService;
        private readonly IRegionAppService regionAppService;
        private readonly ICheckPointTransitPass checkPointTransitPass;
        private readonly IUserAppService userAppService;
        private readonly ISpecieAppService specieAppService;
        private readonly ITransitPassItemAppService transitPassItemAppService;
        private readonly IUnitMeasureAppService unitMeasureAppService;

        public TransitPassController(ITransitPass transitPass, 
            IBillAppService billAppService, IApplicantService applicantService, 
            IBillItemAppService billItemAppService, 
            IActivityAppService activityAppService, 
            IRevenueSourceAppService revenueSourceAppService,
            ILicenseAppService licenseAppService,
            IFinancialYearAppService financialYearAppService,
            IStationAppService stationAppService,
            IRegionAppService regionAppService,
            IUserAppService userAppService,
            ICheckPointTransitPass checkPointTransitPass,
            ISpecieAppService specieAppService,
            ITransitPassItemAppService transitPassItemAppService,
            IUnitMeasureAppService unitMeasureAppService)
        {
            this.transitPass = transitPass;
            this.billAppService = billAppService;
            this.applicantService = applicantService;
            this.billItemAppService = billItemAppService;
            this.activityAppService = activityAppService;
            this.revenueSourceAppService = revenueSourceAppService;
            this.licenseAppService = licenseAppService;
            this.financialYearAppService = financialYearAppService;
            this.stationAppService = stationAppService;
            this.regionAppService = regionAppService;
            this.userAppService = userAppService;
            this.checkPointTransitPass = checkPointTransitPass;
            this.specieAppService = specieAppService;
            this.transitPassItemAppService = transitPassItemAppService;
            this.unitMeasureAppService = unitMeasureAppService;
        }




        // GET: TransitPass/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: TransitPass
        public ActionResult Index()
        {
            var tps = this.transitPass.GetPaidTransitPasses();
            return View(tps);
        }


        // GET: TransitPass
        public ActionResult Pending()
        {
            var tps = this.transitPass.GetUnPaidTransitPasses();
            return View(tps);
        }


        // GET: TransitPass/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public ActionResult GetApplicantList()
        {
            var Applicants = this.applicantService.GetApplicantList();

            return View(Applicants);
        }


        public ActionResult CreateBill(int Id)
        {
            ViewBag.Applicant = this.applicantService.GetApplicantById(Id);        
            ViewBag.Activities = this.activityAppService.GetActivities().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
        
            return View();
        }

        [HttpPost]
        public ActionResult CreateBill(CreateBillInput input, int[] ActivityId, int[] Quantity, int ActivitySourceId)
        {
            try
            {
                //insert Bill details
                DateTime billExpireDate = DateTime.Now;
                billExpireDate = billExpireDate.AddDays(30);

                var activitySource = this.activityAppService.GetActivity(ActivitySourceId);

                if(activitySource != null)
                {
                    input.BillAmount = activitySource.Fee;
                    input.ExpiredDate = billExpireDate;
                    input.StationId = this.userAppService.GetLoggedInUser().StationId;
                    int CreatedBillId = this.billAppService.CreateBill(input);

                    var revenue = this.revenueSourceAppService.GetRevenueResource(activitySource.RevenueSourceId);
                    int code = Convert.ToInt32(revenue.MainRevenueSource.Code);

                    var obj = new CreateBillItemInput
                    {
                        BillId = CreatedBillId,
                        ActivityId = activitySource.Id,
                        Description = activitySource.Description,
                        GfsCode = code,
                        Total = activitySource.Fee
                    };

                    this.billItemAppService.CreateBillItem(obj);

                    //redirect to Tp page
                    TempData["success"] = string.Format(@"The Bill has been Created successfully!");
                    return RedirectToAction("CreateTp", new { Id = CreatedBillId });
                }else
                {
                    //redirect to Tp page
                    TempData["success"] = string.Format(@"make sure sub item source is selected!");
                    return RedirectToAction("CreateBill", new { Id = input.ApplicantId });
                }

            }
            catch (Exception ex)
            {
                TempData["danger"] = ex.Message;
                ViewBag.Applicant = this.applicantService.GetApplicantById(input.ApplicantId);
                ViewBag.Activities = this.activityAppService.GetActivities().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
                return View();

            }

        }

        //Create TP 
        public ActionResult CreateTp(int Id)
        {
            var user = User.Identity.GetUserId();
            ViewBag.IssuedDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.DestinationId = new SelectList(this.regionAppService.GetRegions(), "Id", "Name");
            ViewBag.Applicant = this.applicantService.GetApplicantById(this.billAppService.GetBill(Id).ApplicantId);
            ViewBag.Bill = this.billAppService.GetBill(Id);
            ViewBag.SourceForest = new SelectList(this.stationAppService.GetStations(), "Id", "Name");

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[12];
                rng.GetBytes(tokenData);
                ViewBag.TransitPassNo = Convert.ToString(BitConverter.ToUInt32(tokenData, 0));
            }
            ViewBag.Checkpoints = this.stationAppService.GetStations();
            return View();
        }

        [HttpPost]
        public ActionResult CreateTp(CreateTransitPassInput input, int[] StationId)
        {
            try
            {
                input.ExpireDate = input.IssuedDate.AddDays(input.ExpireDays);
                int TransitpassId = this.transitPass.CreateTransitPass(input);

                for (int i = 0; i < StationId.Count(); i++)
                {
                    var checkpointObj = new CreateCheckPointTransitPass
                    {
                        TransitPassId = TransitpassId,
                        StationId = StationId[i]
                    };

                    int checkpointTP = this.checkPointTransitPass.CreateCheckPointTransitPass(checkpointObj);
                }
            
                return RedirectToAction("CreateTransitPassItems", new { Id = TransitpassId });
            }
            catch
            {
                return View();
            }
        }

        //get Transit Pass Items
        public ActionResult CreateTransitPassItems(int Id)
        {
            var transitPass = this.transitPass.GetTransitPass(Id);

            ViewBag.Species = this.specieAppService.GetSpecies().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.EnglishName });
            ViewBag.UnitMeasures = this.unitMeasureAppService.GetUnitMeasures().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.Activities = this.activityAppService.GetActivities().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
           
            return View(transitPass);
        }

        //Post transit pass items
        [HttpPost]
        public ActionResult CreateTransitPassItems(int TransitPassId,int[] ActivityId, int[] UnitMeasureId,int[] SpecieId, int[] Quantity)
        {
            try
            {
               for(int i = 0; i < ActivityId.Length; i++)
                {
                    var transitPassItem = new CreateTransitPassItem {
                            TransitPassId = TransitPassId,
                            ActivityId = ActivityId[i],
                            UnitMeasureId = UnitMeasureId[i],
                            SpecieId = SpecieId[i],
                            Quantity = (int)Quantity[i]
                     };

                    this.transitPassItemAppService.CreateTransitPassItem(transitPassItem);
                }

                return RedirectToAction("getBill", new { Id = TransitPassId });
            }
            catch
            {
                TempData["danger"] = string.Format(@"Fill all inputs");
                return RedirectToAction("CreateTransitPassItems", new { Id = TransitPassId });
            }
        }

        public ActionResult getBill(int id) {

            try
            {
                var tp = this.transitPass.GetTransitPass(id);
              
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Reset();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptBill.rdlc";

                ReportParameter billId = new ReportParameter("BillId", tp.BillId.ToString());
                reportViewer.LocalReport.SetParameters(new ReportParameter[] { billId });
                reportViewer.LocalReport.DataSources.Clear();

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBill", this.transitPass.getBillByTp(id)));
                reportViewer.LocalReport.Refresh();

                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.Width = 1200;
                reportViewer.Height = 500;
                reportViewer.ShowPrintButton = false;
                reportViewer.ZoomMode = ZoomMode.FullPage;

                ViewBag.rptBill = reportViewer;
                ViewBag.BillId = id;

                return View();
            }
            catch
            {
                TempData["danger"] = string.Format(@"We have detected problems contact the authority!");
                return RedirectToAction("Dashboard", "TransitPass");

            }
        }

        public ActionResult getTransitPass(int id)
        {

            try
            {              
                var tp = this.transitPass.GetTransitPass(id);

                if (this.transitPass.GetTransitPassPrintout(tp.Id) != null)
                {

                    ReportViewer reportViewer = new ReportViewer();
                    reportViewer.Reset();
                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.SizeToReportContent = true;

                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptTransitPass.rdlc";

                    ReportParameter tpId = new ReportParameter("Id", tp.Id.ToString());
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { tpId });
                    reportViewer.LocalReport.DataSources.Clear();

                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsTransitPass", this.transitPass.GetTransitPassPrintout(tp.Id)));
                    reportViewer.LocalReport.Refresh();

                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.Width = 1200;
                    reportViewer.Height = 500;
                    reportViewer.ShowPrintButton = false;
                    reportViewer.ZoomMode = ZoomMode.FullPage;

                    ViewBag.rptTransitPass = reportViewer;
                    ViewBag.Id = id;

                    return View();
                }else
                {
                    TempData["danger"] = string.Format(@"You cant print the TP untill the payment is complete!");
                    return RedirectToAction("Dashboard", "TransitPass");
                }
            }
            catch
            {
                TempData["danger"] = string.Format(@"We have detected problems contact the authority!");
                return RedirectToAction("Dashboard", "TransitPass");

            }


        }


    }
}
