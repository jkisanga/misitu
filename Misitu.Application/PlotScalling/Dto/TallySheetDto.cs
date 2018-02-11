using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
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
    public class TallySheetDto : FullAuditedEntity
        {
            [Required]
            public virtual int PlotId { get; set; }
            public virtual int SpecieCategoryId { get; set; }
            public virtual int DBH { get; set; }
            public virtual int TreesNumber { get; set; }
            public virtual double GnAmount { get; set; }
            public virtual double Volume { get; set; }
            public virtual double Loyality { get; set; }
            public virtual double TFF { get; set; }
            public virtual double LMDA { get; set; }
            public virtual double CESS { get; set; }
            public virtual double VAT { get; set; }
            public virtual double TP { get; set; }
            public virtual double TOTAL { get; set; }

            [ForeignKey("PlotId")]
            public virtual Plot Plot { get; set; }
            [ForeignKey("SpecieCategoryId")]
            public virtual SpecieCategory SpecieCategory { get; set; }

    }
}
