using Misitu.Activities;
using Misitu.Applicants.Interface;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.RevenueSources;
using Misitu.TransitPasses;
using System;
using System.Collections.Generic;
using System.Linq;
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
       // private readonly IMainRevenueSuorce mainRevenueSuorce;

        public TransitPassController(ITransitPass transitPass, IBillAppService billAppService, IApplicantService applicantService, IBillItemAppService billItemAppService, IActivityAppService activityAppService, IRevenueSourceAppService revenueSourceAppService)
        {
            this.transitPass = transitPass;
            this.billAppService = billAppService;
            this.applicantService = applicantService;
            this.billItemAppService = billItemAppService;
            this.activityAppService = activityAppService;
            this.revenueSourceAppService = revenueSourceAppService;
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


            ViewBag.Activities = this.activityAppService.GetActivities();
            ViewBag.ActivitiyId = new SelectList(this.activityAppService.GetActivities(), "Id", "Description");
            return View();
        }

        [HttpPost]
        public ActionResult CreateBill(CreateBillInput input, int[] ActivtiyId, int quantity, double amount)
        {
            int BillId = this.billAppService.CreateBill(input);

           // return RedirectToAction("Index");
             return Json(new { isSuccess = BillId });
        }


        public ActionResult AddTpProduct(int Id)
        {
            ViewBag.Applicant = this.applicantService.GetApplicantById(Id);
            ViewBag.ActivitiyId = new SelectList(this.activityAppService.GetActivities(),"Id", "Description");
           
            var Applicants = this.applicantService.GetApplicantList();

            return View();
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
