using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources.Dto
{
    [AutoMapFrom(typeof(RefSubRevenueSource))]
    public class CreateRefSubRevenueSourcesInput : FullAuditedEntityDto
    {
        public virtual int RevenueResourceId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual Decimal Royalty { get; set; }
        public virtual Decimal TaFF { get; set; }
        public virtual Decimal VAT { get; set; }
        public virtual Decimal CESS { get; set; }
        public virtual Decimal TREE { get; set; }
        public virtual Decimal LMDA { get; set; }
    }
}
