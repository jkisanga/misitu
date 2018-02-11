using Abp.Domain.Entities.Auditing;
using Misitu.Activities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Registration
{
    [Table("DealerActivities")]
    public class DealerActivity: FullAuditedEntity
    {
        public virtual int DealerId { get; set; }
        public virtual int ActivityId { get; set; }

        [ForeignKey("DealerId")]
        public virtual Dealer Dealer { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; } 
    }
}
