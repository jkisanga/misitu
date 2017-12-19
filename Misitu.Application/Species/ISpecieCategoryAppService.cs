using Abp.Application.Services;
using Misitu.Species.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Species
{
    public interface ISpecieCategoryAppService: IApplicationService
    {
        List<SpecieCategoryDto> GetSpecieCategories();

        Task CreateSpecieCategory(CreateSpecieCategoryInput input);

        SpecieCategoryDto GetSpecieCategory(int id);

        Task UpdateSpecieCategory(SpecieCategoryDto input);

        Task DeleteSpecieCategoryAsync(SpecieCategoryDto input);
    }
}
