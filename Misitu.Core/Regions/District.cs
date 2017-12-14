using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Regions
{
  public  class District : FullAuditedEntity
    {
        public virtual int RegionId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}
