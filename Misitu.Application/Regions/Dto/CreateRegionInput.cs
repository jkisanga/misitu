using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Regions.Dto
{
    [AutoMapTo(typeof(Region))]
    public class CreateRegionInput
    {
        [Required]
        public string Name { get; set; }
    }
}
