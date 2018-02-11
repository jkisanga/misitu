using Abp.Authorization;
using Misitu.Authorization.Roles;
using Misitu.MultiTenancy;
using Misitu.Users;

namespace Misitu.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
