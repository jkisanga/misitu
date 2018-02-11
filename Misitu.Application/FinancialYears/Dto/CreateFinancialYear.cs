using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.FinancialYears.Dto
{
    [AutoMapTo(typeof(FinancialYear))]
    public class CreateFinancialYear
    {
        [Required]
        public virtual string Name { get; set; }

        public virtual Boolean IsActive { get; set; }
    }
}
