using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.Applicants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
   public class CreateTransitPass
    {
        [AutoMapFrom(typeof(TransitPass))]
        public virtual int ApplicantId { get; set; }
        public virtual string OrginalCountry { get; set; }
        public virtual string NoOfConsignment { get; set; }
        public virtual int LisenceNo { get; set; }
        public virtual string TransitPassNo { get; set; }
        public virtual int SourceForest { get; set; }
        public virtual DateTime IssuedDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual string SourceName { get; set; }
        //Destnation pull form District
        public virtual int DestinationId { get; set; }
        public virtual string DestinationName { get; set; }
        public virtual string VehcleNo { get; set; }
        public virtual int IssuerOfficer { get; set; }
        public virtual string HummerNo { get; set; }
        public virtual string HummerMaker { get; set; }
        public virtual string HummerStationId { get; set; }
        public virtual string AdditionInformation { get; set; }



        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }


    }
}
