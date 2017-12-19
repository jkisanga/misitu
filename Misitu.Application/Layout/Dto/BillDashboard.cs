using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Layout.Dto
{
    public class BillDashboard
    {
        public int Pending { get; set; }
        public int Paid { get; set; }
        public double TotalCollection { get; set; }
        public int PaidPerMonth { get; set; }
        public int PendingPerMonth { get; set; }
        public double CollectionPerMonth { get; set; }
    }
}
