using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.FinancialYears;
using Misitu.PlotScalling.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using Misitu.FinancialYears.Dto;

namespace Misitu.PlotScalling
{
    public class HarvestingPlanAppService : MisituAppServiceBase, IHarvestingPlanAppService
    {
        private readonly IRepository<HarvestingPlan> _harvestingPlanRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;

        public HarvestingPlanAppService(
          IRepository<HarvestingPlan> harvestingPlanRepository,
          IRepository<FinancialYear> financialYearRepository
          )
        {
            _harvestingPlanRepository = harvestingPlanRepository;
            _financialYearRepository = financialYearRepository;

        }
        public async Task CreateHarvestingPlan(CreateHarvestingPlanInput input)
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            if (current != null)
            {
                var plan = new HarvestingPlan
                {
                    StationId = input.StationId,
                    FinancialYearId = current.Id,
                    Path = input.Path
                   
                };

                var existingPlan = _harvestingPlanRepository.FirstOrDefault(p => p.FinancialYearId == current.Id);
                if (existingPlan == null)
                {
                    await _harvestingPlanRepository.InsertAsync(plan);
                }
                else
                {
                    throw new UserFriendlyException("There is already a Harvesting Plan with Current Financial Year");
                }
            }
            else
            {
                throw new UserFriendlyException("No Active Financial Year");
            }      

    }

    public async Task DeleteHarvestingPlanAsync(HarvestingPlanDto input)
        {
            var plan = _harvestingPlanRepository.FirstOrDefault(input.Id);
            if (plan == null)
            {
                throw new UserFriendlyException("Harvesting Plan not Found!");
            }
            await _harvestingPlanRepository.DeleteAsync(plan);
        }

        public HarvestingPlanDto GetHarvestingPlan(int id)
        {
            var plan = _harvestingPlanRepository.FirstOrDefault(id);

            return plan.MapTo<HarvestingPlanDto>();
        }

        public List<HarvestingPlanDto> GetHarvestingPlans(FinancialYearDto FinancialYear)
        {
            var plan = _harvestingPlanRepository
            .GetAll()
            .Where(P => P.FinancialYearId == FinancialYear.Id)
            .ToList();

            return new List<HarvestingPlanDto>(plan.MapTo<List<HarvestingPlanDto>>());
        }


        public HarvestingPlanDto GetHarvestingPlanByStation(FinancialYearDto FinancialYear, int StationId)
        {
            var plan = _harvestingPlanRepository
            .GetAll()
            .Where(P => P.FinancialYearId == FinancialYear.Id)
            .Where(p => p.StationId == StationId)
            .FirstOrDefault();

            return plan.MapTo<HarvestingPlanDto>();
        }

        public async Task UpdateHarvestingPlan(HarvestingPlanDto input)
        {
            var plan = _harvestingPlanRepository.FirstOrDefault(input.Id);
            plan.StationId = input.StationId;
            plan.Path = input.Path;

            await _harvestingPlanRepository.UpdateAsync(plan);
        }

     
    }
}
