using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Misitu.Stations.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.Stations
{
    public class StationAppService : MisituAppServiceBase, IStationAppService
    {
        private readonly IRepository<Statiton> _stationRepository;

        public StationAppService(IRepository<Statiton> stationRepository)
        {
            _stationRepository = stationRepository;
        }
        public async Task CreateStation(CreateStationInput input)
        {
            //var station = input.MapTo<Statiton>();

            var station = new Statiton
            {
                Name = input.Name,
                Address = input.Address,
                ZoneId = input.ZoneId,
                RegionId = input.RegionId
            };

            var existingStation = _stationRepository.FirstOrDefault(p => p.Name == input.Name);
            if (existingStation == null)
            {
                await _stationRepository.InsertAsync(station);
            }
            else
            {
                throw new UserFriendlyException("There is already a Station with given name");
            }
        }

        public async Task DeleteStationAsync(StationDto input)
        {
            var station = _stationRepository.FirstOrDefault(input.Id);
            if (station == null)
            {
                throw new UserFriendlyException("Station Year not Found!");
            }
            await _stationRepository.DeleteAsync(station);
        }

        public StationDto GetStation(int id)
        {
            var station = _stationRepository.FirstOrDefault(id);

            return station.MapTo<StationDto>();
        }

        public List<StationDto> GetStations()
        {
            var stations = _stationRepository
                  .GetAll()
                  .OrderBy(p => p.Name)
                  .ToList();

            return new List<StationDto>(stations.MapTo<List<StationDto>>());
        }

        public async Task UpdateStation(StationDto input)
        {
            var station = _stationRepository.FirstOrDefault(input.Id);
            station.Name = input.Name;
            station.Address = input.Address;
            station.ZoneId = input.ZoneId;
            station.RegionId = input.RegionId;

            await _stationRepository.UpdateAsync(station);
        }
    }
}
