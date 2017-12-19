using Abp.Application.Services;
using Misitu.RefTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Interface
{
  public  interface IRefServiceCategoryAppService : IApplicationService
    {
        List<RefServiceCategoryDto> GetItemList();

        Task CreateAsync(CreateRefServiceCategoryInput input);

        RefServiceCategoryDto GetObjectById(int id);

        Task UpdateObject(RefServiceCategoryDto input);

        Task DeleteObjectAsync(RefServiceCategoryDto input);
    }
}
