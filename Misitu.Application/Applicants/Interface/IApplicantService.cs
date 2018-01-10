using Abp.Application.Services;
using Misitu.Applicants.Dto;
using Misitu.Applicants.ForestProduce;
using Misitu.RefTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Interface
{
  public  interface IApplicantService : IApplicationService
    {



        int CreateAsync(CreateInput input);

        ApplicantDto GetApplicantById(int id);

        List<ForestProduceAppliedForestDto> GetForestProduceAppliedForestList();

        int CreateForestProduceAppliedForestAsync(CreateForestProduceAppliedForest input);

        ForestProduceAppliedForestDto GetForestProduceAppliedForestById(int id);

        Task UpdateForestProduceAppliedForest(ForestProduceAppliedForestDto input);

        Task DeleteForestProduceAppliedForestAsync(ForestProduceAppliedForestDto input);

        List<ForestProduceRegistrationDto> GetForestProduceRegistrationByApplicantId(int Id);

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
