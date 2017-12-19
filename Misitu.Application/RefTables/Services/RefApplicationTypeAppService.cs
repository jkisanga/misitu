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
    public class RefApplicationTypeAppService : MisituAppServiceBase, IRefApplicationTypeAppService
    {
        private readonly IRepository<RefApplicantType> _refApplicantType;

        public RefApplicationTypeAppService(IRepository<RefApplicantType> refApplicantType)
        {
            _refApplicantType = refApplicantType;
        }

        public async Task CreateApplicationTypeAsync(CreateRefApplicationInput input)
        {
            var applicantType = new RefApplicantType
            {
                Name = input.Name,
                Code = input.Code
        };


            var valueExist = _refApplicantType.FirstOrDefault(p => p.Name == input.Name);
            if (valueExist == null)
            {
              
                await _refApplicantType.InsertAsync(applicantType);
            }
            else
            {
                throw new UserFriendlyException("There is already a Application Type with given name");
            }
            
        }

        public async Task DeleteApplicationAsync(RefApplicationTypeDto input)
        {
            var applicationType = _refApplicantType.FirstOrDefault(input.Id);
            if (applicationType == null)
            {
                throw new UserFriendlyException("Item  not Found!");
            }
            else
            {
                await _refApplicantType.DeleteAsync(applicationType);
            }

    
        }

        public RefApplicationTypeDto GetApplicationTypeById(int id)
        {
            var applicationType = _refApplicantType.FirstOrDefault(id);
            if (applicationType != null)
            {
                return applicationType.MapTo<RefApplicationTypeDto>();
            }else
            {
                throw new UserFriendlyException("Item not Found");
            }
           
        }

        public List<RefApplicationTypeDto> GetRefApplicationTypes()
        {
            var values = _refApplicantType.GetAll().OrderBy(a => a.Name).ToList();
            return new List<RefApplicationTypeDto>(values.MapTo<List<RefApplicationTypeDto>>());
        }

        public async Task UpdateApplicationType(RefApplicationTypeDto input)
        {
            var value = _refApplicantType.FirstOrDefault(input.Id);
            value.Name = input.Name;
            value.Code = input.Code;
            if(value != null)
            {
                await _refApplicantType.UpdateAsync(value);
            }else
            {
                throw new UserFriendlyException("Item Not Found");
            }
            
        }
    }
}
