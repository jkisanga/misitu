using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Licensing.Dto;
using Abp.Domain.Repositories;
using Misitu.PlotScalling;
using Misitu.FinancialYears;
using Abp.UI;
using Misitu.Registration.Dto;

namespace Misitu.Licensing
{
    public class AllocatedPlotAppService : MisituAppServiceBase, IAllocatedPlotAppService
    {
        private readonly IRepository<Compartment> _compartmentRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<Plot> _plotRepository;
        private readonly IRepository<TallySheet> _tallySheetRepository;
        private readonly IRepository<AllocatedPlot> _allocatedPlotRepository;

        public AllocatedPlotAppService(
            IRepository<Compartment> compartmentRepository,
              IRepository<Plot> plotRepository,
            IRepository<TallySheet> tallySheetRepository,
            IRepository<FinancialYear> financialYearRepository,
             IRepository<AllocatedPlot> allocatedPlotRepository
            )
        {
            _compartmentRepository = compartmentRepository;
            _financialYearRepository = financialYearRepository;
            _plotRepository = plotRepository;
            _tallySheetRepository = tallySheetRepository;
            _allocatedPlotRepository = allocatedPlotRepository;
        }

        public  Boolean CreateAllocatedPlot(CreateAllocatedPlotInput input)
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            if (current != null)
            {
                var plot = new AllocatedPlot
                {
                    DealerId = input.DealerId,
                    FinancialYearId = current.Id,
                    PlotId = input.PlotId,
                   
                };

                if(_allocatedPlotRepository.InsertAndGetId(plot) > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            
            }
            else
            {
                throw new UserFriendlyException("No Active Financial Year");
            }

        }

        public async Task DeleteAllocatedPlotAsync(AllocatedPlotDto input)
        {
            var allocatedPlot = _allocatedPlotRepository.FirstOrDefault(input.Id);
            if (allocatedPlot == null)
            {
                throw new UserFriendlyException("Range Year not Found!");
            }
            await _allocatedPlotRepository.DeleteAsync(allocatedPlot);
        }

        public AllocatedPlotDto GetAllocatedPlot(int id)
        {
            throw new NotImplementedException();
        }

        public List<AllocatedPlotDto> GetAllocatedPlots()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAllocatedPlot(AllocatedPlotDto input)
        {
            throw new NotImplementedException();
        }

        // Allocate plots by Dealer
        public List<AllocatedPlotView> GetAllocatedPlotsByDealer(DealerDto dealer)
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            var plots = (from sheet in _tallySheetRepository.GetAll()
                         join plot in _plotRepository.GetAll() on sheet.PlotId equals plot.Id
                         join allocated in _allocatedPlotRepository.GetAll() on plot.Id equals allocated.PlotId
                         where allocated.DealerId == dealer.Id
                         where allocated.FinancialYearId == current.Id
                         where allocated.IsPaid == false
                         orderby plot.Name
                         group sheet by sheet.PlotId into g
                         select new AllocatedPlotView
                         {
                             Id = g.Key,
                             DealerId = dealer.Id,
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

            return plots;
        }
    }
}
