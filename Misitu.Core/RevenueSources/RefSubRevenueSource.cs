using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources
{
    [Table("RefSubRevenueSources")]
    public class RefSubRevenueSource: FullAuditedEntity
    {
        public virtual int RevenueResourceId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual Decimal Royalty { get; set; }
        public virtual Decimal TaFF { get; set; }
        public virtual Decimal VAT { get; set; }
        public virtual Decimal CESS { get; set; }
        public virtual Decimal TREE { get; set; }
        public virtual Decimal LMDA { get; set; }
    }
}
