using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Misitu.Zones.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Zones
{
    public interface IZoneAppService : IApplicationService
    {
        List<ZoneDto> GetZones();

        Task CreateZone(CreateZoneInput input);

        ZoneDto GetZone(int id);

        Task UpdateZone(ZoneDto input);

        Task DeleteZoneAsync(ZoneDto input);
    }
}
