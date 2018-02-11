using Abp.Domain.Entities.Auditing;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
  public  class CheckPointTransitPass : FullAuditedEntity
    {
        public CheckPointTransitPass() { InspectionStatus = false; }

        public virtual int TransitPassId { get; set; }
        public virtual int StationId { get; set; }
        public virtual int InspectorId { get; set; }
        public virtual bool InspectionStatus { get; set; }
        public virtual string AdditionInformation { get; set; }




        [ForeignKey("TransitPassId")]
        public virtual TransitPass TransitPass { get; set; }
        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
    }
}
