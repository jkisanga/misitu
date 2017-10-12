using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing.Dto
{
    public class AllocatedPlotView
    {
        public virtual int Id { get; set; }
        public virtual int DealerId { get; set; }
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
    }
}
