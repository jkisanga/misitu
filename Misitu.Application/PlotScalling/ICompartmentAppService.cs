using Abp.Application.Services;
using Misitu.PlotScalling.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
    public interface ICompartmentAppService:IApplicationService
    {
        List<CompartmentDto> GetCompartments();

        Task CreateCompartment(CreateCompartmentInput input);

        List<CompartmentView> GetTalliedPlotsByRange(int id);

        CompartmentDto GetCompartment(int id);

        Task UpdateCompartment(CompartmentDto input);

        Task DeleteCompartmentAsync(CompartmentDto input);
    }
}
