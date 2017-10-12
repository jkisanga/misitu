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

namespace Misitu.Stations.Dto
{
    [AutoMapFrom(typeof(Statiton))]
    public class StationDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Address { get; set; }
      
        public virtual int ZoneId { get; set; }
    
        public virtual int RegionId { get; set; }

        [ForeignKey("ZoneId")]
        public virtual Zone Zone { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}
