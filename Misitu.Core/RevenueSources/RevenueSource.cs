using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources
{
    [Table("RevenueSources")]
    public class RevenueSource: FullAuditedEntity
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
    }
}
