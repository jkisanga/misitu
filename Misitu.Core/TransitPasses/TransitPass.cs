using Abp.Domain.Entities.Auditing;
using Misitu.Applicants;
using Misitu.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
   public class TransitPass: FullAuditedEntity
    {
        public TransitPass() { Status = false; }
        public virtual int ApplicantId { get; set; }
        public virtual int BillId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Phone { get; set; }
        public virtual string OrginalCountry { get; set; }
        public virtual string NoOfConsignment { get; set; }
        public virtual string LisenceNo { get; set; }
        public virtual string RegistrationNo { get; set; }
        public virtual int StationId { get; set; }//source forest
        public virtual int DistrictId { get; set; }
        public virtual string TransitPassNo { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual int ExpireDays { get; set; }
        public virtual string SourceName { get; set; }
        public virtual string DestinationName { get; set; }
        public virtual string VehcleNo { get; set; }
        public virtual int IssuerOfficer { get; set; }
        public virtual string HummerNo { get; set; }
        public virtual string HummerMaker { get; set; }
        public virtual string HummerStationId { get; set; }
        public virtual string AdditionInformation { get; set; }
        public virtual bool Status { get; set; }
        public virtual byte[] QRCode { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }

    }
}
