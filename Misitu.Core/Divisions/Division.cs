using Abp.Domain.Entities.Auditing;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Divisions
{
    [Table("Divisions")]
    public class Division : FullAuditedEntity
    {
        [Required]

        public virtual string Name { get; set; }
        [Required]
        public virtual int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
    }
}
