using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.Applicants;
using Misitu.FinancialYears;
using Misitu.Registration;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing.Dto
{
    [AutoMapFrom(typeof(Bill))]
    public class BillDto: FullAuditedEntity
    {
        public virtual int ApplicantId { get; set; }
        public virtual int StationId { get; set; }
        public virtual int FinancialYearId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public virtual DateTime IssuedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public virtual DateTime ExpiredDate { get; set; }
        public virtual string ControlNumber { get; set; }
        public virtual Double BillAmount { get; set; }
        public virtual Double PaidAmount { get; set; }
        public virtual string Currency { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public virtual DateTime? PaidDate { get; set; }
        public virtual Boolean IsCanceled { get; set; }
        public virtual string Reason { get; set; }
        public virtual string Description { get; set; }

        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }

      
    }
}
