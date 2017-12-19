using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.ForestProduce
{
   public class ForestProduceAppliedForest : FullAuditedEntity
    {
        public virtual int ForestProduceRegistrationId { get; set; }
        public virtual int StationId { get; set; }
        public virtual int FinancialYearId { get; set; }
        public virtual string Status { get; set; }

        [ForeignKey("ForestProduceRegistrationId")]
        public virtual ForestProduceRegistration ForestProduceRegistration { get; set; }
        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }
        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }
    }
}
