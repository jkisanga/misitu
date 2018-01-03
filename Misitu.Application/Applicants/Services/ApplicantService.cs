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
using Abp.AutoMapper;
using Misitu.Applicants.ForestProduce;

namespace Misitu.Applicants.Services
{
    public class ApplicantService : IApplicant
    {
        private readonly IRepository<Applicant> reporitaryApplicant;
        private readonly IRepository<RefApplicantType> repositoryApplicantType;
        private readonly IRepository<RefIdentityType> repositoryIdentity;
        private readonly IRepository<RefServiceCategory> repositoryServiceCategory;
        private readonly IRepository<ForestProduceRegistration> repositoryForestProduceRegistration;
        private readonly IRepository<ForestProduceAppliedForest> repositoryForestProduceAppliedForest;
        private readonly IRepository<ForestProduceAppliedSpecieCategory> repositoryForestProduceAppliedSpecieCategory;

        public ApplicantService(
            IRepository<Applicant> reporitaryApplicant, 
            IRepository<RefApplicantType> repositoryApplicantType, 
            IRepository<RefIdentityType> repositoryIdentity,
            IRepository<RefServiceCategory> repositoryServiceCategory,
            IRepository<ForestProduceRegistration> repositoryForestProduceRegistration,
            IRepository<ForestProduceAppliedForest> repositoryForestProduceAppliedForest,
            IRepository<ForestProduceAppliedSpecieCategory> repositoryForestProduceAppliedSpecieCategory)
        {
            this.reporitaryApplicant = reporitaryApplicant;
            this.repositoryApplicantType = repositoryApplicantType;
            this.repositoryIdentity = repositoryIdentity;
            this.repositoryServiceCategory = repositoryServiceCategory;
            this.repositoryForestProduceRegistration = repositoryForestProduceRegistration;
            this.repositoryForestProduceAppliedForest = repositoryForestProduceAppliedForest;
            this.repositoryForestProduceAppliedSpecieCategory = repositoryForestProduceAppliedSpecieCategory;
        }

        public  int CreateAsync(CreateInput input)
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
                IDExpiryDate = input.IDExpiryDate,
                FinancialYearId = input.FinancialYearId
            };
            var objExist = this.reporitaryApplicant.FirstOrDefault(a => a.Name == input.Name);
            if (objExist == null)
            {
                 return  this.reporitaryApplicant.InsertAndGetId(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }

        }

        public int CreateForestProduceAppliedForestAsync(CreateForestProduceAppliedForest input)
        {
            var obj = new ForestProduceAppliedForest
            {
                ForestProduceRegistrationId = input.ForestProduceRegistrationId,
                StationId = input.StationId,
                FinancialYearId = input.FinancialYearId,
                Status = input.Status
            };


            return this.repositoryForestProduceAppliedForest.InsertAndGetId(obj);
        }

        public int CreateForestProduceAppliedSpecieCategory(CreateForestProduceAppliedSpecieCategory input)
        {
            var obj = new ForestProduceAppliedSpecieCategory
            {
                ForestProduceRegistrationId = input.ForestProduceRegistrationId,
                SpecieCategoryId = input.SpecieCategoryId,
                FinancialYearId = input.FinancialYearId,
                Status = input.Status
            };


            return this.repositoryForestProduceAppliedSpecieCategory.InsertAndGetId(obj);
        }

        public  int CreateForestProduceRegistration(CreateForestProduceRegistration input)
        {
            var obj = new ForestProduceRegistration
            {
                ApplicantId = input.ApplicantId,
                FinancialYearId = input.FinancialYearId,
                DistrictId = input.DistrictId,
                HasForestBusinessLicense = input.HasForestBusinessLicense,
                TIN = input.TIN,
                BussinessLicenseNo = input.BussinessLicenseNo,
                RegNumber = input.RegNumber,
                HasSawmill = input.HasSawmill,
                IsSawmillInstalled = input.IsSawmillInstalled,
                SawmillInstalledLocation = input.SawmillInstalledLocation,
                InstallationPermitNo = input.InstallationPermitNo,
                TypeOfSawmill = input.TypeOfSawmill,
                SawmillCapacityPerYear = input.SawmillCapacityPerYear,
                HasTrucks = input.HasTrucks,
                TrucksOwnerAttachment = input.TrucksOwnerAttachment,
                SawmillHasLoadingBench = input.SawmillHasLoadingBench,
                SawmillHasBreakDownSaw = input.SawmillHasBreakDownSaw,
                SawmillHasReceivingBench = input.SawmillHasReceivingBench,
                HasChainSawTechnician = input.HasChainSawTechnician,
                HasTrainedOperator = input.HasTrainedOperator,
                OperatorInstitutionName = input.OperatorInstitutionName,
                OperatorCertificateAttachment = input.OperatorCertificateAttachment,
                HasSawmillOperator = input.HasSawmillOperator,
                TaxClearance = input.TaxClearance,
                CertifiedAudted = input.CertifiedAudted
            };
            var objExist = this.repositoryForestProduceRegistration.FirstOrDefault(a => a.ApplicantId == input.ApplicantId);
            if (objExist == null)
            {
                return  this.repositoryForestProduceRegistration.InsertAndGetId(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }


        }

        public int CreateForestProduceRegistrationAsync(CreateForestProduceRegistration input)
        {
            var obj = new ForestProduceRegistration
            {
                ApplicantId = input.ApplicantId,
                FinancialYearId = input.FinancialYearId,
                DistrictId = input.DistrictId,
                HasForestBusinessLicense = input.HasForestBusinessLicense,
                TIN = input.TIN,
                BussinessLicenseNo = input.BussinessLicenseNo,
                RegNumber = input.RegNumber,
                HasSawmill = input.HasSawmill,
                IsSawmillInstalled = input.IsSawmillInstalled,
                SawmillInstalledLocation = input.SawmillInstalledLocation,
                InstallationPermitNo = input.InstallationPermitNo,
                TypeOfSawmill = input.TypeOfSawmill,
                SawmillCapacityPerYear = input.SawmillCapacityPerYear,
                HasTrucks = input.HasTrucks,
                TrucksOwnerAttachment = input.TrucksOwnerAttachment,
                SawmillHasLoadingBench = input.SawmillHasLoadingBench,
                SawmillHasBreakDownSaw = input.SawmillHasBreakDownSaw,
                SawmillHasReceivingBench = input.SawmillHasReceivingBench,
                HasChainSawTechnician = input.HasChainSawTechnician,
                HasTrainedOperator = input.HasTrainedOperator,
                OperatorInstitutionName = input.OperatorInstitutionName,
                OperatorCertificateAttachment = input.OperatorCertificateAttachment,
                HasSawmillOperator = input.HasSawmillOperator,
                TaxClearance = input.TaxClearance,
                CertifiedAudted = input.CertifiedAudted
            };
            //var objExist = this.repositoryForestProduceRegistration.FirstOrDefault(a => a.ApplicantId == input.ApplicantId);
            //if (objExist == null)
            //{
                return this.repositoryForestProduceRegistration.InsertAndGetId(obj);
            //}
            //else
            //{
            //    throw new UserFriendlyException("Item Alredy Exist");
            //}
        }

        public Task DeleteForestProduceAppliedForestAsync(ForestProduceAppliedForestDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteForestProduceAppliedSpecieCategory(ForestProduceAppliedSpecieCategoryDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteForestProduceRegistrationAsync(ForestProduceRegistrationDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteObjectAsync(ApplicantDto input)
        {
            throw new NotImplementedException();
        }

        public ForestProduceAppliedForestDto GetForestProduceAppliedForestById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ForestProduceAppliedForestDto> GetForestProduceAppliedForestList()
        {
            throw new NotImplementedException();
        }

        public ForestProduceAppliedSpecieCategoryDto GetForestProduceAppliedSpecieCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ForestProduceAppliedSpecieCategoryDto> GetForestProduceAppliedSpecieCategoryList()
        {
            throw new NotImplementedException();
        }

        public ForestProduceRegistrationDto GetForestProduceRegistrationById(int id)
        {
            var obj = this.repositoryForestProduceRegistration.FirstOrDefault(id);
            return obj.MapTo<ForestProduceRegistrationDto>();
        }

        public List<ForestProduceRegistrationDto> GetForestProduceRegistrationList()
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
            var obj = reporitaryApplicant.FirstOrDefault(id);
            return obj.MapTo<ApplicantDto>();
        }

        public List<RefApplicationTypeDto> GetRefApplicationTypes()
        {
            throw new NotImplementedException();
        }

        public List<RefServiceCategoryDto> GetServiceCategoryList()
        {
            throw new NotImplementedException();
        }

        public Task UpdateForestProduceAppliedForest(ForestProduceAppliedForestDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForestProduceAppliedSpecieCategory(ForestProduceAppliedSpecieCategoryDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForestProduceRegistration(ForestProduceRegistrationDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObject(ApplicantDto input)
        {
            throw new NotImplementedException();
        }
    }
}
