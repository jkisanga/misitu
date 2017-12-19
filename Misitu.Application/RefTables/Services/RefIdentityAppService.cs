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
    class RefIdentityAppService : IRefIdentityAppService
    {
        private readonly IRepository<RefIdentityType> _repository;

        public RefIdentityAppService(IRepository<RefIdentityType> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateRefIdentityInput input)
        {
            var obj = new RefIdentityType
            {
                Name = input.Name
            };

            var objExist = _repository.FirstOrDefault(a => a.Name == input.Name);
            if(objExist != null)
            {
                await _repository.InsertAsync(obj);
            }else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }
        }

        public async Task DeleteObjectAsync(RefIdentityDto input)
        {
            var objExist = _repository.FirstOrDefault(input.Id);
            if (objExist != null)
            {
                await _repository.DeleteAsync(objExist);
            }
            else
            {
                throw new UserFriendlyException("Item Not Found");
            }
        }

        public List<RefIdentityDto> GetItemList()
        {
            var values = _repository.GetAll().OrderBy(a => a.Name).ToList();

            return new List<RefIdentityDto>(values.MapTo<List<RefIdentityDto>>());
        }

        public RefIdentityDto GetObjectById(int id)
        {
            var obj = _repository.FirstOrDefault(id);
            if (obj != null)
            {
                return obj.MapTo<RefIdentityDto>();
            }
            else
            {
                throw new UserFriendlyException("Item Not Found");
            }
        }

        public async Task UpdateObject(RefIdentityDto input)
        {
            var obj = _repository.FirstOrDefault(input.Id);
            obj.Name = input.Name;
            if (obj != null)
            {
                await _repository.UpdateAsync(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Not Found");
            }
        }
    }
}
