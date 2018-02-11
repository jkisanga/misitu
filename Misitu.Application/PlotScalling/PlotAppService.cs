using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.PlotScalling.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.PlotScalling
{
    public class PlotAppService : MisituAppServiceBase, IPlotAppService
    {
        private readonly IRepository<Compartment> _compartmentRepository;
        private readonly IRepository<Plot> _plotRepository;
        private readonly IRepository<TallySheet> _tallySheetRepository;

        public PlotAppService(
            IRepository<Plot> plotRepository, 
            IRepository<TallySheet> tallySheetRepository,
            IRepository<Compartment> compartmentRepository
            )
        {
            _plotRepository = plotRepository;
            _tallySheetRepository = tallySheetRepository;
            _compartmentRepository = compartmentRepository;
        }

        public int CreatePlot(CreatePlotInput input)
        {
            var plot = new Plot
            {
                Name = input.Name,
                CompartmentId = input.CompartmentId
            };

          
            if (_plotRepository.FirstOrDefault(p => p.Name == input.Name && p.CompartmentId == input.CompartmentId) == null)
            {
                // _plotRepository.InsertAndGetId(plot);

                return _plotRepository.InsertAndGetId(plot);
            }
            else
            {
                throw new UserFriendlyException("There is already a Plot with given name");
            }
        }

        public async Task DeletePlotAsync(PlotDto input)
        {
            var plot = _plotRepository.FirstOrDefault(input.Id);
            if (plot == null)
            {
                throw new UserFriendlyException("Plot Year not Found!");
            }
            await _plotRepository.DeleteAsync(plot);
        }

        public PlotDto GetPlot(int id)
        {
            var plot = _plotRepository.FirstOrDefault(id);

            return plot.MapTo<PlotDto>();
        }

        public List<PlotDto> GetPlots()
        {
          

            var plots = _plotRepository
                 .GetAll()
                 .OrderBy(p => p.Name)
                 .ToList();

            return new List<PlotDto>(plots.MapTo<List<PlotDto>>());
        }

        public List<PlotDto> GetPlotsByCompartment(int id)
        {
            var plots = (from sheet in _tallySheetRepository.GetAll()
                         join plot in _plotRepository.GetAll() on sheet.PlotId equals plot.Id
                         where plot.CompartmentId == id
                         orderby plot.Name
                         group sheet by sheet.PlotId into g
                         select new PlotDto
                         {
                             Id = g.Key,
                             isAllocated = g.Select( x => x.Plot.IsAllocated).FirstOrDefault(),
                             CompartmentId = id,
                             Name = g.Select(x => x.Plot.Name).FirstOrDefault(),
                             Trees = g.Sum(t => t.TreesNumber),
                             Volume = g.Sum(t => t.Volume),
                             Loyality = g.Sum(t => t.Loyality),
                             TFF = g.Sum(t => t.TFF),
                             LMDA = g.Sum(t => t.LMDA),
                             CESS = g.Sum(t => t.CESS),
                             VAT = g.Sum(t => t.VAT),
                             TP = g.Sum(t => t.TP),
                             TOTAL = g.Sum(t => t.TOTAL)
                         }

                            ).ToList();

            return new List<PlotDto>(plots.MapTo<List<PlotDto>>());
        }

       

        //List of Tallied plots by compartmnent
        public List<PlotDto> GetTalliedPlotsByCompartment(int id)
        {
        

            var plots = (from sheet in _tallySheetRepository.GetAll()
                         join plot in _plotRepository.GetAll() on sheet.PlotId equals plot.Id
                         where plot.CompartmentId == id
                         where plot.IsAllocated == false
                         orderby plot.Name
                         group sheet by sheet.PlotId into g
                         select new PlotDto
                         {
                             Id = g.Key,
                             CompartmentId = id,
                             Name = g.Select(x => x.Plot.Name).FirstOrDefault(),
                             Trees = g.Sum(t => t.TreesNumber),
                             Volume = g.Sum(t => t.Volume),
                             Loyality = g.Sum(t => t.Loyality),
                             TFF = g.Sum(t => t.TFF),
                             LMDA = g.Sum(t => t.LMDA),
                             CESS = g.Sum(t => t.CESS),
                             VAT = g.Sum(t => t.VAT),
                             TP = g.Sum(t => t.TP),
                             TOTAL = g.Sum(t => t.TOTAL)
                         }

                          ).ToList();

            return new List<PlotDto>(plots.MapTo<List<PlotDto>>());
        }

        public async Task UpdatePlot(PlotDto input)
        {
            var plot = _plotRepository.FirstOrDefault(input.Id);
            plot.Name = input.Name;
            plot.CompartmentId = input.CompartmentId;

            await _plotRepository.UpdateAsync(plot);
        }

        //update allocated plot
        public async Task UpdatePlotAllocation(PlotDto input)
        {
            var plot = _plotRepository.FirstOrDefault(input.Id);
            plot.IsAllocated = true;
            await _plotRepository.UpdateAsync(plot);

        }
    }
}
