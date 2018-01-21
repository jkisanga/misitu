using Abp.Domain.Entities.Auditing;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.ExportImport
{
  public  class ExportSpecie : FullAuditedEntity
    {

        [Required]
        public virtual int ExportDetailId { get; set; }

        [Required]
        public virtual int SpecieId { get; set; }

        [Required]
        public virtual string Quantity { get; set; }

        [Required]
        public virtual string Size { get; set; }

        [Required]
        public virtual double Price { get; set; }

        [ForeignKey("ExportDetailId")]
        public virtual ExportDetail ExportDetail { get; set; }

        [ForeignKey("SpecieId")]
        public virtual Specie Specie { get; set; }
    }
}
