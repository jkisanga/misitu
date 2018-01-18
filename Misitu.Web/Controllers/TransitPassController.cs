using Microsoft.AspNet.Identity;
using Misitu.Activities;
using Misitu.Applicants.Interface;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.FinancialYears;
using Misitu.Licensing;
using Misitu.Regions;
using Misitu.RevenueSources;
using Misitu.Stations;
using Misitu.TransitPasses;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
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
        // private readonly IMainRevenueSuorce mainRevenueSuorce;

        public TransitPassController(ITransitPass transitPass, IBillAppService billAppService, IApplicantService applicantService, IBillItemAppService billItemAppService, 
            IActivityAppService activityAppService, 
            IRevenueSourceAppService revenueSourceAppService,
            ILicenseAppService licenseAppService,
            IFinancialYearAppService financialYearAppService,
            IStationAppService stationAppService,
            IRegionAppService regionAppService,
            ICheckPointTransitPass checkPointTransitPass)
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
            this.checkPointTransitPass = checkPointTransitPass;
           // this.mainRevenueSuorce = mainRevenueSuorce;
        }




        // GET: TransitPass/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: TransitPass
        public ActionResult Index()
        {
            return View();
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
            DateTime today = DateTime.Today;
            ViewBag.IssuedDate = DateTime.Today;
            ViewBag.ExpiredDate = DateTime.Today.AddDays(15);
            ViewBag.Description = "Hundi Malipo ya Malipo ya Transitpass";

            ViewBag.Activities = this.activityAppService.GetActivities();
            ViewBag.ActivitiyId = new SelectList(this.activityAppService.GetActivities(), "Id", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult CreateBill(CreateBillInput input, int[] ActivtiyId, int[] quantity, double[] amount)
        {
            try
            {



                //insert Bill details
                int BillId = this.billAppService.CreateBill(input);


                //insert bill item details
                double GTotal = 0;
                double BillAmount = 0;
                var GfsCode = 1;

               
                for (int i = 0; i <= ActivtiyId.Length - 1; i++)
                {
                    var ActivityObj = this.activityAppService.GetActivity(ActivtiyId[i]);
                    var RevenueSourceObj = this.revenueSourceAppService.GetRevenueResource(ActivityObj.RevenueSourceId);
                    GTotal = quantity[i] * (float)amount[i];
                    GfsCode = Convert.ToInt32(RevenueSourceObj.Code);
                    var obj = new CreateBillItemInput
                    {

                        BillId = BillId,
                        ActivityId = ActivtiyId[i],
                        Description = input.Description,
                        GfsCode = GfsCode,
                        Total = GTotal


                    };

                    BillAmount = BillAmount + quantity[i] * (float)amount[i];

                    int BillItemId = this.billItemAppService.CreateBillItem(obj);

                }

                var BillObj = this.billAppService.GetBill(BillId);
                BillObj.BillAmount = BillAmount;
                this.billAppService.UpdateBill(BillObj);
                


                //redirect to Tp page
                return RedirectToAction("CreateTp", new { Id = BillId });
                //return Json(new { Total = GTotal, gfs = GfsCode });
            }
            catch
            {
                return View();
            }
                
        
            
        }

        //Create TP 
        public ActionResult CreateTp(int Id)
        {
            var user = User.Identity.GetUserId();
            ViewBag.IssuedDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.DestinationId = new SelectList(this.regionAppService.GetRegions(), "Id", "Name");
            var Fyear = this.financialYearAppService.GetActiveFinancialYear();
            var billObj = this.billAppService.GetBill(Id);
            ViewBag.Applicant = this.applicantService.GetApplicantById(billObj.ApplicantId);
            ViewBag.Bill = billObj;
            ViewBag.SourceForest = new SelectList(this.stationAppService.GetStations(), "Id", "Name");
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[12];
                rng.GetBytes(tokenData);
           ViewBag.TransitPassNo  = Convert.ToString(BitConverter.ToUInt32(tokenData, 0));
            }
            ViewBag.Checkpoints = this.stationAppService.GetStations();
            return View();
        }

        [HttpPost]
        public ActionResult CreateTp(CreateTransitPassInput input, int[] StationId)
        {
            try
            {
                int TransitpassId = this.transitPass.CreateTransitPass(input);

                for (int i = 0; i <= StationId.Count() - 1; i++)
                {
                    var checkpointObj = new CreateCheckPointTransitPass
                    {
                        TransitPassId = TransitpassId,
                        StationId = StationId[i]
                    };

                    int checkpointTP = this.checkPointTransitPass.CreateCheckPointTransitPass(checkpointObj);
                }
                // return Json(new { Result = StationId });
                return RedirectToAction("Dashboard");
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransitPass/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransitPass/Edit/5
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

        // GET: TransitPass/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransitPass/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
