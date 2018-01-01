using Abp.Application.Services;
using Misitu.Applicants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Interface
{
    public interface IApplicationTypeService : IApplicationService
    {
        List<ApplicationTypeDto> GetRefApplicationTypes();
    }
}
