using Abp.AutoMapper;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling.Dto
{
    [AutoMapFrom(typeof(TallySheet))]
    public class CreateTallySheetInput
    {
        [Required]
        public virtual int PlotId { get; set; }
        public virtual int SpecieCategoryId { get; set; }
        public virtual int TariffNumber { get; set; }

        [ForeignKey("PlotId")]
        public virtual Plot Plot { get; set; }

        [ForeignKey("SpecieCategoryId")]
        public virtual SpecieCategory SpecieCategory { get; set; }
    }
}
