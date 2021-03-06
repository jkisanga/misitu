﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Misitu.Ranges;
using Misitu.Divisions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Ranges.Dto
{
    [AutoMapFrom(typeof(Range))]
    public class RangeDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual int DivisionId { get; set; }

        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }
    }
}
