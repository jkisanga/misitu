using Abp.Application.Services;
using Misitu.PlotScalling.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
    public interface IPlotAppService:IApplicationService
    {
        List<PlotDto> GetPlots();

        List<PlotDto> GetPlotsByCompartment(int id);

        List<PlotDto> GetTalliedPlotsByCompartment(int id);
       
        int CreatePlot(CreatePlotInput input);

        PlotDto GetPlot(int id);

        Task UpdatePlotAllocation(PlotDto input);

        Task UpdatePlot(PlotDto input);

        Task DeletePlotAsync(PlotDto input);
    }
}

