using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Misitu.Regions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Regions
{
    public interface IRegionAppService : IApplicationService
    {
        List<RegionDto> GetRegions();

        Task CreateRegion(CreateRegionInput input);

        RegionDto GetRegion(int id);

        Task UpdateRegion(RegionDto input);

        Task DeleteRegionAsync(RegionDto input);
    }
}
