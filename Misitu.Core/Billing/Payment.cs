using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing
{
 public   class Payment : FullAuditedEntity
    {
        public virtual int BillId { get; set; }
        public virtual string PaymentControlNo { get; set; }
        public virtual Double BillAmount { get; set; }
        public virtual Double PaidAmount { get; set; }
        public virtual string PayOption { get; set; }
        public virtual string Currency { get; set; }
        public virtual DateTime TranDate { get; set; }
        public virtual string UsedPayChannel { get; set; }
        public virtual string PayerCellNo { get; set; }
        public virtual string PayerName { get; set; }
        public virtual string PayerEmail { get; set; }
        public virtual string PspReceiptNo { get; set; }
        public virtual string PspName { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }


    }
}
