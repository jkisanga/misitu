using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Dto
{
    public class CustomTransitPassCheckpointDto
    {
        public int Id { get; set; }
        public int TransitPassId { get; set; }
        public string Checkpoint { get; set; }
        public bool InspectionStatus { get; set; }
        public string AdditionInformation { get; set; }
    }
}
