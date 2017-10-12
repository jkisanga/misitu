using Misitu.EntityFramework;
using Misitu.GnTreeVolumeRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Migrations.SeedData
{
    public class DefaultGNCreatot
    {
        private readonly MisituDbContext _context;

        public DefaultGNCreatot(MisituDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
           

            List<GnTreeVolumeRate> rates = new List<GnTreeVolumeRate>
               {
                new GnTreeVolumeRate { Dbh = 11,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 12,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 13,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 14,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 15,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 16,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 17,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 18,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 19,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 20,Class = "I", Amount=5700},
                new GnTreeVolumeRate { Dbh = 21,Class = "II", Amount=11300},
                new GnTreeVolumeRate { Dbh = 22,Class = "II", Amount=11300},
                new GnTreeVolumeRate { Dbh = 23,Class = "II", Amount=11300},
                new GnTreeVolumeRate { Dbh = 24,Class = "II", Amount=11300},
                new GnTreeVolumeRate { Dbh = 25,Class = "II", Amount=11300},
                new GnTreeVolumeRate { Dbh = 26,Class = "III", Amount=28300},
                new GnTreeVolumeRate { Dbh = 27,Class = "III", Amount=28300},
                new GnTreeVolumeRate { Dbh = 28,Class = "III", Amount=28300},
                new GnTreeVolumeRate { Dbh = 29,Class = "III", Amount=28300},
                new GnTreeVolumeRate { Dbh = 30,Class = "III", Amount=28300},
                new GnTreeVolumeRate { Dbh = 31,Class = "IV", Amount=48900},
                new GnTreeVolumeRate { Dbh = 32,Class = "IV", Amount=48900},
                new GnTreeVolumeRate { Dbh = 33,Class = "IV", Amount=48900},
                new GnTreeVolumeRate { Dbh = 34,Class = "IV", Amount=48900},
                new GnTreeVolumeRate { Dbh = 35,Class = "IV", Amount=48900},
                new GnTreeVolumeRate { Dbh = 36,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 37,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 38,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 39,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 40,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 41,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 42,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 43,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 44,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 45,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 46,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 47,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 48,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 49,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 50,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 51,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 52,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 53,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 54,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 55,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 56,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 57,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 58,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 59,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 60,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 61,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 62,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 63,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 64,Class = "V", Amount=54200},
                new GnTreeVolumeRate { Dbh = 65,Class = "V", Amount=54200},
            };

            foreach (var rate in rates)
            {
                AddGNIfNotExists(rate);
            }


        }
        private void AddGNIfNotExists(GnTreeVolumeRate gn)
        {
            if (_context.GnTreeVolumeRates.Any(l => l.Dbh == gn.Dbh))
            {
                return;
            }

            _context.GnTreeVolumeRates.Add(gn);

            _context.SaveChanges();
        }
    }
}
