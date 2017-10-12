using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.PlotScalling;
using Misitu.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing.Dto
{
    [AutoMapFrom(typeof(AllocatedPlot))]
    public class AllocatedPlotDto: FullAuditedEntity
    {
      
            public virtual int DealerId { get; set; }

            public virtual int PlotId { get; set; }

            public virtual int FinancialYearId { get; set; }


            [ForeignKey("DealerId")]
            public virtual Dealer Dealer { get; set; }
            [ForeignKey("PlotId")]
            public virtual Plot Plot { get; set; }
            [ForeignKey("FinancialYearId")]
            public virtual FinancialYear FinancialYear { get; set; }
        }
}
