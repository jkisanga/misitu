using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Species
{
    public class Specie:FullAuditedEntity
    {
        [Required]
        public virtual int SpecieCategoryId { get; set; }
        [Required]
        public virtual string EnglishName { get; set; }
        public virtual string CommonName { get; set; }
        public virtual string SwahiliName { get; set; }

        [ForeignKey("SpecieCategoryId")]
        public virtual SpecieCategory SpecieCategory { get; set; }
    }
}
