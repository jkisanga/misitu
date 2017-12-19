using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources.Dto
{
  
    [AutoMapFrom(typeof(RefSubRevenueSource))]
    public class RefSubRevenueSourcesDto : FullAuditedEntityDto
        {
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
    }
    
}
