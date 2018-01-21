using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Misitu.Authorization;
using Misitu.Authorization.Roles;
using Misitu.EntityFramework;
using Misitu.Users;
using Microsoft.AspNet.Identity;

namespace Misitu.Migrations.SeedData
{
    public class HostRoleAndUserCreator
    {
        private readonly MisituDbContext _context;

        public HostRoleAndUserCreator(MisituDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            var staffRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Staff);
            var clientRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Client);
            if (adminRoleForHost == null || staffRoleForHost== null || clientRoleForHost==null)
            {
                adminRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Admin, DisplayName = StaticRoleNames.Host.Admin, IsStatic = true });
                _context.SaveChanges();

                 staffRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Staff, DisplayName = StaticRoleNames.Host.Staff, IsStatic = true });
                _context.SaveChanges();

                clientRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Client, DisplayName = StaticRoleNames.Host.Client, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new MisituAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForHost.Id
                        });
                }

                _context.SaveChanges();
            }

            //Admin user for tenancy host

            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        UserName = User.AdminUserName,
                        Name = "System",
                        Surname = "Administrator",
                        EmailAddress = "admin@aspnetboilerplate.com",
                        IsEmailConfirmed = true,
                        Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                    });

                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, staffRoleForHost.Id));

                _context.SaveChanges();
            }
        }
    }
}