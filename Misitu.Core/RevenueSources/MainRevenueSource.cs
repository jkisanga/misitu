using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources
{
    [Table("MainRevenueSources")]
   public class MainRevenueSource : FullAuditedEntity
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual int ParentId { get; set; }
    }
}
