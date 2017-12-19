using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Divisions.Dto
{
    [AutoMapFrom(typeof(Division))]
    public class DivisionDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
    }
}
