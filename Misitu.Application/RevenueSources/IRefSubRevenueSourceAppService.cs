using Abp.Application.Services;
using Misitu.RevenueSources.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RevenueSources
{
    public interface IRefSubRevenueSourceAppService: IApplicationService
    {
        List<RefSubRevenueSourcesDto> GetRefSubRevenueResources(int Id);

        Task CreateRevenueResource(CreateRevenueSourcesInput input);

        RefSubRevenueSourcesDto GetRefSubRevenueResource(int id);

        Task UpdateRefSubRevenueResource(RefSubRevenueSourcesDto input);

        Task DeleteRefSubRevenueResourceAsync(RefSubRevenueSourcesDto input);

       //List<RefSubRevenueSource> getRefSubRevenueSources()
    }
}
