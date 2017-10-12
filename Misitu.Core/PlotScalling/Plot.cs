using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
    [Table("Plots")]
    public class Plot:FullAuditedEntity
    {

        public Plot()
        {
            IsAllocated = false;
        }

        [Required]
        public virtual int CompartmentId { get; set; }
        [Required]
        public virtual string Name { get; set; }

        public virtual Boolean IsAllocated { get; set; }

        [ForeignKey("CompartmentId")]
        public virtual Compartment Compartment { get; set; }
    }
}
