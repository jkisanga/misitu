using Misitu.RefTables.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.RefTables.Dto;
using Abp.Domain.Repositories;
using Misitu.RefereneceTables;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.RefTables.Services
{
    class RefServiceCategoryAppService : IRefServiceCategoryAppService
    {
        private readonly IRepository<RefServiceCategory> repository;

        public RefServiceCategoryAppService(IRepository<RefServiceCategory> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(CreateRefServiceCategoryInput input)
        {
            var obj = new RefServiceCategory
            {
                Name = input.Name,
                Description = input.Description
            };

            var objExist = this.repository.FirstOrDefault(a => a.Name == input.Name);
            if (objExist == null)
            {
                await this.repository.InsertAsync(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }
        }

        public async Task DeleteObjectAsync(RefServiceCategoryDto input)
        {
            var objExist = this.repository.FirstOrDefault(input.Id);
            if (objExist != null)
            {
                await this.repository.DeleteAsync(objExist);
            }
            else
            {
                throw new UserFriendlyException("Item Not Found");
            }
        }

        public List<RefServiceCategoryDto> GetItemList()
        {
            var values = this.repository.GetAll().OrderBy(a => a.Name).ToList();

            return new List<RefServiceCategoryDto>(values.MapTo<List<RefServiceCategoryDto>>());
        }

        public RefServiceCategoryDto GetObjectById(int id)
        {
            var obj = this.repository.FirstOrDefault(id);
            if (obj != null)
            {
                return obj.MapTo<RefServiceCategoryDto>();
            }
            else
            {
                throw new UserFriendlyException("Item Not Found");
            }
        }

        public async Task UpdateObject(RefServiceCategoryDto input)
        {
            var obj = this.repository.FirstOrDefault(input.Id);
            obj.Name = input.Name;
            obj.Description = input.Description;
            if (obj != null)
            {
                await this.repository.UpdateAsync(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Not Found");
            }
        }
    }
}
