using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Misitu.Authorization;
using Misitu.Users;
using Misitu.Stations;
using System.Linq;
using Misitu.Users.Dto;
using Misitu.Authorization.Roles;
using Abp.Application.Services.Dto;

namespace Misitu.Web.Controllers
{
    [AbpMvcAuthorize]
    public class UsersController : MisituControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IStationAppService _stationAppService;
        private readonly RoleManager _roleManager;

        public UsersController(IUserAppService userAppService, IStationAppService stationAppService, RoleManager roleManager)
        {
            _userAppService = userAppService;
            _stationAppService = stationAppService;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var output = await _userAppService.GetUsers();
            return View(output);
        }

        public ActionResult Create()
        {
            var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            var roles = _roleManager.Roles.ToList().Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName });
            ViewBag.StationId = stations;
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserInput input, string[] Roles)
        {
            if (ModelState.IsValid)
            {
                await _userAppService.CreateUser(input, Roles);
                return RedirectToAction("Index");

            }
            else
            {
                var stations = _stationAppService.GetStations().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                var roles = _roleManager.Roles.ToList().Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName });
                ViewBag.StationId = stations;
                ViewBag.Roles = roles;
                return View();
            }
        }

        public async Task<ActionResult> EditUserModal(int Id)
        {
            var user =  _userAppService.Get(Id);
            var roles = (await _userAppService.GetRoles()).Items;
            var model = new EditUserDto
            {
                User = user,
                Roles = roles
            };
            return View("_EditUserModal", model);
        }
    }
}