using Abp.AutoMapper;
using Misitu.Regions;
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
   public class CreateDistrictInput
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}
