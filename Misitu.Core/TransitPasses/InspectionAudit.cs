using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
  public  class InspectionAudit : FullAuditedEntity 
    {
        public virtual int CheckPointTransitPassId { get; set; }
        public virtual string Action { get; set; }
        public virtual string AdditionInformation { get; set; }


        [ForeignKey("CheckPointTransitPassId")]
        public virtual CheckPointTransitPass CheckPointTransitPass { get; set; }
    }
}
