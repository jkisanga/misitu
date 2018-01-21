using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources.Dto
{
  
    [AutoMapFrom(typeof(RevenueSource))]
    public class RevenueSourcesDto : FullAuditedEntityDto
        {
        public virtual int MainRevenueSourceId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }

        [ForeignKey("MainRevenueSourceId")]
        public virtual MainRevenueSource MainRevenueSource { get; set; }
    }
    
}
