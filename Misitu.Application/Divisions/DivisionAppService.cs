using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Misitu.Divisions.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;


namespace Misitu.Divisions
{
    public class DivisionAppService : MisituAppServiceBase, IDivisionAppService
    {
        private readonly IRepository<Division> _divisionRepository;

        public DivisionAppService(IRepository<Division> divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }
        public async Task CreateDivision(CreateDivisionInput input)
        {
            //var Division = input.MapTo<Division>();

            var division = new Division
            {
                Name = input.Name,
                StationId = input.StationId
            };

            var existingDivision = _divisionRepository.FirstOrDefault(p => p.Name == input.Name && p.StationId == input.StationId);
            if (existingDivision == null)
            {
                await _divisionRepository.InsertAsync(division);
            }
            else
            {
                throw new UserFriendlyException("There is already a Division with given name for the same station");
            }
        }

        public async Task DeleteDivisionAsync(DivisionDto input)
        {
            var division = _divisionRepository.FirstOrDefault(input.Id);
            if (division == null)
            {
                throw new UserFriendlyException("Division Year not Found!");
            }
            await _divisionRepository.DeleteAsync(division);
        }

        public DivisionDto GetDivision(int id)
        {
            var division = _divisionRepository.FirstOrDefault(id);

            return division.MapTo<DivisionDto>();
        }

        public List<DivisionDto> GetDivisions()
        {
            var divisions = _divisionRepository
                  .GetAll()
                  .OrderBy(p => p.Name)
                  .ToList();

            return new List<DivisionDto>(divisions.MapTo<List<DivisionDto>>());
        }

        public async Task UpdateDivision(DivisionDto input)
        {
            var division = _divisionRepository.FirstOrDefault(input.Id);
            division.Name = input.Name;
            division.StationId = input.StationId;
        
            await _divisionRepository.UpdateAsync(division);
        }
    }
}
