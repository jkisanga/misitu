using Abp.Domain.Entities.Auditing;
using Misitu.Activities;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing
{
    [Table("BillItems")]
    public class BillItem : FullAuditedEntity
    {
        public virtual int BillId { get; set; }
        public virtual int ActivityId { get; set; }

        [Required]
        public virtual string Description { get; set; }

        public virtual double EquvAmont { get; set; }

        public virtual double MiscAmont { get; set; }

        public virtual int GfsCode { get; set; }

        public virtual double Loyality { get; set; }
        public virtual double TFF { get; set; }
        public virtual double LMDA { get; set; }
        public virtual double VAT { get; set; }
        public virtual double CESS { get; set; }
        public virtual double TP { get; set; }
        public virtual double DataSheet { get; set; }
        public virtual double Others { get; set; }
        public virtual double Total { get; set; }
        public virtual Double EquvAmont { get; set; }
        public virtual Double MiscAmont { get; set; }
        public virtual int GfsCode { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
    }
}
