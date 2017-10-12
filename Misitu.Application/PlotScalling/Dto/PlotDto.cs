using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
    [AutoMapFrom(typeof(Plot))]
    public class PlotDto : FullAuditedEntity
    {
        [Required]
        public virtual int CompartmentId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public virtual int Trees { get; set; }
        public virtual double Volume { get; set; }
        public virtual double Loyality { get; set; }
        public virtual double TFF { get; set; }
        public virtual double LMDA { get; set; }
        public virtual double CESS { get; set; }
        public virtual double VAT { get; set; }
        public virtual double TP { get; set; }
        public virtual double TOTAL { get; set; }

        [ForeignKey("CompartmentId")]
        public virtual Compartment Compartment { get; set; }
    }
    
}
