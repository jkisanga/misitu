using Abp.Domain.Entities.Auditing;
using Misitu.FinancialYears;
using Misitu.Species;
using Misitu.Stations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.ExportImport
{
   public class ExportDetail : FullAuditedEntity
    {
        [Required]
        public virtual int ApplicantId { get; set; }

        [Required]
        public virtual int SpecieCategoryId { get; set; }

        [Required]
        public virtual int StationId { get; set; }

        [Required]
        public virtual int FinancialYearId { get; set; }

        [Required]
        public virtual string BankName { get; set; }

        [Required]
        public virtual string BankAddress { get; set; }

        [Required]
        public virtual string Destination { get; set; }

        [Required]
        public virtual DateTime ShipmentDate { get; set; }

        [ForeignKey("ApplicantId")]
        public virtual Applicant Applicant { get; set; }

        [ForeignKey("SpecieCategoryId")]
        public virtual SpecieCategory SpecieCategory { get; set; }

        [ForeignKey("StationId")]
        public virtual Statiton Station { get; set; }

        [ForeignKey("FinancialYearId")]
        public virtual FinancialYear FinancialYear { get; set; }


    }
}
