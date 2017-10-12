using Abp.AutoMapper;
using Misitu.FinancialYears;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Registration.Dto
{
    [AutoMapFrom(typeof(Candidate))]
    public class CreateDealerInput
    {
        public virtual string SerialNumber { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Address { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public virtual string Email { get; set; }
        [Required]
        public virtual string Phone { get; set; }

        public virtual int StationId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual string ReceiptNumber { get; set; }
        public virtual double Amount { get; set; }
        public virtual DateTime RegisteredDate { get; set; }
        public virtual DateTime? IssuedDate { get; set; }

        public virtual string TIN { get; set; }
        public virtual string BusinessLicense { get; set; }
        public virtual string PaymentReferenceNumber { get; set; }
        public virtual string PrintedBy { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
