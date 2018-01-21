using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.RevenueSources.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.RevenueSources
{
    public class RevenueSourceAppService : MisituAppServiceBase, IRevenueSourceAppService
    {
        private readonly IRepository<RevenueSource> _revenueResourceRepository;

        public RevenueSourceAppService(IRepository<RevenueSource> revenueResourceRepository)
        {
            _revenueResourceRepository = revenueResourceRepository;
        }

        public async Task CreateRevenueResource(CreateRevenueSourcesInput input)
        {


            var Exist = _revenueResourceRepository.FirstOrDefault(p => p.Description == input.Description);
            if (Exist == null)
            {
                var resource = new RevenueSource {
                    Code = input.Code,
                    Description = input.Description
                };
                await _revenueResourceRepository.InsertAsync(resource);
            }
            else
            {
                throw new UserFriendlyException("There is already a Revenue Resource with given name");
            }
        }

        public async Task DeleteRevenueResourceAsync(RevenueSourcesDto input)
        {
            var resource = _revenueResourceRepository.FirstOrDefault(input.Id);
            if (resource == null)
            {
                throw new UserFriendlyException("Revenue Resource not Found!");
            }

            await _revenueResourceRepository.DeleteAsync(resource);
        }

        public RevenueSourcesDto GetRevenueResource(int id)
        {
            var resource = _revenueResourceRepository.FirstOrDefault(id);

            return resource.MapTo<RevenueSourcesDto>();
        }

        public List<RevenueSourcesDto> GetRevenueResources()
        {
            var resources = _revenueResourceRepository
           .GetAll()
           .OrderBy(p => p.Description)
           .ToList();

            return new List<RevenueSourcesDto>(resources.MapTo<List<RevenueSourcesDto>>());
        }

        public async Task UpdateRevenueResource(RevenueSourcesDto input)
        {
            // here aoutomapping can be done;
            var resource = _revenueResourceRepository.FirstOrDefault(input.Id);
            resource.Description = input.Description;
            resource.Code = input.Code;

            await _revenueResourceRepository.UpdateAsync(resource);
        }




    }
}
