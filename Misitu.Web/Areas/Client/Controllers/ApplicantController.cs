using Abp.Application.Services;
using Misitu.Applicants.Interface;
using Misitu.Applicants.Services;
using Misitu.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Misitu.Applicants.Dto;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using Misitu.Users.Dto;
using Misitu.Users;
using Misitu.Authorization.Roles;
using Abp.Net.Mail;
using Microsoft.AspNet.Identity;
using Abp.Authorization.Users;
using Misitu.RefTables.Interface;
using Misitu.Stations;
using Misitu.Registration;
using Misitu.Registration.Dto;
using Misitu.Activities;
using Misitu.Billing;
using Misitu.FinancialYears;
using Microsoft.Reporting.WebForms;

namespace Misitu.Web.Areas.Client.Controllers
{
    
    public class ApplicantController : MisituControllerBase
    {


        private readonly IApplicationTypeService _applicationTypeService;
        private readonly IRefIdentityAppService _refIdentityService;
        private readonly IApplicantService _applicantService;
        private readonly IDealerAppService _dealerAppService; // handles and stores the application fro registration
        private readonly IDealerActivityAppService _dealerActivityAppService;
        private readonly IActivityAppService _activityAppService;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserAppService _userAppService;
        private readonly IStationAppService _stationAppService;
        private readonly IBillAppService _billAppService;
        private readonly IFinancialYearAppService _financialYearAppService;

        private static Random random = new Random();

        public ApplicantController(
                  IApplicationTypeService applicationTypeService,
                  IRefIdentityAppService refIdentityService,
                  IApplicantService applicantService,
                  IDealerAppService dealerApptService,
                  IActivityAppService activityAppService,
                  IDealerActivityAppService dealerActivityAppService,
                  IStationAppService stationAppService,
                  IBillAppService billAppService,
                  IFinancialYearAppService financialYearAppService,
                  UserManager userManager,
                  IUserAppService userAppService,
                  RoleManager roleManager,
                  IEmailSender emailSender
                  )
        {

            _applicationTypeService = applicationTypeService;
            _refIdentityService = refIdentityService;
            _applicantService = applicantService;
            _dealerAppService = dealerApptService;
            _dealerActivityAppService = dealerActivityAppService;
            _activityAppService = activityAppService;
            _stationAppService = stationAppService;
            _billAppService = billAppService;
            _financialYearAppService = financialYearAppService;
            _userManager = userManager;
            _userAppService = userAppService;
            _roleManager = roleManager;
            _emailSender = emailSender;

        }

      
        // GET: Client/Account/Create
        public ActionResult Account()
        {
            var applicationTypes = _applicationTypeService.GetRefApplicationTypes().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            var IdTypes = _refIdentityService.GetItemList().Select(c => new SelectListItem { Value = c.Name.ToString(), Text = c.Name });
            ViewBag.Type = applicationTypes;
            ViewBag.IDtype = IdTypes;
            return View();
        }

        // POST: Client/Account/Create
        [DisableValidation]
        [HttpPost]
        public  ActionResult Account(CreateInput input)
        {
           
                int applicantId = _applicantService.CreateAsync(input);

                if (applicantId > 0)
                {
                    // TODO: Add insert logic here

                    return RedirectToAction("AddUser", new { Id = applicantId });

                }

                else
                {
                var applicationTypes = _applicationTypeService.GetRefApplicationTypes().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                var IdTypes = _refIdentityService.GetItemList().Select(c => new SelectListItem { Value = c.Name.ToString(), Text = c.Name });
                ViewBag.Type = applicationTypes;
                ViewBag.IDtype = IdTypes;
                return View(input);
                }                    
        }

       public ActionResult AddUser(int Id)
        {
           
            ViewBag.Applicant = _applicantService.GetApplicantById(Id);         
            return View();
        }

        [HttpPost]
        public async Task <ActionResult> AddUser(CreateUserInput input)
        {
            if (ModelState.IsValid)
            {
                //Create user
                var user = new User
                {
                    Name = input.Name,
                    Surname = input.Surname,
                    EmailAddress = input.EmailAddress,
                    UserName = input.UserName,
                    IsActive = true,
                    ApplicantId = input.ApplicantId
                    
                };

                string[] roles = _roleManager.Roles.Where(r => r.Name == "Client").Select(c => c.Name).ToArray();

               var pass = Users.User.CreateRandomPassword();

                user.Password = new PasswordHasher().HashPassword(pass);

                //Add default roles
                user.Roles = new List<UserRole>();
                foreach (var clientRole in  _roleManager.Roles.Where(r => r.Name == "Client").ToList())
                {
                    user.Roles.Add(new UserRole { RoleId = clientRole.Id });
                }

               
                //await _userManager.CreateAsync(user);
                CheckErrors(await _userManager.CreateAsync(user));

                //Send a notification email

                //await _emailSender.SendAsync(
                //      to: input.EmailAddress,
                //      subject: "Login Credemtials to TFS Portal",
                //      body: $"Your username: <b>{input.UserName}</b>, Password: <b>{pass}</b> and url:<b><a href='http://192.168.43.29:8081/account'>Click Here to login</a></b>",
                //      isBodyHtml: true
                //  );

                return RedirectToAction("UserCreateSuccess");

            }
            else
            {
                ViewBag.Applicant = _applicantService.GetApplicantById(input.ApplicantId);
                return View(input);
            }
        }

        public ActionResult UserCreateSuccess()
        {
            return View();
        }

        [Authorize]
        public ActionResult ApplicantProfile()
        {

         var applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);

            ViewBag.Users = _userAppService.GetUsersByApplicant(applicant.Id);

            return View(applicant);
        }

        [Authorize]
        public ActionResult ApplyForRegistration()
        {
            var applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
            var Station = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.Applicant = applicant;
            ViewBag.StationId = Station;
            ViewBag.SerialNumber = RandomSerialNumbers();
            return View();
        }

        [Authorize]
        [DisableValidation]
        [HttpPost]
        public ActionResult ApplyForRegistration(CreateDealerInput input)
        {
            try
            {
                if (ModelState.IsValid) {
                    if(_dealerAppService.IsApplicationExists(input.ApplicantId, _financialYearAppService.GetActiveFinancialYear()))
                    {
                        TempData["danger"] = string.Format(@"Your have already submitted application for current  financial year ""{0}"" !", _financialYearAppService.GetActiveFinancialYear().Name);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        int DealerId = _dealerAppService.CreateDealer(input);
                        TempData["success"] = string.Format(@"The Application has been successfully created!.");
                        return RedirectToAction("AddActivities", new { Id = DealerId });
                    }
                
                }
                else
                {
                    var applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
                    var Station = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    ViewBag.Applicant = applicant;
                    ViewBag.StationId = Station;
                    ViewBag.SerialNumber = RandomSerialNumbers();
                    return View();
                }

            }catch(Exception ex)
            {
                var applicant = _applicantService.GetApplicantById(_userAppService.GetLoggedInUser().ApplicantId);
                var Station = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.Applicant = applicant;
                ViewBag.StationId = Station;
                ViewBag.SerialNumber = RandomSerialNumbers();
                ModelState.AddModelError("danger", ex.Message);

                return View();
            }
        }


        [Authorize]
        public ActionResult AddActivities(int id)
        {
            var dealer = _dealerAppService.GetDealer(id);
            var activities = _activityAppService.GetActivities();
            var DealerActivities = _dealerActivityAppService.GetDealerActivities(dealer);
            ViewBag.Dealer = dealer;
            ViewBag.Activities = activities;
            ViewBag.DealerActivities = DealerActivities;
            return View();
        }

        [HttpPost]
        [Authorize]
        [DisableValidation]
        public ActionResult AddActivities(CreateDealerActivityInput input, int[] ActivityId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var activityId in ActivityId)
                    {

                        input.ActivityId = activityId;

                        _dealerActivityAppService.CreateDealerActivity(input);

                    }
                    TempData["success"] = string.Format(@"The activities has been successfully Added!.");
                    return RedirectToAction("AddActivities", new { id = input.DealerId });
                }
                else
                {
                    return View("AddActivities", new { id = input.DealerId });
                }

            }
            catch
            {
                return View();
            }

        }

        [Authorize]
        //Submit Application
        
        public ActionResult submit(int id)
        {
            
                var dealer = _dealerAppService.GetDealer(id);

                if (dealer != null)
                {
                    dealer.IsSubmitted = true;
                    _dealerAppService.UpdateDealer(dealer);
                    TempData["success"] = string.Format(@"Your Application has been successfully received!");
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["danger"] = string.Format(@"We failed to recognize the application made!");
                    return RedirectToAction("AddActivities",new { Id = dealer.Id});
                }
                                 
        }



        // GET: DealerActivities/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var activity = _dealerActivityAppService.GetDealerActivity(id);
            var dealerId = activity.DealerId;

            await _dealerActivityAppService.DeleteDealerActivityAsync(activity);

            TempData["success"] = string.Format(@"The activity ""{0}"" has been successfully Removed.", activity.Activity.Description);
            return RedirectToAction("AddActivities", new { id = dealerId });
        }


        //Get: Deny registration application
        [Authorize]
        public ActionResult ApplicationDetails()
        {
            var application = _dealerAppService.GetRegApplication(_userAppService.GetLoggedInUser().ApplicantId, _financialYearAppService.GetActiveFinancialYear());
            if (application != null)
            {
                ViewBag.DealerActivities = _dealerActivityAppService.GetDealerActivities(application);
                return View(application);
            }else
            {
                TempData["danger"] = string.Format(@"Your have not applied for current financial year!");
                return RedirectToAction("Index", "Dashboard");
            }
            
        }

        //Print bill for Registration For current Financial Year 

        [Authorize]
        public ActionResult getRegistrationBill(int id)
        {

           
            try
            {
                var finacialYear = _financialYearAppService.GetActiveFinancialYear();
                var bill = _billAppService.GetBillForRegistrationByFyr(id, finacialYear);

                ReportViewer reportViewer = new ReportViewer();
                reportViewer.Reset();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.SizeToReportContent = true;
               
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptBill.rdlc";

                ReportParameter billId = new ReportParameter("BillId", bill.Id.ToString());
                reportViewer.LocalReport.SetParameters(new ReportParameter[] { billId });
                reportViewer.LocalReport.DataSources.Clear();

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DSBill", _billAppService.Print(bill.Id)));
                reportViewer.LocalReport.Refresh();
              

                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.Width = 1200;
                reportViewer.Height = 500;
                reportViewer.ShowPrintButton = false;
                reportViewer.ZoomMode = ZoomMode.FullPage;

                
                DisableExportOption(reportViewer, "Excel");


                ViewBag.rptBill = reportViewer;
                ViewBag.BillId = id;

                return View();
            }
            catch 
            {
                TempData["danger"] = string.Format(@"We have detected problems contact the authority!");
                return RedirectToAction("Index", "Dashboard");

            }
           

        }


        public void DisableExportOption(ReportViewer ReportViewerID, string strFormatName)
        {
            foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
            {
                if (extension.Name.ToLower() == strFormatName.ToLower())
                {
                    System.Reflection.FieldInfo info = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }


        public static string RandomSerialNumbers(int length = 6)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
