using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.RevenueSources.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using Misitu.Registration;

namespace Misitu.RevenueSources
{
    public class MainRevenueResourceService : MisituAppServiceBase, Interface1
    {
        private readonly IRepository<MainRevenueSource> repositoryMainRevenueSource;

        public MainRevenueResourceService(IRepository<MainRevenueSource> repository)
        {
            this.repositoryMainRevenueSource = repository;
        }

        public int CreateMainRevenueSource(CreateMainRevenueSource input)
        {
            var obj = new MainRevenueSource
            {
                Code = input.Code,
                Description = input.Description,
                
            };
            var objExist = this.repositoryMainRevenueSource.FirstOrDefault(a => a.Code == input.Code);
            if (objExist == null)
            {
                return this.repositoryMainRevenueSource.InsertAndGetId(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }
        }

        public async Task DeleteMainRevenueSourceAsync(MainRevenueSourceDto input)
        {
            var obj = this.repositoryMainRevenueSource.FirstOrDefault(input.Id);
            if (obj == null)
            {
                throw new UserFriendlyException("Item not Found!");
            }

            await this.repositoryMainRevenueSource.DeleteAsync(obj);
        }

        public MainRevenueSourceDto GetMainRevenueSource(int id)
        {
            var obj = this.repositoryMainRevenueSource.FirstOrDefault(id);

            return obj.MapTo<MainRevenueSourceDto>();
        }

        public List<MainRevenueSourceDto> GetMainRevenueSuorces()
        {
            var values = this.repositoryMainRevenueSource
           .GetAll()
           .OrderBy(p => p.CreationTime)
           .ToList();

            return new List<MainRevenueSourceDto>(values.MapTo<List<MainRevenueSourceDto>>());
        }

        public async Task UpdateMainRevenueSource(MainRevenueSourceDto input)
        {
            var obj = this.repositoryMainRevenueSource.FirstOrDefault(input.Id);
            obj.Code = input.Code;
            obj.Description = input.Description;
            await this.repositoryMainRevenueSource.UpdateAsync(obj);
        }
    }
}
