using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Misitu.Authorization;
using Misitu.MultiTenancy;

namespace Misitu.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Setup_Tenants)]
    public class TenantsController : MisituControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}