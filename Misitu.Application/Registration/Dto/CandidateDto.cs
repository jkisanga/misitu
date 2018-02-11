using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
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
    public class CandidateDto: FullAuditedEntity
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Adress { get; set; }
        [Required]
        public virtual string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public virtual string Email { get; set; }

        public virtual double AllocatedCubicMetres { get; set; }

        public virtual string Species { get; set; }

        public virtual Boolean IsRegistered { get; set; }

        [Required]
        public virtual int FinancialYearId { get; set; }

        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
