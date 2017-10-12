using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Regions.Dto
{
    [AutoMapFrom(typeof(Region))]
    public class RegionDto : FullAuditedEntityDto
    {
        [Required]
        public string Name { get; set; }
      }   
  
}
