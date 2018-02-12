﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Misitu.RefereneceTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Dto
{
    [AutoMapFrom(typeof(RefUnitMeasure))]
    public class UnitMeasureDto : FullAuditedEntityDto
    {
        [Required]
        public virtual string Name { get; set; }
    }
}