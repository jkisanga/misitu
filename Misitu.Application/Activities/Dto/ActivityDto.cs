using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Activities.Dto
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityDto:FullAuditedEntity
    {
        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual double Fee { get; set; }

        [Required]
        public virtual double RegistrationFee { get; set; }
    }
}
