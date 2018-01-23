using Abp.AutoMapper;
using Misitu.Applicants;
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
    [AutoMapFrom(typeof(Dealer))]
    public class CreateDealerInput
    {
        public virtual string SerialNumber { get; set; }
        [Required]
        public virtual int StationId { get; set; }
        public virtual int ApplicantId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual double Amount { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
