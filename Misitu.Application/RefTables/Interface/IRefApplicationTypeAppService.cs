using Abp.Application.Services;
using Misitu.RefTables.Dto;
using Misitu.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Interface
{
    public interface IRefApplicationTypeAppService :IApplicationService
    {

       List<RefApplicationTypeDto> GetRefApplicationTypes();

        Task CreateApplicationTypeAsync(CreateRefApplicationInput input);

        RefApplicationTypeDto GetApplicationTypeById(int id);

        Task UpdateApplicationType(RefApplicationTypeDto input);

        Task DeleteApplicationAsync(RefApplicationTypeDto input);
        List<District> GetDistrictList();
    }
}
