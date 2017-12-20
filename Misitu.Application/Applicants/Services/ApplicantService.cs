using Misitu.Applicants.Interface.ForestProduce;
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

namespace Misitu.Applicants.Services
{
    public class ApplicantService : IApplicant
    {
        private readonly IRepository<Applicant> reporitaryApplicant;
        private readonly IRepository<RefApplicantType> repositoryApplicantType;
        private readonly IRepository<RefIdentityType> repositoryIdentity;
        private readonly IRepository<RefServiceCategory> repositoryServiceCategory;

        public ApplicantService(IRepository<Applicant> reporitaryApplicant, IRepository<RefApplicantType> repositoryApplicantType, IRepository<RefIdentityType> repositoryIdentity, IRepository<RefServiceCategory> repositoryServiceCategory)
        {
            this.reporitaryApplicant = reporitaryApplicant;
            this.repositoryApplicantType = repositoryApplicantType;
            this.repositoryIdentity = repositoryIdentity;
            this.repositoryServiceCategory = repositoryServiceCategory;
        }

        public async Task CreateAsync(CreateInput input)
        {
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
                IDExpiryDate = input.IDExpiryDate
            };
            var objExist = this.reporitaryApplicant.FirstOrDefault(a => a.Name == input.Name);
            if (objExist != null)
            {
                await this.reporitaryApplicant.InsertAsync(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }

        }

        public Task DeleteObjectAsync(ApplicantDto input)
        {
            throw new NotImplementedException();
        }

        public List<RefIdentityDto> GetIdentityTypeList()
        {
            throw new NotImplementedException();
        }

        public List<ApplicantDto> GetItemList()
        {
            throw new NotImplementedException();
        }

        public ApplicantDto GetObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RefApplicationTypeDto> GetRefApplicationTypes()
        {
            throw new NotImplementedException();
        }

        public List<RefServiceCategoryDto> GetServiceCategoryList()
        {
            throw new NotImplementedException();
        }

        public Task UpdateObject(ApplicantDto input)
        {
            throw new NotImplementedException();
        }
    }
}
