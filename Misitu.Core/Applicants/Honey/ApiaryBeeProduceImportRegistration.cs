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

namespace Misitu.Applicants
{
   public class ApiaryBeeProduceImportRegistration : FullAuditedEntity
    {
        public ApiaryBeeProduceImportRegistration()
        {
            Status = 0;
            CertificatePrented = 0;
        }

        public virtual int ApplicantId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual int DistrictId { get; set; }
        public virtual int Status { get; set; }
        public virtual string Resoan { get; set; }
        public virtual int ActionOfficerId { get; set; }
        public virtual DateTime DateVisited { get; set; }
        public virtual DateTime DateApproved { get; set; }
        public virtual int CertificatePrented { get; set; }
        public virtual string TIN { get; set; }
        public virtual string BussinessLicenseNo { get; set; }
        public virtual int RegNumber { get; set; }


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
