using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Dto
{
   public class CustomTransitPassItemDto
    {
        public int Id { get; set;}
        public int TransitPassId { get; set; }
        public string Item { get; set; }
        public string UnitMeasure { get; set; }
        public string Specie { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
    }
}
