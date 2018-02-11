using Abp.Application.Services;
using Misitu.RevenueSources.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources
{
    public interface IRevenueSourceAppService: IApplicationService
    {
        List<RevenueSourcesDto> GetRevenueResources();

        Task CreateRevenueResource(CreateRevenueSourcesInput input);

        RevenueSourcesDto GetRevenueResource(int id);

        Task UpdateRevenueResource(RevenueSourcesDto input);

        Task DeleteRevenueResourceAsync(RevenueSourcesDto input);



    }
}
