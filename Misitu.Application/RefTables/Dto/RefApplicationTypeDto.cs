using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Dto
{
    [AutoMapFrom(typeof(RefApplicationTypeDto))]
   public class RefApplicationTypeDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
    }
}
