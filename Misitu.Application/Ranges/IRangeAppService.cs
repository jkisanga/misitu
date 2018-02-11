using Abp.Application.Services;
using Misitu.Ranges.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Ranges
{
    public interface IRangeAppService: IApplicationService
    {
        List<RangeDto> GetRanges();

        Task CreateRange(CreateRangeInput input);

        RangeDto GetRange(int id);

        Task UpdateRange(RangeDto input);

        Task DeleteRangeAsync(RangeDto input);
    }
}
