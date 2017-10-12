using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Misitu.Regions.Dto;
using Abp.Domain.Repositories;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.Regions
{
    public class RegionAppService : MisituAppServiceBase, IRegionAppService
    {
        private readonly IRepository<Region> _regionRepository;

        public RegionAppService(IRepository<Region> regionRepository)
        {
            _regionRepository = regionRepository;
        }

        // Region list
        public List<RegionDto> GetRegions()
        {
            var Regions = _regionRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .ToList();

            return new List<RegionDto>(Regions.MapTo<List<RegionDto>>());
        }

        //create new Region

        public async Task CreateRegion(CreateRegionInput input)
        {
            var Region = input.MapTo<Region>();

            var RegionExist = _regionRepository.FirstOrDefault(p => p.Name == input.Name);
            if (RegionExist == null)
            {
                await _regionRepository.InsertAsync(Region);
            }
            else
            {
                throw new UserFriendlyException("There is already a Region with given name");
            }
          
        }

        //get edit Region
        public RegionDto GetRegion(int id)
        {
            var Region =   _regionRepository.FirstOrDefault(id);

            return Region.MapTo<RegionDto>();
           
        }

        //update Region
        public async Task UpdateRegion(RegionDto input)
        {
            // here aoutomapping can be done;
            var Region = _regionRepository.FirstOrDefault(input.Id);
            Region.Name = input.Name;

            await _regionRepository.UpdateAsync(Region);
        }

       
        //delete Region
        public async Task DeleteRegionAsync(RegionDto input)
        {
            var Region = _regionRepository.FirstOrDefault(input.Id);
            if (Region == null)
            {
                throw new UserFriendlyException("Region not Found!");
            }

            await _regionRepository.DeleteAsync(Region);
            
        }
    }
}
