using Abp.AutoMapper;
using Misitu.Applicants.ExportImport;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Dto.ExportImport
{
   [AutoMapFrom(typeof(ExportAttachment))]
   public  class CreateExportAttachment
    {
        [Required]
        public virtual int ExportDetailId { get; set; }

        public virtual string BrelaRegistrationCert { get; set; }

        public virtual string LicenceCert { get; set; }

        public virtual string TaxClearanceCert { get; set; }

        public virtual string EnquiryOrder { get; set; }

        public virtual string ExportReturns { get; set; }

        public virtual string ForestProduceRegCert { get; set; }

        public virtual string AutholizedLetter { get; set; }

        public virtual string SawMillerContract { get; set; }

        public virtual string MouCert { get; set; }

        [ForeignKey("ExportDetailId")]
        public virtual ExportDetail ExportDetail { get; set; }
    }
}
