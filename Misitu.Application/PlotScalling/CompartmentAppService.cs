using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.PlotScalling.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using Misitu.FinancialYears;

namespace Misitu.PlotScalling
{
    public class CompartmentAppService : MisituAppServiceBase, ICompartmentAppService
    {
        private readonly IRepository<Compartment> _compartmentRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<Plot> _plotRepository;
        private readonly IRepository<TallySheet> _tallySheetRepository;

        public CompartmentAppService(
            IRepository<Compartment> compartmentRepository,
              IRepository<Plot> plotRepository,
            IRepository<TallySheet> tallySheetRepository,
            IRepository<FinancialYear> financialYearRepository
            )
        {
            _compartmentRepository = compartmentRepository;
            _financialYearRepository = financialYearRepository;
            _plotRepository = plotRepository;
            _tallySheetRepository = tallySheetRepository;
        }

        public async Task CreateCompartment(CreateCompartmentInput input)
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = datevalue.Month.ToString();
            String currentYear = datevalue.Year.ToString();

            

            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            if (current != null)
            {
                int CalculatedAge = Convert.ToInt32(currentYear) - Convert.ToInt32(input.PlantedYear);
                var compartment = new Compartment
                {
                    Name = input.Name,
                    RangeId = input.RangeId,
                    FinancialYearId = current.Id,
                    Species = input.Species,
                    PlantedYear = input.PlantedYear,
                    Age = CalculatedAge,
                    Area = input.Area,
                    EstimatedVolume = input.EstimatedVolume,
                    Season = input.Season,
                    TariffNumber = input.TariffNumber
                };

                var existingCompartment = _compartmentRepository.FirstOrDefault(p => p.Name == input.Name);
                if (existingCompartment == null)
                {
                    await _compartmentRepository.InsertAsync(compartment);
                }
                else
                {
                    throw new UserFriendlyException("There is already a Compartment with given name");
                }
            }
            else
            {
                throw new UserFriendlyException("No Active Financial Year");
            }

         
        }

        public async Task DeleteCompartmentAsync(CompartmentDto input)
        {
            var compartment = _compartmentRepository.FirstOrDefault(input.Id);
            if (compartment == null)
            {
                throw new UserFriendlyException("Range Year not Found!");
            }
            await _compartmentRepository.DeleteAsync(compartment);
        }

        public List<CompartmentView> GetTalliedPlotsByRange(int id)
        {
            var plots = from comp in _compartmentRepository.GetAll()
                        where comp.RangeId == id
                        select new CompartmentView
                        {
                            Name = comp.Name,
                            plots = from sheet in _tallySheetRepository.GetAll()
                                    join plot in _plotRepository.GetAll() on sheet.PlotId equals plot.Id
                                    where plot.CompartmentId == comp.Id
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
                        };
            return new List<CompartmentView>(plots.ToList());
        }


        public CompartmentDto GetCompartment(int id)
        {
            var compartment = _compartmentRepository.FirstOrDefault(id);

            return compartment.MapTo<CompartmentDto>();
        }

        public List<CompartmentDto> GetCompartments()
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            var compartments = _compartmentRepository
              .GetAll()
              .Where(p => p.FinancialYearId == current.Id)
              .OrderBy(p => p.Name)
              .ToList();

            return new List<CompartmentDto>(compartments.MapTo<List<CompartmentDto>>());
        }

        public async Task UpdateCompartment(CompartmentDto input)
        {
            var compartment = _compartmentRepository.FirstOrDefault(input.Id);
            compartment.Name = input.Name;
            compartment.RangeId = input.RangeId;
            compartment.FinancialYearId = input.FinancialYearId;
            compartment.Species = input.Species;
            compartment.PlantedYear = input.PlantedYear;
            compartment.Age = input.Age;
            compartment.Area = input.Area;
            compartment.EstimatedVolume = input.EstimatedVolume;
            compartment.Season = input.Season;
            compartment.TariffNumber = input.TariffNumber;

            await _compartmentRepository.UpdateAsync(compartment);
        }
    }
}
