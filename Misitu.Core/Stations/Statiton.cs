﻿using Abp.Domain.Entities.Auditing;
using Misitu.Regions;
using Misitu.Zones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Stations
{
    [Table("Stations")]
    public class Statiton: FullAuditedEntity
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Address { get; set; }
        [Required]
        public virtual int ZoneId { get; set; }
        [Required]
        public virtual int RegionId { get; set; }
        public virtual int ParentId { get; set; }

        [ForeignKey("ZoneId")]
        public virtual Zone Zone { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }


    }
}
