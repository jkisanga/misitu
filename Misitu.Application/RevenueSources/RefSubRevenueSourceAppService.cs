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
    public class RefSubRevenueSourceAppService : MisituAppServiceBase, IRefSubRevenueSourceAppService
    {
        private readonly IRepository<RefSubRevenueSource> _refSubRevenueResourceRepository;
        private readonly IRepository<RevenueSource> _revenueResourceRepository;

        public RefSubRevenueSourceAppService(IRepository<RefSubRevenueSource> refSubRevenueResourceRepository, IRepository<RevenueSource> revenueResourceRepository)
        {
            _refSubRevenueResourceRepository = refSubRevenueResourceRepository;
            _revenueResourceRepository = revenueResourceRepository;
        }

        public async Task CreateRefSubRevenueResource(CreateRefSubRevenueSourcesInput input)
        {


            var Exist = _refSubRevenueResourceRepository.FirstOrDefault(p => p.Code == input.Code);
            if (Exist == null)
            {
                var resource = new RefSubRevenueSource {
                    Code = input.Code,
                    Description = input.Description
                };
                await _refSubRevenueResourceRepository.InsertAsync(resource);
            }
            else
            {
                throw new UserFriendlyException("There is already a Sub Revenue Resource with given name");
            }
        }

        public Task CreateRevenueResource(CreateRevenueSourcesInput input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRefSubRevenueResourceAsync(RevenueSourcesDto input)
        {
            var resource = _refSubRevenueResourceRepository.FirstOrDefault(input.Id);
            if (resource == null)
            {
                throw new UserFriendlyException("Revenue Sub Resource not Found!");
            }

            await _refSubRevenueResourceRepository.DeleteAsync(resource);
        }

        public Task DeleteRefSubRevenueResourceAsync(RefSubRevenueSourcesDto input)
        {
            throw new NotImplementedException();
        }

        public RefSubRevenueSourcesDto GetRefSubRevenueResource(int id)
        {
            var resource = _refSubRevenueResourceRepository.FirstOrDefault(id);

            return resource.MapTo<RefSubRevenueSourcesDto>();
        }

        public List<RefSubRevenueSourcesDto> GetRefSubRevenueResources(int Id)
        {
            var resources = _refSubRevenueResourceRepository.GetAll().Where(a => a.RevenueResourceId == Id)
           .OrderBy(p => p.Code)
           .ToList();

            return new List<RefSubRevenueSourcesDto>(resources.MapTo<List<RefSubRevenueSourcesDto>>());
        }

        public async Task UpdateRefSubRevenueResource(RefSubRevenueSourcesDto input)
        {
            // here aoutomapping can be done;
            var resource = _refSubRevenueResourceRepository.FirstOrDefault(input.Id);
            resource.Description = input.Description;
            resource.Code = input.Code;

            await _refSubRevenueResourceRepository.UpdateAsync(resource);
        }
    }
}
