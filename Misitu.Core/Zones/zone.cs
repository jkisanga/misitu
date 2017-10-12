using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Zones
{
    [Table("Zones")]
    public class Zone : FullAuditedEntity
    {
       
        [Required]
      
        public virtual string Name { get; set; }

        [Required]
      
        public virtual string Description { get; set; }
    }
}
