using Abp.AutoMapper;
using Abp.UI;
using Microsoft.Reporting.WebForms;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.FinancialYears;
using Misitu.Layout.Dto;
using Misitu.Licensing;
using Misitu.Registration;
using Misitu.RevenueSources;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.Billing
{
    public class BillsController : Controller
    {

        private readonly IBillAppService _billAppService;
        private readonly IBillItemAppService _billItemAppService;
        private readonly IDealerAppService _dealerAppService;
        private readonly IDealerActivityAppService _dealerActivityAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly ILicenseAppService _licenseAppService;
        private readonly IRevenueSourceAppService _revenueSourceAppService;
        private readonly IUserAppService _userAppService;
        private readonly IStationAppService _stationAppService;

        public BillsController(
            IBillAppService billAppService, 
            IBillItemAppService billItemAppService,
            IDealerAppService dealerAppService,
            IDealerActivityAppService dealerActivityAppService,
             FinancialYearAppService financialYEarAppService,
             ILicenseAppService licenseAppService,
             IRevenueSourceAppService revenueSourceAppService,
             IUserAppService userAppService,
             IStationAppService stationAppService
            )
        {
            _billAppService = billAppService;
            _billItemAppService = billItemAppService;
            _dealerAppService = dealerAppService;
            _dealerActivityAppService = dealerActivityAppService;
            _financialYearAppService = financialYEarAppService;
            _licenseAppService = licenseAppService;
            _revenueSourceAppService = revenueSourceAppService;
            _userAppService = userAppService;
            _stationAppService = stationAppService;
        }

        // GET: Bills/Dashboard
        public ActionResult Dashboard()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var userInfo = _userAppService.GetLoggedInUser();

            var Dashboard = new BillDashboard
            {
                Pending = _billAppService.GetTotalPendingBillsByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                Paid = _billAppService.GetTotalPaidBillsByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                PaidPerMonth = _billAppService.GetTotalMonthBillsByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                PendingPerMonth = _billAppService.GetTotalMonthPendingBillsByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear),
                TotalCollection = _billAppService.GetTotalPendingBillsAmountByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear).Sum(),
                CollectionPerMonth = _billAppService.GetTotalPendingMonthBillsAmountByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear).Sum()
            };

            return View(Dashboard);
        }
        // GET: Bills
        public ActionResult Index()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var bills = _billAppService.GetBills(finacialYear);
            return View(bills);
        }

        // GET: Bills
        public ActionResult Paid()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var bills = _billAppService.GetPayedBills(finacialYear);
            return View(bills);
        }

        // GET: Bills/Create
        public ActionResult Create(int id)
        {
            var dealer = _dealerAppService.GetDealer(id);
            ViewBag.Dealer = dealer;
            return View();
        }

        // GET: Bills/Create
        public ActionResult CreateNew()
        {
          
            var dealers = _dealerAppService.GetAllDealers().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            var sources = _revenueSourceAppService.GetRevenueResources().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
            ViewBag.DealerId = dealers;
            ViewBag.choices = _revenueSourceAppService.GetRevenueResources();
            ViewBag.RevenueSourceId = sources;
            return View();
        }

        // POST: Bills/Create
        [HttpPost]
        public  ActionResult Create(CreateBillInput input,int[] sources, double[] Amount,double total)
        {
            if (ModelState.IsValid)
            {
                input.BillAmount = Convert.ToDouble(total);
                int bill =  _billAppService.CreateBill(input);

                if (bill > 0)
                {
                    for (int i = 0; i < sources.Length; i++)
                    {
                        var source = _revenueSourceAppService.GetRevenueResource(sources[i]);
                        if (source != null)
                        {
                            CreateBillItemInput item = new CreateBillItemInput
                            {
                                BillId = bill,
                                RevenueResourceId = source.Id,
                                Description = source.Description,
                                Loyality = Convert.ToDouble(Amount[i])
                            };

                            _billItemAppService.CreateBillItem(item);
                        }
                    }
                  
                }
                //return RedirectToAction("CreateNew");
                return Json(Url.Action("Details", "Bills",new { id = bill}));
            }
            else
            {
                var dealers = _dealerAppService.GetAllDealers().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.DealerId = dealers;
                return View(input);            
        }
    }

        // GET: Bills/confirm/5
        public ActionResult Details(int id)
        {
            var bill = new BillItemModel
            {
                Bill = _billAppService.GetBill(id),
                Items = _billItemAppService.GetBillItems(_billAppService.GetBill(id))
            };
            
            return View(bill);
        }

        //View bill in report viewer

        public ActionResult BillViewer(int id)
        {
            var bill = _billAppService.GetBill(id);
            return View(bill);
        }

        public ActionResult BillReportViewer(int id)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptBill.rdlc";

            ReportParameter billId = new ReportParameter("BillId", id.ToString());
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { billId });
            reportViewer.LocalReport.DataSources.Clear();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBill", _billAppService.Print(id)));

            reportViewer.LocalReport.Refresh();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.Width = 1200;
            reportViewer.Height = 500;
            reportViewer.ShowPrintButton = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;

            ViewBag.rptBill = reportViewer;
            ViewBag.BillId = id;
            return PartialView("_BillReportViewer");
        }


        // GET: Bills/confirm/5
        public ActionResult Confirm(int id)
        {
            var bill = _billAppService.GetBill(id);
            return View(bill);
        }

        // Post: Bills/confirm/5
        [HttpPost]
        public ActionResult Confirm(int id, BillDto input, double Amount)
        {
            var bill = _billAppService.GetBill(id);
            var dealer = _dealerAppService.GetDealer(input.DealerId);

            if(bill != null)
            {
                _billAppService.ConfirmBill(bill, Amount);
            
            }
            else
            {
                throw new UserFriendlyException("Enter the paid amount");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Download(int id)
        {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptBill.rdlc";

            ReportParameter billId = new ReportParameter("BillId", id.ToString());
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { billId });
            reportViewer.LocalReport.DataSources.Clear();
       
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBill", _billAppService.Print(id)));
          

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            //Render the report

            byte[] renderedBytes = reportViewer.LocalReport.Render(
               "PDF", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            var filepath = Path.Combine(Server.MapPath("~/App_Data"), "Bill.pdf");

            using (FileStream stream = new FileStream(filepath, FileMode.Create))
            {
                stream.Write(renderedBytes, 0, renderedBytes.Length);
            }

            return File(filepath, "application / force - download", Path.GetFileName(filepath));

        }

    

    }
}
