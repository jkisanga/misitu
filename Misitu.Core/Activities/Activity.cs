using Abp.Domain.Entities.Auditing;
using Misitu.RefereneceTables;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Activities
{
    [Table("Activities")]
    public class Activity : FullAuditedEntity
    {
        
        public virtual int RevenueSourceId { get; set; }
     
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }
      
        public virtual double Fee { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual string Flag { get; set; }

        public virtual double RegistrationFee { get; set; }
        [ForeignKey("RevenueSourceId")]
        public virtual RevenueSource RevenueSource { get; set; }
    }
}
