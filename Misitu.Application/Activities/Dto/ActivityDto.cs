using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.RefereneceTables;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Activities.Dto
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityDto:FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int RevenueSourceId { get; set; }

        [Required]
        public virtual double Fee { get; set; }

        public virtual double RegistrationFee { get; set; }

        [ForeignKey("RevenueSourceId")]
        public virtual RevenueSource Revenue { get; set; }
    }
}
