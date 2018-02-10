using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;
using Misitu.Regions;
using Misitu.Districts.Dto;

namespace Misitu.Districts
{
    public class DistrictAppService : MisituAppServiceBase, IDistrictAppService
    {
        private readonly IRepository<District> _districtRepository;

        public DistrictAppService(IRepository<District> districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public void CreateDistrict(CreateDistrictInput input)
        {
            
            var district = new District
            {
                Name = input.Name,
                RegionId = input.RegionId
            };

            var existing = _districtRepository.FirstOrDefault(p => p.Name == input.Name);
            if (existing == null)
            {
                _districtRepository.InsertAsync(district);
            }
            else
            {
                throw new UserFriendlyException("There is already a District with given name");
            }
        }

        public void DeleteDistrict(DistrictDto input)
        {
            var district = _districtRepository.FirstOrDefault(input.Id);
            if (district == null)
            {
                throw new UserFriendlyException("District not Found!");
            }
            _districtRepository.Delete(district);
        }

        public DistrictDto GetDistrict(int id)
        {
            var district = _districtRepository.FirstOrDefault(id);

            return district.MapTo<DistrictDto>();
        }

        public List<DistrictDto> GetDistricts()
        {
            var districts = _districtRepository
                 .GetAll()
                 .OrderBy(p => p.Name)
                 .ToList();

            return new List<DistrictDto>(districts.MapTo<List<DistrictDto>>());
        }

        public List<DistrictDto> GetDistrictsByRegionId(int Id)
        {
            var districts = _districtRepository
                    .GetAll()
                    .Where(d => d.RegionId == Id)
                    .OrderBy(p => p.Name)
                    .ToList();

            return new List<DistrictDto>(districts.MapTo<List<DistrictDto>>());
        }

        public void UpdateDistrict(DistrictDto input)
        {
            var district = _districtRepository.FirstOrDefault(input.Id);
            district.Name = input.Name;
            district.RegionId = input.RegionId;

             _districtRepository.Update(district);
        }   
    }
}
