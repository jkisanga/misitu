using Abp.Application.Services;
using Misitu.Licensing.Dto;
using Misitu.Registration.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing
{
    public interface IAllocatedPlotAppService: IApplicationService
    {
        List<AllocatedPlotDto> GetAllocatedPlots();

        Boolean CreateAllocatedPlot(CreateAllocatedPlotInput input);

        List<AllocatedPlotView> GetAllocatedPlotsByDealer(DealerDto dealer);

        AllocatedPlotDto GetAllocatedPlot(int id);

        Task UpdateAllocatedPlot(AllocatedPlotDto input);

        Task DeleteAllocatedPlotAsync(AllocatedPlotDto input);
    }
}
