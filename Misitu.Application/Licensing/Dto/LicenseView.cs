using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing.Dto
{
    public class LicenseView
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Dealer { get; set; }

        public DateTime IssuedDate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

    }
}
