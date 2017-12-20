using Abp.Application.Services;
using Misitu.Applicants.Dto;
using Misitu.RefTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Interface.ForestProduce
{
  public  interface IApplicant : IApplicationService
    {

        List<ApplicantDto> GetItemList();

        Task CreateAsync(CreateInput input);

        ApplicantDto GetObjectById(int id);

        Task UpdateObject(ApplicantDto input);

        Task DeleteObjectAsync(ApplicantDto input);

        //get list of application Type
        List<RefApplicationTypeDto> GetRefApplicationTypes();

        //get Applicant Identity Categories
        List<RefIdentityDto> GetIdentityTypeList();

        List<RefServiceCategoryDto> GetServiceCategoryList();

    }
}
