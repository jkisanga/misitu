using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources.Dto
{
    [AutoMapFrom(typeof(MainRevenueSource))]
   public class CreateMainRevenueSource
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual int ParentId { get; set; }
    }
}
