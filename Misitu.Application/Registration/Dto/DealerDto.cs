using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
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
    public class DealerDto:FullAuditedEntity
    {
        public virtual string SerialNumber { get; set; }
        public virtual int StationId { get; set; }
        public virtual int ApplicantId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual string BillControlNumber { get; set; }
        public virtual double Amount { get; set; }
        public virtual DateTime? IssuedDate { get; set; }
        public virtual int? PrintedUserId { get; set; }
        public virtual bool IsSubmitted { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual bool IsDenied { get; set; }
        public virtual int? ApprovedUserId { get; set; }
        public virtual string Remark { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
