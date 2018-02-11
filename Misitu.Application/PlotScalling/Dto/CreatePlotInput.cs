using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling.Dto
{
    [AutoMapFrom(typeof(Plot))]
    public class CreatePlotInput 
    {
        [Required]
        public virtual int CompartmentId { get; set; }
        [Required]
        public virtual string Name { get; set; }

        [ForeignKey("CompartmentId")]
        public virtual Compartment Compartment { get; set; }
    }

}
