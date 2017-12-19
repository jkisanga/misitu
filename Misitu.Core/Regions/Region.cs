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
    [Table("Regions")]
    public class Region : FullAuditedEntity
    {
        [Required]
        public virtual string Name { get; set; }
    }
}
