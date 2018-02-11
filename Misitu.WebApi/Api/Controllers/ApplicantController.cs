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
using Misitu.RevenueSources;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Text;

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
        private readonly IRevenueSourceAppService revenueSourceAppService;

        public ApplicantController(IApplicantService applicantService, IActivityAppService activityAppService, IBillAppService billAppService, IFinancialYearAppService financialYearAppService, IBillItemAppService billItemAppService, ITransitPass transitPass, IStationAppService stationAppService, ICheckPointTransitPass checkPointTransitPass, IRevenueSourceAppService revenueSourceAppService)
        {
            this.applicantService = applicantService;
            this.activityAppService = activityAppService;
            this.billAppService = billAppService;
            this.financialYearAppService = financialYearAppService;
            this.billItemAppService = billItemAppService;
            this.transitPass = transitPass;
            this.stationAppService = stationAppService;
            this.checkPointTransitPass = checkPointTransitPass;
            this.revenueSourceAppService = revenueSourceAppService;
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
            var ActivityObj = this.activityAppService.GetActivity(input.ActivityId);


            var revenue = this.revenueSourceAppService.GetRevenueResource(ActivityObj.RevenueSourceId);
            int code = Convert.ToInt32(revenue.MainRevenueSource.Code);

            var obj = new CreateBillItemInput
            {
                BillId = input.BillId,
                ActivityId = input.ActivityId,
                Total = input.Total,
                GfsCode = code,
                Description = input.Description

            };

            int billItem = this.billItemAppService.CreateBillItemAPI(obj);
            var newBillTem = this.billItemAppService.GetBillItem(billItem);

            return Json("");
        }


        [HttpGet]
       // [Route("/api/Applicant/ResponseTransitpassP")]
        public IHttpActionResult ResponseTransitpassP()
        {
            string data_string = "<gepgBillSubReq>" +
              "<BillHdr>" +
              "<SpCode>SP128</SpCode>" +
              "<RtrRespFlg>true</RtrRespFlg>" +
              "</BillHdr>" +
              "<BillTrxInf>" +
              "<BillId>1812022009968</BillId>" +
              "<SubSpCode>3001</SubSpCode> " +
        "<SpSysId>TFS001</SpSysId>" +
        "<BillAmt>1180</BillAmt>" +
        "<MiscAmt>0</MiscAmt>" +
        "<BillExprDt>2017-12-31T23:59:59</BillExprDt>" +
        "<PyrId>5144AA5914</PyrId>" +
          "<PyrName>test</PyrName>" +
          "<BillDesc>Bill Number 5913</BillDesc>" +
          "<BillGenDt>2017-12-21T09:39:00</BillGenDt>" +
          "<BillGenBy>Bhstenkubo</BillGenBy>" +
          "<BillApprBy>Joshua</BillApprBy>" +
          "<PyrCellNum/>" +
          " <PyrEmail/>" +
          "<Ccy>TZS</Ccy>" +
          "<BillEqvAmt>1180</BillEqvAmt>" +
          "<RemFlag>false</RemFlag>" +
          "<BillPayOpt>1</BillPayOpt>" +
          "<BillItems>" +
          "<BillItem>" +
          "<BillItemRef>5144AA5914</BillItemRef>" +
          "<UseItemRefOnPay>N</UseItemRefOnPay>" +
          "<BillItemAmt>1180</BillItemAmt>" +
          "<BillItemEqvAmt>1180</BillItemEqvAmt>" +
          "<BillItemMiscAmt>0</BillItemMiscAmt>" +
         "<GfsCode>140316</GfsCode>" +
         "</BillItem>" +
         "</BillItems>" +
         "</BillTrxInf>" +
         "</gepgBillSubReq>";


            return Json(data_string);


        }



        [HttpPost]
        [Route("/api/Applicant/ResponseTransitpass")]
        public IHttpActionResult ResponseTransitpass([FromBody] TransitPass input)
        {
            Logger.Debug("Testing logger");

            var obj = new CreateTransitPassInput
            {
                BillId = input.BillId,
                ApplicantId = input.ApplicantId,
                SourceName = input.SourceName,
                DestinationName = input.DestinationName,
                VehcleNo = input.VehcleNo,
                ExpireDate = DateTime.Now.AddDays(input.ExpireDays),
                AdditionInformation = input.AdditionInformation,
                HummerMaker = input.HummerMaker,
                HummerNo = input.HummerNo,
                ExpireDays = input.ExpireDays

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

        public IHttpActionResult getTransitPass(int id)
        {
            return Json("");
        }

        [HttpGet]
        public IHttpActionResult getTPBill(int id)
        {
            List<BillPrint> billPrint = this.transitPass.getBillByTp(id);

            return Json(billPrint);
        }


        [HttpGet]
        public IHttpActionResult getBillByTPId(int id)
        {
            BillPrint billPrint = this.transitPass.getBillByTPId(id);

            return Json(billPrint);
        }

        [HttpPost]
        [Route("/api/Applicant/sendRequestToGEPG/{Id}")]
        public IHttpActionResult sendRequestToGEPG(int Id)
        {
            var valuesBillItems = "";
            var billObj = this.billAppService.GetBill(Id);
            var billItems = this.billItemAppService.GetBillItems(Id);


            if (billItems != null)
            {
                foreach (var billItem in billItems)
                {

                    valuesBillItems = "<BillItem><BillItemRef>"+billItem.Id+"</BillItemRef><UseItemRefOnPay>N</UseItemRefOnPay><BillItemAmt>"+billItem.Total+"</BillItemAmt><BillItemEqvAmt>"+billItem.Total+"</BillItemEqvAmt><BillItemMiscAmt>0.0</BillItemMiscAmt><GfsCode>"+billItem.GfsCode+"</GfsCode></BillItem>";

                }
            }
            else
            {
                valuesBillItems = "<BillItem><BillItemRef></BillItemRef><UseItemRefOnPay>N</UseItemRefOnPay><BillItemAmt></BillItemAmt><BillItemEqvAmt></BillItemEqvAmt><BillItemMiscAmt>0.0</BillItemMiscAmt><GfsCode></GfsCode></BillItem>";
            }



            Logger.Debug("Testing Debug");
            Logger.Error("Testing Logger Error");



            string data_string = "<gepgBillSubReq><BillHdr><SpCode>SP128</SpCode><RtrRespFlg>true</RtrRespFlg></BillHdr><BillTrxInf><BillId>TFS"+billObj.Id+"</BillId><SubSpCode>3001</SubSpCode> <SpSysId>TFS001</SpSysId><BillAmt>"+billObj.BillAmount+ "</BillAmt><MiscAmt>0</MiscAmt><BillExprDt>"+billObj.ExpiredDate.ToString("yyyy-MM-ddTHH:mm:ss")+"</BillExprDt><PyrId>"+billObj.Applicant.Name+ "</PyrId><PyrName>"+billObj.Applicant.Name+ "</PyrName><BillDesc>"+billObj.Description+ "</BillDesc><BillGenDt>"+billObj.IssuedDate.ToString("yyyy-MM-ddTHH:mm:ss")+"</BillGenDt><BillGenBy>TFS</BillGenBy><BillApprBy>TFS</BillApprBy><PyrCellNum/><PyrEmail/><Ccy>TZS</Ccy><BillEqvAmt>"+billObj.BillAmount+"</BillEqvAmt><RemFlag>false</RemFlag><BillPayOpt>1</BillPayOpt><BillItems>"+valuesBillItems+"</BillItems></BillTrxInf></gepgBillSubReq>";

            string url = "http://154.118.230.18/api/bill/qrequest";

            return Json(postXMLData(url, data_string));



           // return Json(data_string);
        }

     

        public string postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            string postData = "XMLData=" + requestXml;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "application/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            request.Headers["Gepg-Com"] = "default.sp.in";
            request.Headers["Gepg-Code"] = "SP128";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return response.StatusCode.ToString();
        }

       
        [HttpPost]
        [Route("/api/Applicant/GetControlNo")]
        public IHttpActionResult GetControlNo()
        {
           // var request = WebRequest.Create("http://localhost:61814/api/Applicant/GetControlNo/") as HttpWebRequest;
            //var response = request.GetResponse();
            var response = File.ReadAllText("http://localhost:61814/api/Applicant/GetControlNo");

            //Stream receiveStream = response.GetResponseStream();
            //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

          

           // BillDto billDto = this.billAppService.GetBill(92);
           // billDto.ControlNumber = "test";
           // billDto.ApplicantId = 5;
           //int id = this.billAppService.UpdateBill1(billDto);
           // var ackResponse = "<gepgBillSubRespAck><TrxStsCode>7101</TrxStsCode></gepgBillSubRespAck>";
            //Trace.WriteLine(ackResponse);
           
            return Json("");
        }


        [HttpGet]
        public IHttpActionResult ReceivePayment(XmlDocument xmlDocument)
        {
             //Acknowledge to GePG
         var ackResponse = "<gepgPmtSpInfoAck><TrxStsCode>7101</TrxStsCode></gepgPmtSpInfoAck>";
            Console.WriteLine(ackResponse);


            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            xmlDocument.WriteTo(tx);

            string str = sw.ToString();//

            //    // Manipulate curl post result
            //    $requestBody = file_get_contents('php://input');
            //    $outResult = simplexml_load_string($requestBody);

            //        if (!empty($outResult))
            //        {
            //            foreach ($outResult->children() as $child) {
            //            $TrxId = (String) $child->TrxId;
            //            $PayRefId = (String) $child->PayRefId;
            //            $BillId = (String) $child->BillId;
            //            $PayCtrNum = (String) $child->PayCtrNum;
            //            $BillAmt = (String) $child->BillAmt;
            //            $PaidAmt = (String) $child->PaidAmt;
            //            $BillPayOpt = (String) $child->BillPayOpt;
            //            $CCy = (String) $child->CCy;
            //            $TrxDtTm = (String) $child->TrxDtTm;
            //            $UsdPayChnl = (String) $child->UsdPayChnl;
            //            $PyrCellNum = (String) $child->PyrCellNum;
            //            $PyrName = (String) $child->PyrName;
            //            $PyrEmail = (String) $child->PyrEmail;
            //            $PspReceiptNumber = (String) $child->PspReceiptNumber;
            //            $PspName = (String) $child->PspName;
            //            $CtrAccNum = (String) $child->CtrAccNum;

            //$obj = new Payment();
            //            $obj->bill_id = $BillId;
            //            $obj->payment_control_no = $PayCtrNum;
            //$obj->bill_amount = $BillAmt;
            //            $obj->paid_amount = $PaidAmt;
            //            $obj->pay_option = $BillPayOpt;
            //            $obj->currency = $CCy;
            //$transDate = date('Y-m-d', strtotime($TrxDtTm));
            //            $obj->tranDate = $transDate;
            //            $obj->used_pay_channel = $UsdPayChnl;
            //            $obj->payer_cell_no = $PyrCellNum;
            //            $obj->payer_name = $PyrName;
            //            $obj->payer_email = $PyrEmail;
            //            $obj->save();
            //            }

            return Json(str);
        }

            }



    }



    