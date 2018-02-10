using Abp.Runtime.Validation;
using Abp.UI;
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
using Misitu.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class TransitPassController : MisituControllerBase
    {

        private readonly ITransitPass _transitPassAppService;
        private readonly IBillAppService _billAppService;
        private readonly IApplicantService _applicantAppService;
        private readonly IBillItemAppService _billItemAppService;
        private readonly IActivityAppService _activityAppService;
        private readonly IRevenueSourceAppService _revenueSourceAppService;
        private readonly ILicenseAppService _licenseAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IStationAppService _stationAppService;
        private readonly IRegionAppService _regionAppService;
        private readonly ICheckPointTransitPass _checkPointTransitPassAppService;
        private readonly IUserAppService _userAppService;
        private readonly ISpecieAppService _specieAppService;
        private readonly ITransitPassItemAppService _transitPassItemAppService;
        private readonly IUnitMeasureAppService _unitMeasureAppService;
        public TransitPassController(ITransitPass transitPassAppService, 
            IBillAppService billAppService, IApplicantService applicantAppService, 
            IBillItemAppService billItemAppService, 
            IActivityAppService activityAppService, 
            IRevenueSourceAppService revenueSourceAppService,
            ILicenseAppService licenseAppService,
            IFinancialYearAppService financialYearAppService,
            IStationAppService stationAppService,
            IRegionAppService regionAppService,
            IUserAppService userAppService,
            ICheckPointTransitPass checkPointTransitPassAppService,
            ISpecieAppService specieAppService,
            ITransitPassItemAppService transitPassItemAppService,
            IUnitMeasureAppService unitMeasureAppService)
        {
            _transitPassAppService = transitPassAppService;
            _billAppService = billAppService;
            _applicantAppService = applicantAppService;
            _billItemAppService = billItemAppService;
            _activityAppService = activityAppService;
            _revenueSourceAppService = revenueSourceAppService;
            _licenseAppService = licenseAppService;
            _financialYearAppService = financialYearAppService;
            _stationAppService = stationAppService;
            _regionAppService = regionAppService;
            _userAppService = userAppService;
            _checkPointTransitPassAppService = checkPointTransitPassAppService;
            _specieAppService = specieAppService;
            _transitPassItemAppService = transitPassItemAppService;
            _unitMeasureAppService = unitMeasureAppService;
        }

        // GET: TransitPass/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: TransitPass
        public ActionResult Index()
        {
            var tps = this._transitPassAppService.GetPaidTransitPasses();
            return View(tps);
        }

        // GET: Pending TransitPass
        public ActionResult Pending()
        {
            var tps = _transitPassAppService.GetUnPaidTransitPasses();
            return View(tps);
        }

        // GET:Expired TransitPass
        public ActionResult Expired()
        {
            var tps = _transitPassAppService.GetExpiredTransitPasses();
            return View(tps);
        }

        // GET: TransitPass/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
 
        public ActionResult GetApplicantList()
        {
            var Applicants = _applicantAppService.GetApplicantList();

            return View(Applicants);
        }

        public ActionResult CreateBill(int Id)
        {
            ViewBag.Applicant = _applicantAppService.GetApplicantById(Id);        
            ViewBag.Activities = _activityAppService.GetActivities().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
        
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

                var activitySource = _activityAppService.GetActivity(ActivitySourceId);

                if(activitySource != null)
                {
                    input.BillAmount = activitySource.Fee;
                    input.ExpiredDate = billExpireDate;
                    input.StationId = _userAppService.GetLoggedInUser().StationId;
                    int CreatedBillId = _billAppService.CreateBill(input);

                    var revenue = _revenueSourceAppService.GetRevenueResource(activitySource.RevenueSourceId);
                    int code = Convert.ToInt32(revenue.MainRevenueSource.Code);

                    var obj = new CreateBillItemInput
                    {
                        BillId = CreatedBillId,
                        ActivityId = activitySource.Id,
                        Description = activitySource.Description,
                        GfsCode = code,
                        Total = activitySource.Fee
                    };

                    _billItemAppService.CreateBillItem(obj);

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
                ViewBag.Applicant = _applicantAppService.GetApplicantById(input.ApplicantId);
                ViewBag.Activities = _activityAppService.GetActivities().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
                return View();
            }

        }
        
        //Create TP 
        public ActionResult CreateTp(int Id)
        {
            var user = User.Identity.GetUserId();
            ViewBag.IssuedDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.RegionId = new SelectList(_regionAppService.GetRegions(), "Id", "Name");
            ViewBag.Applicant = _applicantAppService.GetApplicantById(_billAppService.GetBill(Id).ApplicantId);
            ViewBag.Bill = _billAppService.GetBill(Id);
            ViewBag.StationId = new SelectList(_stationAppService.GetStations(), "Id", "Name");

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[12];
                rng.GetBytes(tokenData);
                ViewBag.TransitPassNo = Convert.ToString(BitConverter.ToUInt32(tokenData, 0));
            }
            ViewBag.Checkpoints = _stationAppService.GetStations();
            return View();
        }

        [HttpPost]
        public ActionResult CreateTp(CreateTransitPassInput input, int[] StationId)
        {
            try
            {
                var writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                var result = writer.Write(input.TransitPassNo);
                var barcodeBitmap = new Bitmap(result);

                input.QRCode = ImageExtensions.ToByteArray(barcodeBitmap, ImageFormat.Png);
               
                input.ExpireDate = input.IssuedDate.AddDays(input.ExpireDays);
                int TransitpassId = _transitPassAppService.CreateTransitPass(input);

                for (int i = 0; i < StationId.Count(); i++)
                {
                    var checkpointObj = new CreateCheckPointTransitPass
                    {
                        TransitPassId = TransitpassId,
                        StationId = StationId[i]
                    };

                    int checkpointTP = _checkPointTransitPassAppService.CreateCheckPointTransitPass(checkpointObj);
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
            var transitPass = _transitPassAppService.GetTransitPass(Id);

            ViewBag.Species = _specieAppService.GetSpecies().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.EnglishName });
            ViewBag.UnitMeasures = _unitMeasureAppService.GetUnitMeasures().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.Activities = _activityAppService.GetActivities().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
           
            return View(transitPass);
        }

        //Post transit pass items
        [HttpPost]
        public ActionResult CreateTransitPassItems(int TransitPassId,int[] ActivityId, int[] UnitMeasureId,int[] SpecieId, int[] Quantity, string[] Size)
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
                            Quantity = (int)Quantity[i],
                            Size = Size[i]
                    };

                    _transitPassItemAppService.CreateTransitPassItem(transitPassItem);
                }

                return RedirectToAction("getBill", new { Id = TransitPassId });
            }
            catch(UserFriendlyException ex)
            {
                TempData["danger"] = string.Format(ex.Message);
                return RedirectToAction("CreateTransitPassItems", new { Id = TransitPassId });
            }
        }

        public ActionResult getBill(int id) {

            try
            {
                var tp = _transitPassAppService.GetTransitPass(id);
              
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Reset();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;

                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptBill.rdlc";

                ReportParameter billId = new ReportParameter("BillId", tp.BillId.ToString());
                reportViewer.LocalReport.SetParameters(new ReportParameter[] { billId });
                reportViewer.LocalReport.DataSources.Clear();

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBill", _transitPassAppService.getBillByTp(id)));
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
                var tp = _transitPassAppService.GetTransitPass(id);

                if (_transitPassAppService.GetTransitPassPrintout(tp.Id) != null)
                {
                    ReportViewer reportViewer = new ReportViewer();
                    reportViewer.Reset();
                    reportViewer.ProcessingMode = ProcessingMode.Local;
                    reportViewer.SizeToReportContent = true;

                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptTransitPass.rdlc";

                    ReportParameter tpId = new ReportParameter("Id", tp.Id.ToString());
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { tpId });
                    reportViewer.LocalReport.DataSources.Clear();

                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsTransitPass",_transitPassAppService.GetTransitPassPrintout(tp.Id)));
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
