using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.FinancialYears
{
    [Table("FinancialYears")]
    public class FinancialYear : FullAuditedEntity
    {
        public FinancialYear()
        {
            IsActive = false;
        }

        [Required]

        public virtual string Name { get; set; }

        public virtual Boolean IsActive { get; set; }
    }
}
