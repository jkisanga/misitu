using Abp.Application.Services;
using Misitu.Stations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Stations
{
    public interface IStationAppService: IApplicationService
    {
        List<StationDto> GetStations();

        Task CreateStation(CreateStationInput input);

        StationDto GetStation(int id);

        Task UpdateStation(StationDto input);

        Task DeleteStationAsync(StationDto input);
    }
}
