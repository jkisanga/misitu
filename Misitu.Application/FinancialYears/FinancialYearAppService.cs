using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.FinancialYears.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.FinancialYears
{
    public class FinancialYearAppService : MisituAppServiceBase, IFinancialYearAppService
    {
        private readonly IRepository<FinancialYear> _financialYearRepository;

        public FinancialYearAppService(IRepository<FinancialYear> financialYearRepository)
        {
            _financialYearRepository = financialYearRepository;
        }

        //Activate Financial Year
        public async Task ActivateFinancialYearAsync(FinancialYearDto input)
        {
            var year = _financialYearRepository.FirstOrDefault(input.Id);
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);
            
            if(current != null)
            {
                current.IsActive = false;
                await _financialYearRepository.UpdateAsync(current);
                year.IsActive = true;
            }
            else
            {
                year.IsActive = true;
            }
         

            await _financialYearRepository.UpdateAsync(year);
        }

        public async Task CreateFinancialYear(CreateFinancialYear input)
        {
            var year = input.MapTo<FinancialYear>();

            var existingYear = _financialYearRepository.FirstOrDefault(p => p.Name == input.Name);
            if (existingYear == null)
            {
                await _financialYearRepository.InsertAsync(year);
            }
            else
            {
                throw new UserFriendlyException("There is already a Financial Year with given name");
            }
        }

        public async Task DeleteFinancialYearAsync(FinancialYearDto input)
        {
            var year = _financialYearRepository.FirstOrDefault(input.Id);

            if (year == null && year.IsActive==true)
            {
                throw new UserFriendlyException("Financial Year Is Active or not Found!");
            }
            else {
                await _financialYearRepository.DeleteAsync(year);
            }
            
        }

        public FinancialYearDto GetFinancialYear(int id)
        {
            var year = _financialYearRepository.FirstOrDefault(id);

            return year.MapTo<FinancialYearDto>();
        }

        public FinancialYearDto GetActiveFinancialYear()
        {
            var year = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            if(year != null){
                return year.MapTo<FinancialYearDto>();
            }
            else
            {
                throw new UserFriendlyException(" Active Financial Year  not Found!");
            }
        }

        public List<FinancialYearDto> GetFinancialYears()
        {
            var years = _financialYearRepository
             .GetAll()
             .OrderBy(p => p.Name)
             .ToList();

            return new List<FinancialYearDto>(years.MapTo<List<FinancialYearDto>>());
        }

        public async Task UpdateFinancialYear(FinancialYearDto input)
        {
            var year = _financialYearRepository.FirstOrDefault(input.Id);
            year.Name = input.Name;

            await _financialYearRepository.UpdateAsync(year);
        }
    }
}
