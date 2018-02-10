using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Misitu.Regions;
using Misitu.Stations;
using Misitu.Zones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Districts.Dto
{
    [AutoMapFrom(typeof(District))]
    public class DistrictDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
 
        public virtual int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}
