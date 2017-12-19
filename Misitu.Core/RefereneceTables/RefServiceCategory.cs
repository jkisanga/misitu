using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefereneceTables
{
   public class RefServiceCategory : FullAuditedEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
       
    }
}
