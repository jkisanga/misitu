using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing.Dto
{
    public class BillPrint
    {
        public int Id { get; set; }
        public string ControlNumber { get; set; }
        public string PayerName { get; set; }
        public string PayerAddress { get; set; }
        public string PayerPhone { get; set; }
        public string Station { get; set; }
        public string StationAddress { get; set; }
        public string Currency { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public double BilledAmount { get; set; }

        public int BillId { get; set; }
        public string Description { get; set; }
        public string ItemDescription { get; set; }
        public double Amount { get; set; }
    }

}
