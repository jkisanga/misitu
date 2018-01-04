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

namespace Misitu.Web.Areas.Client.Controllers
{

    public class ApplicantController : MisituControllerBase
    {


        private readonly IApplicationTypeService _applicationTypeService;
        private readonly IApplicantService _applicantService;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IEmailSender _emailSender;

        public ApplicantController(
                  IApplicationTypeService applicationTypeService,
                  IApplicantService applicantService,
                  UserManager userManager,
                  RoleManager roleManager,
                  IEmailSender emailSender
                  )
        {

            _applicationTypeService = applicationTypeService;
            _applicantService = applicantService;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;

        }

      

        // GET: Client/Account/Create
        public ActionResult Register()
        {
            var applicationTypes = _applicationTypeService.GetRefApplicationTypes().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.Type = applicationTypes;
            return View();
        }

        // POST: Client/Account/Create
        [DisableValidation]
        [HttpPost]
        public  ActionResult Register(CreateInput input)
        {
            int applicantId =  _applicantService.CreateAsync(input);
            

            if (applicantId > 0) {
                // TODO: Add insert logic here

                return RedirectToAction("AddUser", new { Id = applicantId });

            }
            else
            {

                var applicationTypes = _applicationTypeService.GetRefApplicationTypes().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.Type = applicationTypes;
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
                    IsActive = true
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
                _emailSender.Send(
                    to: input.EmailAddress,
                    subject: "Login Credemtials to TFS Portal",
                    body: $"Your username: <b>{input.UserName}</b>, Password: <b>{pass}</b> and url:<b><a href='http://192.168.43.29:8081/account'>Click Here to login</a></b>",
                    isBodyHtml: true
                );

                return RedirectToAction("Login","Account",new { area=""});

            }
            else
            {
                ViewBag.Applicant = _applicantService.GetApplicantById(input.ApplicantId);
                return View(input);
            }
        }
    }
}
