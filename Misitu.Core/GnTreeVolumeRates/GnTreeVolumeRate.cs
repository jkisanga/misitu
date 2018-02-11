using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.GnTreeVolumeRates
{
    [Table("GnTreeVolumeRates")]
    public class GnTreeVolumeRate : FullAuditedEntity
    {
        public virtual int Dbh { get; set; }
        public virtual string Class { get; set; }
        public virtual double Amount { get; set; }
    }
}
