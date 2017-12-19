using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing.Dto
{
    public class LicenceCertDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Station { get; set; }
        public string StationAddress { get; set; }
        public string Currency { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Description { get; set; }
        public int BillId { get; set; }
        public string ItemDescription { get; set; }
        public double Royality { get; set; }
        public double OtherCharges { get; set; }
        public double Total { get; set; }
    }
}
