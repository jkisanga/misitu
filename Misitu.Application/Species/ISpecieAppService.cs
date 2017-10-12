using Abp.Application.Services;
using Misitu.Species.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Species
{
    public interface ISpecieAppService: IApplicationService
    {
        List<SpecieDto> GetSpecies();

        Task CreateSpecie(CreateSpecieInput input);

        SpecieDto GetSpecie(int id);

        Task UpdateSpecie(SpecieDto input);

        Task DeleteSpecieAsync(SpecieDto input);
    }
}
