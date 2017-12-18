using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefereneceTables
{
  public  class RefIdentityType : FullAuditedEntity
    {
        public virtual string Name { get; set; }

    }
}
