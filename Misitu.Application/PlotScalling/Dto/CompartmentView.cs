using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling.Dto
{
    [AutoMapFrom(typeof(Compartment))]
    public class CompartmentView
    {

       public virtual string Name { get; set; }      
       public virtual int RangeId { get; set; }
       public virtual IQueryable<PlotDto> plots { get; set; }

    }
}
