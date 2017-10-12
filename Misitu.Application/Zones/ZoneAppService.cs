using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Misitu.Zones.Dto;
using Abp.Domain.Repositories;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.Zones
{
    public class ZoneAppService : MisituAppServiceBase, IZoneAppService
    {
        private readonly IRepository<Zone> _zoneRepository;

        public ZoneAppService(IRepository<Zone> zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        // zone list
        public List<ZoneDto> GetZones()
        {
            var zones = _zoneRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Description)
            .ToList();

            return new List<ZoneDto>(zones.MapTo<List<ZoneDto>>());
        }

        //create new zone

        public async Task CreateZone(CreateZoneInput input)
        {
            var zone = input.MapTo<Zone>();

            var ZoneExist = _zoneRepository.FirstOrDefault(p => p.Name == input.Name);
            if (ZoneExist == null)
            {
                await _zoneRepository.InsertAsync(zone);
            }
            else
            {
                throw new UserFriendlyException("There is already a Zone with given name");
            }
          
        }

        //get edit zone
        public ZoneDto GetZone(int id)
        {
            var zone =   _zoneRepository.FirstOrDefault(id);

            return zone.MapTo<ZoneDto>();
           
        }

        //update zone
        public async Task UpdateZone(ZoneDto input)
        {
            // here aoutomapping can be done;
            var zone = _zoneRepository.FirstOrDefault(input.Id);
            zone.Description = input.Description;
            zone.Name = input.Name;

            await _zoneRepository.UpdateAsync(zone);
        }

       
        //delete zone
        public async Task DeleteZoneAsync(ZoneDto input)
        {
            var zone = _zoneRepository.FirstOrDefault(input.Id);
            if (zone == null)
            {
                throw new UserFriendlyException("Zone not Found!");
            }

            await _zoneRepository.DeleteAsync(zone);
            
        }
    }
}
