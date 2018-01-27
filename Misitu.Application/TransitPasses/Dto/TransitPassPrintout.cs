using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Dto
{
    public class TransitPassPrintout
    {
        public virtual int Id { get; set; }
        public virtual int BillId { get; set; }
        public virtual string Applicant { get; set; }
        public virtual string StationName { get; set; }
        public virtual string StationAddress { get; set; }
        public virtual string OrginalCountry { get; set; }
        public virtual string NoOfConsignment { get; set; }
        public virtual string LisenceNo { get; set; }
        public virtual string TransitPassNo { get; set; }
        public virtual int SourceForest { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual string SourceName { get; set; }
        public virtual string DestinationName { get; set; }
        public virtual string VehcleNo { get; set; }   
        public virtual string HummerNo { get; set; }
        public virtual string HummerMaker { get; set; }
        public virtual string HummerStation { get; set; }
        public virtual string AdditionInformation { get; set; }
        public virtual string CreatedUser { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual int ItemId { get; set; }
        public virtual string ItemDescription { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int CheckpointId { get; set; }
        public virtual string CheckpointName { get; set; }

    }
}
