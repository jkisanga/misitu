using Abp.Domain.Entities.Auditing;
using Misitu.RefereneceTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Activities
{
    [Table("Activities")]
    public class Activity : FullAuditedEntity
    {
        [Required]
        public virtual string Description { get; set; }
        public virtual int RefServiceCategoryId { get; set; }

        [Required]
        public virtual double Fee { get; set; }

        public virtual double RegistrationFee { get; set; }

    }
}
