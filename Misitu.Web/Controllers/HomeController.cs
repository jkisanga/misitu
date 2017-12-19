using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Misitu.Registration;
using Misitu.FinancialYears;
using Misitu.Layout.Dto;
using Misitu.Users;
using Misitu.Billing;
using Misitu.Licensing;
using Misitu.Stations;

namespace Misitu.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : MisituControllerBase
    {

        private readonly IDealerAppService _dealerAppService;
        private readonly IBillAppService _billAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IUserAppService _userAppService;
        private readonly IStationAppService _stationAppService;
        private readonly ILicenseAppService _licenseAppService;

        public HomeController(
            IFinancialYearAppService financialYearAppService,
            IUserAppService userAppService,
            IDealerAppService dealerAppService,
            IBillAppService billAppService,
            ILicenseAppService licenseAppService,
            IStationAppService stationAppService
            )
        {
            _financialYearAppService = financialYearAppService;
            _userAppService = userAppService;
            _dealerAppService = dealerAppService;
            _billAppService = billAppService;
            _licenseAppService = licenseAppService;
            _stationAppService = stationAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            var Logginuser = _userAppService.GetLoggedInUser();
            var userInfo = _userAppService.GetUserLogidInInfo();
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();

            var header = new HeaderDto {
                dealers = _dealerAppService.GetTotalDealerByStationId(_stationAppService.GetStation(Logginuser.StationId),finacialYear),
                licenses =_licenseAppService.GetTotalLicenseByStationId(Logginuser.StationId),
                bills = _billAppService.GetTotalBillsByStation(_stationAppService.GetStation(Logginuser.StationId),finacialYear),
                financialYear = _financialYearAppService.GetActiveFinancialYear(),
                UserInfo = userInfo
                };

            return PartialView("_Header",header);
        }
    }
}