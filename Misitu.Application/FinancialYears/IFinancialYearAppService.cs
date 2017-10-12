using Abp.Application.Services;
using Misitu.FinancialYears.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.FinancialYears
{
    public interface IFinancialYearAppService : IApplicationService
    {
        List<FinancialYearDto> GetFinancialYears();

        Task CreateFinancialYear(CreateFinancialYear input);

        FinancialYearDto GetFinancialYear(int id);

        FinancialYearDto GetActiveFinancialYear();

        Task UpdateFinancialYear(FinancialYearDto input);

        Task DeleteFinancialYearAsync(FinancialYearDto input);

        Task ActivateFinancialYearAsync(FinancialYearDto input);
    }
}
