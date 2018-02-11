using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Misitu.Ranges.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.Ranges
{
    public class RangeAppService : MisituAppServiceBase, IRangeAppService
    {
        private readonly IRepository<Range> _rangeRepository;

        public RangeAppService(IRepository<Range> rangeRepository)
        {
            _rangeRepository = rangeRepository;
        }
        public async Task CreateRange(CreateRangeInput input)
        {
            //var Range = input.MapTo<Range>();

            var range = new Range
            {
                Name = input.Name,
                DivisionId = input.DivisionId
            };

            var existingRange = _rangeRepository.FirstOrDefault(p => p.Name == input.Name);
            if (existingRange == null)
            {
                await _rangeRepository.InsertAsync(range);
            }
            else
            {
                throw new UserFriendlyException("There is already a Range with given name");
            }
        }

        public async Task DeleteRangeAsync(RangeDto input)
        {
            var range = _rangeRepository.FirstOrDefault(input.Id);
            if (range == null)
            {
                throw new UserFriendlyException("Range Year not Found!");
            }
            await _rangeRepository.DeleteAsync(range);
        }

        public RangeDto GetRange(int id)
        {
            var range = _rangeRepository.FirstOrDefault(id);

            return range.MapTo<RangeDto>();
        }

        public List<RangeDto> GetRanges()
        {
            var ranges = _rangeRepository
                  .GetAll()
                  .OrderBy(p => p.Name)
                  .ToList();

            return new List<RangeDto>(ranges.MapTo<List<RangeDto>>());
        }

        public async Task UpdateRange(RangeDto input)
        {
            var range = _rangeRepository.FirstOrDefault(input.Id);
            range.Name = input.Name;
            range.DivisionId = input.DivisionId;
        
            await _rangeRepository.UpdateAsync(range);
        }
    }
}
