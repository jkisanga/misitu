using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Species.Dto
{
    [AutoMapFrom(typeof(SpecieCategory))]
    public class SpecieCategoryDto: FullAuditedEntity
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual double Amount { get; set; }
        public virtual string Description { get; set; }
    }
}
