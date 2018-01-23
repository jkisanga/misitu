using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Misitu.Applicants.ExportImport;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Dto.ExportImport
{
    [AutoMapFrom(typeof(ExportSpecie))]
    public class ExportSpecieDto:FullAuditedEntity
    {      
        public virtual int ExportDetailId { get; set; }

        public virtual int SpecieId { get; set; }

        public virtual string Quantity { get; set; }

        public virtual string Size { get; set; }

        public virtual double Price { get; set; }

        [ForeignKey("ExportDetailId")]
        public virtual ExportDetail ExportDetail { get; set; }

        [ForeignKey("SpecieId")]
        public virtual Specie Specie { get; set; }
    }
}
