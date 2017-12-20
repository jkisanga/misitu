using Abp.Application.Services;
using Misitu.RefTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Interface
{
  public  interface IRefIdentityAppService : IApplicationService
    {

        List<RefIdentityDto> GetItemList();

        Task CreateAsync(CreateRefIdentityInput input);

        RefIdentityDto GetObjectById(int id);

        Task UpdateObject(RefIdentityDto input);

        Task DeleteObjectAsync(RefIdentityDto input);
    }
}
