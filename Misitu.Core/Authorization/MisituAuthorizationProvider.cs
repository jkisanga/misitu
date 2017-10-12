using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Misitu.Authorization
{
    public class MisituAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var setup = pages.CreateChildPermission(PermissionNames.Pages_Setup, L("Setup"));
            var registration = pages.CreateChildPermission(PermissionNames.Pages_Registration, L("Registration"));
            var scalling = pages.CreateChildPermission(PermissionNames.Pages_Plot_Scalling, L("PlotScalling"));
            var licensing = pages.CreateChildPermission(PermissionNames.Pages_Harvest_Licensing, L("HarvestLicensing"));
            var billing = pages.CreateChildPermission(PermissionNames.Pages_Billing, L("Billing"));
            var tp = pages.CreateChildPermission(PermissionNames.Pages_Transit_Passes, L("TransitPasses"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Setup_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MisituConsts.LocalizationSourceName);
        }
    }
}
