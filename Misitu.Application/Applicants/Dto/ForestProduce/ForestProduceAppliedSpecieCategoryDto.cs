using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.ForestProduce
{
    [AutoMapFrom(typeof(ForestProduceAppliedSpecieCategory))]
   public class ForestProduceAppliedSpecieCategoryDto : FullAuditedEntityDto
    {
        public virtual int ForestProduceRegistrationId { get; set; }
        public virtual int SpecieCategoryId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual string Status { get; set; }
        public virtual Decimal Volume { get; set; }

        [ForeignKey("ForestProduceRegistrationId")]
        public virtual ForestProduceRegistration ForestProduceRegistration { get; set; }
        [ForeignKey("SpecieCategoryId")]
        public virtual SpecieCategory SpecieCategory { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
