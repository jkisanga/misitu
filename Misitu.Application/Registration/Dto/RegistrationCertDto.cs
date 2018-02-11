using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Registration.Dto
{
    public class RegistrationCertDto
    {
        public int Id { get; set; }
        public  string SerialNumber { get; set; }
        public  string Name { get; set; }
        public  string Address { get; set; }
        public  string Email { get; set; }
        public  string Phone { get; set; }
        public  String Station { get; set; }
        public  string ReceiptNumber { get; set; }
        public  double Amount { get; set; }
        public  DateTime RegisteredDate { get; set; }
        public  DateTime? IssuedDate { get; set; }
        public string ExpireYear { get; set; }
        public string AutholizedOfficer { get; set; }
    }
}
