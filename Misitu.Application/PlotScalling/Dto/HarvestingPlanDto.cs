using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling.Dto
{
    [AutoMapFrom(typeof(HarvestingPlan))]
    public class HarvestingPlanDto: FullAuditedEntity
    {
        [Required]
        public virtual int StationId { get; set; }
        [Required]
        public virtual int FinancialYearId { get; set; }
        [Required]
        public virtual string Path { get; set; }


        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
    }
}
