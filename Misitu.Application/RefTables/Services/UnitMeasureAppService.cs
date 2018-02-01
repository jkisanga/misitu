using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Misitu.RefereneceTables;
using Misitu.RefTables.Dto;
using Misitu.RefTables.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Services
{
    public class UnitMeasureAppService: MisituAppServiceBase, IUnitMeasureAppService
    {
        private readonly IRepository<RefUnitMeasure> _unitMeasureRepository;

        public UnitMeasureAppService(IRepository<RefUnitMeasure> unitMeasureRepository)
        {
            _unitMeasureRepository = unitMeasureRepository;
        }

        // Region list
        public List<UnitMeasureDto> GetUnitMeasures()
        {
            var unitMeasure = _unitMeasureRepository
            .GetAll()
            .OrderBy(p => p.Name)
            .ToList();

            return new List<UnitMeasureDto>(unitMeasure.MapTo<List<UnitMeasureDto>>());
        }

        //create new Region

        public async Task CreateUnitMeasure(CreateUnitMeasureInput input)
        {
            var unitMeasure = input.MapTo<RefUnitMeasure>();

            var IsExist = _unitMeasureRepository.FirstOrDefault(p => p.Name == input.Name);
            if (IsExist == null)
            {
                await _unitMeasureRepository.InsertAsync(unitMeasure);
            }
            else
            {
                throw new UserFriendlyException("There is already a UnitMeasure with given name");
            }

        }

        //get edit Region
        public UnitMeasureDto GetUnitMeasure(int id)
        {
            var UnitMeasure = _unitMeasureRepository.FirstOrDefault(id);

            return UnitMeasure.MapTo<UnitMeasureDto>();

        }

        //update Region
        public async Task UpdateUnitMeasure(UnitMeasureDto input)
        {
            // here aoutomapping can be done;
            var UnitMeasure = _unitMeasureRepository.FirstOrDefault(input.Id);
            UnitMeasure.Name = input.Name;

            await _unitMeasureRepository.UpdateAsync(UnitMeasure);
        }


        //delete Region
        public async Task DeleteUnitMeasure(UnitMeasureDto input)
        {
            var UnitMeasure = _unitMeasureRepository.FirstOrDefault(input.Id);
            if (UnitMeasure == null)
            {
                throw new UserFriendlyException("UnitMeasure not Found!");
            }

            await _unitMeasureRepository.DeleteAsync(UnitMeasure);

        }
    }
}
