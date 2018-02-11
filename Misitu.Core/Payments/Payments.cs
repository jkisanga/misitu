using Abp.Domain.Entities.Auditing;
using Misitu.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Payments
{
    [Table("Payments")]
    public class Payments: FullAuditedEntity
    {
        public virtual int BillId { get; set; }
        public virtual string ControlNumber { get; set; }
        public virtual Double BillAmount { get; set; }
        public virtual Double PaidAmount { get; set; }
        public virtual string Currency { get; set; }
        public virtual DateTime? PaidDate { get; set; }
        public virtual string PayerNo { get; set; }
        public virtual string PayerName { get; set; }
        public virtual string SpReceipt { get; set; }
        public virtual string SpName { get; set; }
        public virtual string PaymentReceipt { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }

    }
}
