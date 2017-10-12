using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Species.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.Species
{
    public class SpecieAppService : MisituAppServiceBase, ISpecieAppService
    {
        private readonly IRepository<Specie> _specieRepository;

        public SpecieAppService(IRepository<Specie> specieRepository)
        {
            _specieRepository = specieRepository;
        }

        public async Task CreateSpecie(CreateSpecieInput input)
        {
            var specie = new Specie
            {
                SpecieCategoryId = input.SpecieCategoryId,
                EnglishName = input.EnglishName,
                CommonName = input.CommonName,
                SwahiliName = input.SwahiliName
            };

            var Exist = _specieRepository.FirstOrDefault(p => p.EnglishName == input.EnglishName);
            if (Exist == null)
            {
                await _specieRepository.InsertAsync(specie);
            }
            else
            {
                throw new UserFriendlyException("There is already a Specie with given name");
            }
        }

        public async Task DeleteSpecieAsync(SpecieDto input)
        {
            var specie = _specieRepository.FirstOrDefault(input.Id);
            if (specie == null)
            {
                throw new UserFriendlyException("Specie not Found!");
            }

            await _specieRepository.DeleteAsync(specie);
        }

        public SpecieDto GetSpecie(int id)
        {
            var specie = _specieRepository.FirstOrDefault(id);

            return specie.MapTo<SpecieDto>();
        }

        public List<SpecieDto> GetSpecies()
        {
            var species = _specieRepository
            .GetAll()
            .OrderBy(p => p.EnglishName)
            .ToList();

            return new List<SpecieDto>(species.MapTo<List<SpecieDto>>());
        }

        public async Task UpdateSpecie(SpecieDto input)
        {
            var specie = _specieRepository.FirstOrDefault(input.Id);
            specie.EnglishName = input.EnglishName;
            specie.CommonName = input.CommonName;
            specie.SwahiliName = input.SwahiliName;
            specie.SpecieCategoryId = input.SpecieCategoryId;

            await _specieRepository.UpdateAsync(specie);
        }
    }
}
