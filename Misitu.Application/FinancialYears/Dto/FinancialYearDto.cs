using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.FinancialYears.Dto
{
    [AutoMapFrom(typeof(FinancialYear))]
    public class FinancialYearDto: FullAuditedEntityDto
    {      
        [Required]
        public virtual string Name { get; set; }

        public virtual Boolean IsActive { get; set; }
    }
}
