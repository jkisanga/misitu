
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Applicants.Dto;
using Misitu.RefTables.Dto;
using Abp.Domain.Repositories;
using Misitu.RefereneceTables;
using Abp.UI;
using Misitu.Applicants.Interface;
using Misitu.FinancialYears;
using Abp.AutoMapper;

namespace Misitu.Applicants.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IRepository<Applicant> reporitaryApplicant;
        private readonly IRepository<RefApplicantType> repositoryApplicantType;
        private readonly IRepository<RefIdentityType> repositoryIdentity;
        private readonly IRepository<RefServiceCategory> repositoryServiceCategory;
        private readonly IRepository<FinancialYear> financialYearRepository;

        public ApplicantService(IRepository<Applicant> reporitaryApplicant,
            IRepository<RefApplicantType> repositoryApplicantType, 
            IRepository<RefIdentityType> repositoryIdentity,
            IRepository<RefServiceCategory> repositoryServiceCategory,
              IRepository<FinancialYear> financialYearRepository
            )
        {
            this.reporitaryApplicant = reporitaryApplicant;
            this.repositoryApplicantType = repositoryApplicantType;
            this.repositoryIdentity = repositoryIdentity;
            this.repositoryServiceCategory = repositoryServiceCategory;
            this.financialYearRepository = financialYearRepository;
        }

        public  int  CreateAsync(CreateInput input)
        {

            //get current active financial year;
            var currentYr = financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            var obj = new Applicant
            {
                Type = input.Type,
                Name = input.Name,
                Adress = input.Adress,
                Phone = input.Phone,
                Email = input.Email,
                IsTanzanian = input.IsTanzanian,
                IDtype = input.IDtype,
                IDNumber = input.IDNumber,
                IDIssuePlace = input.IDIssuePlace,
                IDExpiryDate = DateTime.Now,
                FinancialYearId = currentYr.Id
            };
            var objExist = this.reporitaryApplicant.FirstOrDefault(a => a.Name == input.Name);
            if (objExist == null)
            {
               int ApplicantId =  this.reporitaryApplicant.InsertAndGetId(obj);
               return ApplicantId;

            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }

        }

      
        //get applicant by id
        public ApplicantDto GetApplicantById(int id)
        {
            var applicant = this.reporitaryApplicant.FirstOrDefault(id);

            return applicant.MapTo<ApplicantDto>();
        }

      
    }
}
