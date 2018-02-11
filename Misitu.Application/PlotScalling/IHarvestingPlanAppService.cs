using Abp.Application.Services;
using Misitu.FinancialYears;
using Misitu.FinancialYears.Dto;
using Misitu.PlotScalling.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
   public interface IHarvestingPlanAppService: IApplicationService
    {
        List<HarvestingPlanDto> GetHarvestingPlans(FinancialYearDto FinancialYear);

        HarvestingPlanDto GetHarvestingPlanByStation(FinancialYearDto FinancialYear, int StationId);

        Task CreateHarvestingPlan(CreateHarvestingPlanInput input);
      
        HarvestingPlanDto GetHarvestingPlan(int id);

        Task UpdateHarvestingPlan(HarvestingPlanDto input);

        Task DeleteHarvestingPlanAsync(HarvestingPlanDto input);
    }
}
