using Abp.AutoMapper;
using Misitu.Activities;
using Misitu.RefereneceTables;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Dto
{
    [AutoMapFrom(typeof(TransitPassItem))]
    public class CreateTransitPassItem
    {
        public virtual int TransitPassId { get; set; }
        public virtual int ActivityId { get; set; }
        public virtual int UnitMeasureId { get; set; }
        public virtual int SpecieId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Size { get; set; }

        [ForeignKey("TransitPassId")]
        public virtual TransitPass TransitPass { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }

        [ForeignKey("SpecieId")]
        public virtual Specie Specie { get; set; }

        [ForeignKey("UnitMeasureId")]
        public virtual RefUnitMeasure UnitMeasure { get; set; }
    }
}
