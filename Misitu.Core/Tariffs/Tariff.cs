using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Tariffs
{
    [Table("Tariffs")]
    public class Tariff : FullAuditedEntity
    {
        [Required]
        public virtual int DBH { get; set; }
        [Required]
        public virtual double T40 { get; set; }
        [Required]
        public virtual double T41 { get; set; }
        [Required]
        public virtual double T42 { get; set; }
        [Required]
        public virtual double T43 { get; set; }
        [Required]
        public virtual double T44 { get; set; }
        [Required]
        public virtual double T45 { get; set; }
        [Required]
        public virtual double T46 { get; set; }
        [Required]
        public virtual double T47 { get; set; }
        [Required]
        public virtual double T48 { get; set; }
        [Required]
        public virtual double T49 { get; set; }
        [Required]
        public virtual double T50 { get; set; }
        [Required]
        public virtual double T51 { get; set; }
        [Required]
        public virtual double T52 { get; set; }
        [Required]
        public virtual double T53 { get; set; }
        [Required]
        public virtual double T54 { get; set; }
        [Required]
        public virtual double T55 { get; set; }
        [Required]
        public virtual double T56 { get; set; }
        [Required]
        public virtual double T57 { get; set; }
        [Required]
        public virtual double T58 { get; set; }
        [Required]
        public virtual double T59 { get; set; }
        [Required]
        public virtual double T60 { get; set; }
        [Required]
        public virtual double T61 { get; set; }
        [Required]
        public virtual double T62 { get; set; }
        [Required]
        public virtual double T63 { get; set; }
        [Required]
        public virtual double T64 { get; set; }
        [Required]
        public virtual double T65 { get; set; }
        [Required]
        public virtual double T66 { get; set; }
        [Required]
        public virtual double T67 { get; set; }
        [Required]
        public virtual double T68 { get; set; }
        [Required]
        public virtual double T69 { get; set; }
        [Required]
        public virtual double T70 { get; set; }
        [Required]
        public virtual double T71 { get; set; }
        [Required]
        public virtual double T72 { get; set; }  

    }
}
