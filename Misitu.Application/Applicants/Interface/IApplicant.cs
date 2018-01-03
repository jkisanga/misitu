using Abp.Application.Services;
using Misitu.Applicants.Dto;
using Misitu.Applicants.ForestProduce;
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

        int CreateAsync(CreateInput input);

        ApplicantDto GetObjectById(int id);

        Task UpdateObject(ApplicantDto input);

        Task DeleteObjectAsync(ApplicantDto input);

        //get list of application Type
        List<RefApplicationTypeDto> GetRefApplicationTypes();

        //get Applicant Identity Categories
        List<RefIdentityDto> GetIdentityTypeList();

        List<RefServiceCategoryDto> GetServiceCategoryList();

        //Create ForestProduceRegistration
        //int CreateForestProduceRegistration(CreateForestProduceRegistration forestProduceRegistrationDto);

        List<ForestProduceAppliedForestDto> GetForestProduceAppliedForestList();

        int CreateForestProduceAppliedForestAsync(CreateForestProduceAppliedForest input);

        ForestProduceAppliedForestDto GetForestProduceAppliedForestById(int id);

        Task UpdateForestProduceAppliedForest(ForestProduceAppliedForestDto input);

        Task DeleteForestProduceAppliedForestAsync(ForestProduceAppliedForestDto input);




        List<ForestProduceRegistrationDto> GetForestProduceRegistrationList();

        int CreateForestProduceRegistrationAsync(CreateForestProduceRegistration input);

        ForestProduceRegistrationDto GetForestProduceRegistrationById(int id);

        Task UpdateForestProduceRegistration(ForestProduceRegistrationDto input);

        Task DeleteForestProduceRegistrationAsync(ForestProduceRegistrationDto input);




        List<ForestProduceAppliedSpecieCategoryDto> GetForestProduceAppliedSpecieCategoryList();

        int CreateForestProduceAppliedSpecieCategory(CreateForestProduceAppliedSpecieCategory input);

        ForestProduceAppliedSpecieCategoryDto GetForestProduceAppliedSpecieCategoryById(int id);

        Task UpdateForestProduceAppliedSpecieCategory(ForestProduceAppliedSpecieCategoryDto input);

        Task DeleteForestProduceAppliedSpecieCategory(ForestProduceAppliedSpecieCategoryDto input);
    }
}
