using System;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Misitu.Api.Models;
using Misitu.Authorization;
using Misitu.MultiTenancy;
using Misitu.Users;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Misitu.Applicants.Interface;
using Misitu.Activities;
using Misitu.Billing;
using Misitu.FinancialYears;
using System.Web.Http.Description;
using Misitu.Billing.Dto;
using Misitu.TransitPasses;
using Misitu.Stations;

namespace Misitu.Api.Controllers
{
    public class ApplicantController : AbpApiController
    {
        private readonly IApplicantService applicantService;
        private readonly IActivityAppService activityAppService;
        private readonly IBillAppService billAppService;
        private readonly IFinancialYearAppService financialYearAppService;
        private readonly IBillItemAppService billItemAppService;
        private readonly ITransitPass transitPass;
        private readonly IStationAppService stationAppService;
        private readonly ICheckPointTransitPass checkPointTransitPass;

        public ApplicantController(IApplicantService applicantService, IActivityAppService activityAppService, IBillAppService billAppService, IFinancialYearAppService financialYearAppService, IBillItemAppService billItemAppService, ITransitPass transitPass, IStationAppService stationAppService, ICheckPointTransitPass checkPointTransitPass)
        {
            this.applicantService = applicantService;
            this.activityAppService = activityAppService;
            this.billAppService = billAppService;
            this.financialYearAppService = financialYearAppService;
            this.billItemAppService = billItemAppService;
            this.transitPass = transitPass;
            this.stationAppService = stationAppService;
            this.checkPointTransitPass = checkPointTransitPass;
        }

        [HttpGet()]
        public IHttpActionResult applicantList()
        {
            var applicants = this.applicantService.GetApplicantList();
            return Ok(applicants);
        }


        [HttpGet()]
        public IHttpActionResult activityList()
        {
            var activities = this.activityAppService.GetActivities();
            return Ok(activities);
        }

        [HttpGet()]
        public IHttpActionResult BillList()
        {
            var fYear = this.financialYearAppService.GetActiveFinancialYear();

            var billObj = this.billAppService.GetBills(fYear);
            return Json(billObj);
        }


        [HttpGet()]
        public IHttpActionResult BillItemList()
        {

            var billObj = this.billItemAppService.GetBillItems();
            return Json(billObj);
        }



        [HttpGet()]
        public IHttpActionResult TPList()
        {

           var obj = this.transitPass.GetTransitPasses();
            return Json(obj);
        }



        [HttpGet]
        // [ResponseType(typeof(BillItemDto))]
        public IHttpActionResult BillActivityList()
        {
            var fYear = this.financialYearAppService.GetActiveFinancialYear();

            var billObj = this.billAppService.GetBills(fYear);

            return Json(this.billItemAppService.GetBillItems());
        }


        [HttpGet]
        public IHttpActionResult CheckpointList()
        {

            var checkpoints = this.stationAppService.GetStations();

            return Json(checkpoints);
        }



        [HttpGet]
        public IHttpActionResult CheckpointTPList()
        {

            var checkpoints = this.checkPointTransitPass.GetCheckPointTransitPasses();

            return Json(checkpoints);
        }


        [HttpPost]
        [Route("/api/Applicant/RespondBill")]
        public IHttpActionResult RespondBill([FromBody] Bill input)
        {

            //insert Bill details
            var obj = new CreateBillInput
            {
                ApplicantId = input.ApplicantId,
                StationId = input.StationId,
                Currency = "TZS",
                Description = input.Description,
                BillAmount = input.BillAmount
            };


            var Billid = this.billAppService.CreateBill(obj);
            var bill = this.billAppService.GetBill(Billid);

            return Json(bill);


        }


        [HttpPost]
        [Route("/api/Applicant/ResponseBillItem")]
        public IHttpActionResult ResponseBillItem([FromBody] BillItem input)
        {
            var obj = new CreateBillItemInput
            {
                BillId = input.BillId,
                ActivityId = input.ActivityId,
                Total = input.Total,
                GfsCode = input.GfsCode,
                Description = input.Description

            };

            int billItem = this.billItemAppService.CreateBillItem(obj);
            var newBillTem = this.billItemAppService.GetBillItem(billItem);

            return Json(newBillTem);
        }



        [HttpPost]
        [Route("/api/Applicant/ResponseTransitpass")]
        public IHttpActionResult ResponseTransitpass([FromBody] TransitPass input)
        {
            var obj = new CreateTransitPassInput
            {
                BillId = input.BillId,
                ApplicantId = input.ApplicantId,
                ExpireDate = input.ExpireDate,
                AdditionInformation = input.AdditionInformation,
                HummerMaker = input.HummerMaker,
                HummerNo = input.HummerNo

            };

            int transitpassId = this.transitPass.CreateTransitPass(obj);
            var newTransitpass = this.transitPass.GetTransitPass(transitpassId);

            return Json(newTransitpass);
        }

        [HttpPost]
        [Route("/api/Applicant/ResponseCheckpointTP")]
        public IHttpActionResult ResponseCheckpointTP([FromBody] CheckPointTransitPass input)
        {

            var obj = new CreateCheckPointTransitPass
            {
                TransitPassId = input.TransitPassId,
                StationId = input.StationId

            };

            int ctId = this.checkPointTransitPass.CreateCheckPointTransitPass(obj);
            var ct = this.checkPointTransitPass.GetCheckPointTransitPass(ctId);

            return Json(ct);
        }


    }
}
    