using Abp.Domain.Entities.Auditing;
using Misitu.Activities;
using Misitu.FinancialYears;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants
{
   public class ApplicantActivity : FullAuditedEntity
    {
        
        public virtual int ActivityId { get; set; }
        public virtual int ApplicantId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual string Status { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
