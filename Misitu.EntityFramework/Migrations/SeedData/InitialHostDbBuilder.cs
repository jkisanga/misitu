using Misitu.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Misitu.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly MisituDbContext _context;

        public InitialHostDbBuilder(MisituDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new DefaultLanguagesTextsCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
