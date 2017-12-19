using Abp.Domain.Entities.Auditing;
using Misitu.Licensing;
using Misitu.PlotScalling;
using Misitu.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Harvesting
{
    [Table("HarvestingLogs")]
    public class HarvestingLog:FullAuditedEntity
    {
        public virtual int PlotId { get; set; }
        public virtual int DealerId { get; set; }
        public virtual int LicenseId { get; set; }

        [Required]
        public virtual string TruckNumber { get; set; }

        [Required]
        public virtual string DriverName { get; set; }

        [Required]
        public virtual double AccumulativeCBM { get; set; }

        [ForeignKey("PlotId")]
        public virtual Plot Plot { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealer Dealer { get; set; }

        [ForeignKey("LicenseId")]
        public virtual License License { get; set; }

    }
}
