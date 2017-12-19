using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Ranges;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
    [Table("Compartments")]
    public class Compartment: FullAuditedEntity
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual int RangeId { get; set; }
        [Required]
        public virtual int FinancialYearId { get; set; }
        [Required]     
        public virtual string Species { get; set; }
        [Required]
        public virtual string PlantedYear { get; set; }
        [Required]
        public virtual float Age { get; set; }
        [Required]
        public virtual float Area { get; set; }
        [Required]
        public virtual float EstimatedVolume { get; set; }
        [Required]
        public virtual string Season { get; set; }
        [Required]
        public virtual float TariffNumber { get; set; }

        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
        [ForeignKey("RangeId")]
        public virtual Range Range { get; set; }
    }
}
