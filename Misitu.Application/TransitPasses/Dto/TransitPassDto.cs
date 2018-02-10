using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.Applicants;
using Misitu.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
    [AutoMapFrom(typeof(TransitPass))]
    public class TransitPassDto: FullAuditedEntityDto
    {
        

        public virtual int ApplicantId { get; set; }
        public virtual int BillId { get; set; }
        public virtual string OrginalCountry { get; set; }
        public virtual string NoOfConsignment { get; set; }
        public virtual string LisenceNo { get; set; }
        public virtual string TransitPassNo { get; set; }
        public virtual string RegistrationNo { get; set; }
        public virtual int StationId { get; set; }//source forest
        public virtual int DistrictId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public virtual DateTime IssuedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
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



        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }

        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }


    }
}
