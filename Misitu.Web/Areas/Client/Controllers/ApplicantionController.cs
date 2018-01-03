using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Misitu.Applicants;
using Misitu.Web.Models;
using Misitu.FinancialYears;
using Misitu.Applicants.Interface.ForestProduce;
using Misitu.Applicants.Dto;
using System.Threading.Tasks;
using Misitu.RefTables.Interface;
using Abp.Runtime.Validation;
using Misitu.Applicants.ForestProduce;

namespace Misitu.Web.Areas.Client.Controllers
{

    public class ApplicantionController : Controller
    {
        private readonly IFinancialYearAppService financialYear;
        private readonly IApplicant applicant;
        private readonly IRefApplicationTypeAppService refApplicationTypeAppService;
      //  private readonly IForestProduceAppliedForest forestProduceAppliedForest;
       // private readonly IForestProduceRegistration forestProduceRegistration;
       

        public ApplicantionController(IFinancialYearAppService financialYear, IApplicant applicant, 
            IRefApplicationTypeAppService refApplicationTypeAppService
            //IForestProduceAppliedForest forestProduceAppliedForest,
           // IForestProduceRegistration forestProduceRegistration
            )
        {
            this.financialYear = financialYear;
            this.applicant = applicant;
            this.refApplicationTypeAppService = refApplicationTypeAppService;
           // this.forestProduceRegistration = forestProduceRegistration;
           // this.forestProduceAppliedForest = forestProduceAppliedForest;


        }

        

        // GET: Client/Applicantion
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Client/Applicantion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return View();
        }

        // GET: Client/Applicantion/Create
        public ActionResult Create()
        {
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.FinancialYearId = activeYear.Id;
            ViewBag.Type = new SelectList(this.refApplicationTypeAppService.GetRefApplicationTypes(), "Id", "Name"); 
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include = "Type,Name,Adress,Phone,Email,IsTanzanian,IDtype,IDNumber,IDIssuePlace,IDExpiryDate,FinancialYearId")] CreateInput applicantDto)
        {
            if (ModelState.IsValid)
            {
                //var ApplicantId =  this.applicant.CreateAsync(applicantDto);
                var ApplicantId =  10;
              
                return RedirectToAction("ForestProduceRegistration", "Applicantion", new { Id = ApplicantId});
            }
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.FinancialYearId = activeYear.Id;
            ViewBag.Type = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
            return View(applicantDto);
        }

        // GET: Client/Applicantion/Edit/5
        public ActionResult Edit(int? id)
        {
            
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Name,Adress,Phone,Email,IsTanzanian,IDtype,IDNumber,IDIssuePlace,IDExpiryDate,FinancialYearId,IsDeleted,DeleterUserId,DeletionTime,LastModificationTime,LastModifierUserId,CreationTime,CreatorUserId")] ApplicantDto applicantDto)
        {
            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }
            //ViewBag.FinancialYearId = new SelectList(db.FinancialYears, "Id", "Name", applicantDto.FinancialYearId);
            return View(applicantDto);
        }

        // GET: Client/Applicantion/Delete/5
        public ActionResult Delete(int? id)
        {
           
            return View();
        }

   

       
        
        public ActionResult ForestProduceRegistration(int Id)
        {
            ViewBag.Applicant = this.applicant.GetObjectById(Id);
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.FinancialYearId = activeYear.Id;
            ViewBag.DistrictId = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableValidation]
        public  ActionResult ForestProduceRegistration(CreateForestProduceRegistration input)
        {

            if (ModelState.IsValid)
            {
               var ForestProductRegistrationId = this.applicant.CreateForestProduceRegistrationAsync(input);

                return RedirectToAction("ForestProduceAppliedForest", "Applicantion", new { Id = ForestProductRegistrationId });
            }
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.FinancialYearId = activeYear.Id;
            ViewBag.Type = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
            return View(input);
        }

        public ActionResult ForestProduceAppliedForest(int Id)
        {
            ViewBag.ForestProduceRegistration = this.applicant.GetForestProduceRegistrationById(Id);
            var activeYear = this.financialYear.GetActiveFinancialYear();
            ViewBag.FinancialYearId = activeYear.Id;
            return View();
        }

        [HttpPost]
        [DisableValidation]
        [ValidateAntiForgeryToken]
        public ActionResult ForestProduceAppliedForest(CreateForestProduceAppliedForest input)
        {
            if (ModelState.IsValid)
            {
                var ForestProductRegistrationId = this.applicant.CreateForestProduceAppliedForestAsync(input);

                return RedirectToAction("ForestProduceAppliedForest", "Applicantion", new { Id = input.ForestProduceRegistrationId });
            }
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.FinancialYearId = activeYear.Id;
            ViewBag.Type = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
            return View(input);
        }


        public ActionResult ForestProduceAppliedSpecieCategory(int Id)
        {
            ViewBag.ForestProduceRegistration = this.applicant.GetForestProduceRegistrationById(Id);
            var activeYear = this.financialYear.GetActiveFinancialYear();
            ViewBag.FinancialYearId = activeYear.Id;
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

                return RedirectToAction("ForestProduceAppliedSpecieCategory", "Applicantion", new { Id = input.ForestProduceRegistrationId });
            }
            var activeYear = this.financialYear.GetActiveFinancialYear();

            ViewBag.FinancialYearId = activeYear.Id;
            ViewBag.Type = new SelectList(this.refApplicationTypeAppService.GetDistrictList(), "Id", "Name");
            return View(input);
        }
    }
}
