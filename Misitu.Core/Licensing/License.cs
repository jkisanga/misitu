using Abp.Domain.Entities.Auditing;
using Misitu.Billing;
using Misitu.FinancialYears;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing
{
    [Table("Licenses")]
    public class License:FullAuditedEntity
    {
        public virtual string serialNumber { get; set; }
        public virtual int StationId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual int BillId { get; set; }
        public virtual string Location { get; set; }
        public virtual DateTime? PaidDate { get; set; }
        public virtual string ReceiptNumber { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual string IssuedBy { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
    }
}
