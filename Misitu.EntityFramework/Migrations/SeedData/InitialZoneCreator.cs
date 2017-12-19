using Misitu.EntityFramework;
using Misitu.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Migrations.SeedData
{
    public class InitialZoneCreator
    {
        private readonly MisituDbContext _context;

        public InitialZoneCreator(MisituDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var douglas = _context.Zones.FirstOrDefault(p => p.Name == "HQ");
            if (douglas == null)
            {
                _context.Zones.Add(
                    new Zone
                    {
                        Name = "HQ",
                        Description = "Headquarter"
                    });
                _context.SaveChanges();
            }

            var asimov = _context.Zones.FirstOrDefault(p => p.Name == "EZ");
            if (asimov == null)
            {
                _context.Zones.Add(
                    new Zone
                    {
                        Name = "EZ",
                        Description = "Eastern Zone"
                    });
                _context.SaveChanges();
            }
        }
    }
}
