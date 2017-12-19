using System.Linq;
using Misitu.EntityFramework;
using Misitu.MultiTenancy;

namespace Misitu.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly MisituDbContext _context;

        public DefaultTenantCreator(MisituDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
