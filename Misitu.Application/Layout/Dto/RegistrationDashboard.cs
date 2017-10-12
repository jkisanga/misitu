using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Layout.Dto
{
    public class RegistrationDashboard
    {
     
        public int candidates { get; set; }
        public int pending { get; set; }
        public int dealers { get; set; }      
        public double estimatedVolume { get; set; }
        public double TotalCollection { get; set; }
        public int dealersPerMonth { get; set; }
        public int pendingPerMonth { get; set; }
        public double CollectionPerMonth { get; set; }

    }
}
