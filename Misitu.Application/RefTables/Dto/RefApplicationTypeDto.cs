﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Dto
{
    [AutoMapFrom(typeof(RefApplicationTypeDto))]
    class RefApplicationTypeDto : FullAuditedEntityDto
    {

    }
}
