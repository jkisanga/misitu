using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Regions;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.TranstPass
{
   public class GenericTranstPass : FullAuditedEntity
    {
        public GenericTranstPass()
        {
            Status = 0;
            Prented = 0;
        }

        public virtual int ApplicantId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual int DistrictId { get; set; }
        public virtual int Status { get; set; }
        public virtual string Resoan { get; set; }
        public virtual int ActionOfficerId { get; set; }
        public virtual DateTime ExpiryDate { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual int Prented { get; set; }
       
        public virtual int TPNumber { get; set; }
        public virtual string From { get; set; }
        public virtual string To { get; set; }
        public virtual string LicenceBillNo { get; set; }


        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        [ForeignKey("ActionOfficerId")]
        public virtual User User { get; set; }
    }
}
