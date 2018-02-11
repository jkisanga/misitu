using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Species.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.Species
{
    public class SpecieCategoryAppService : MisituAppServiceBase, ISpecieCategoryAppService
    {

        private readonly IRepository<SpecieCategory> _specieCategoryRepository;

        public SpecieCategoryAppService(IRepository<SpecieCategory> specieCategoryRepository)
        {
            _specieCategoryRepository = specieCategoryRepository;

        }


        public async Task CreateSpecieCategory(CreateSpecieCategoryInput input)
        {
            var category = new SpecieCategory
            {
                Description = input.Description,
                Name = input.Name,
                Amount = input.Amount

            };

            var Exist = _specieCategoryRepository.FirstOrDefault(p => p.Name == input.Name);
            if (Exist == null)
            {
                await _specieCategoryRepository.InsertAsync(category);
            }
            else
            {
                throw new UserFriendlyException("There is already a Category with given name");
            }
        }

        public async Task DeleteSpecieCategoryAsync(SpecieCategoryDto input)
        {
            var category = _specieCategoryRepository.FirstOrDefault(input.Id);
            if (category == null)
            {
                throw new UserFriendlyException("Category not Found!");
            }

            await _specieCategoryRepository.DeleteAsync(category);
        }

        public List<SpecieCategoryDto> GetSpecieCategories()
        {
            var categories = _specieCategoryRepository
             .GetAll()
             .OrderBy(p => p.Name)
             .ToList();

            return new List<SpecieCategoryDto>(categories.MapTo<List<SpecieCategoryDto>>());
        }

        public SpecieCategoryDto GetSpecieCategory(int id)
        {
            var category = _specieCategoryRepository.FirstOrDefault(id);

            return category.MapTo<SpecieCategoryDto>();
        }

        public async Task UpdateSpecieCategory(SpecieCategoryDto input)
        {
            var category = _specieCategoryRepository.FirstOrDefault(input.Id);
            category.Description = input.Description;
            category.Name = input.Name;
            category.Amount = input.Amount;

            await _specieCategoryRepository.UpdateAsync(category);
        }
    }
}
