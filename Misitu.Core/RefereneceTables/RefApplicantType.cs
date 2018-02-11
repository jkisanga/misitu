using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefereneceTables
{
    [Table("RefApplicantTypes")]
  public  class RefApplicantType : FullAuditedEntity
    {
        
        [Required]
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }

    }
}
