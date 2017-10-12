using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.FinancialYears.Dto;
using Misitu.Licensing.Dto;
using Abp.Domain.Repositories;
using Misitu.FinancialYears;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.Licensing
{
    public class LicenseAppService : MisituAppServiceBase, ILicenseAppService
    {
        private readonly IRepository<License> _licenseRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;

        public LicenseAppService(IRepository<License> licenseRepository,
            IRepository<FinancialYear> financialYearRepository)
        {
            _licenseRepository = licenseRepository;
            _financialYearRepository = financialYearRepository;
        }

        public async Task ConfirmPayment(LicenseDto input, string PaymentReference)
        {
            var license = _licenseRepository.FirstOrDefault(input.Id);
            if (license != null)
            {
                license.ReceiptNumber = PaymentReference;
                license.PaidDate = DateTime.Now;
                await _licenseRepository.UpdateAsync(license);
            }
            else
            {
                throw new UserFriendlyException("License not Found!");
            }
        }

        public void CreateLicense(CreateLicenseInput input)
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            Random random = new Random();

            if (current != null)
            {
                var license = new License
                {
                    serialNumber = random.Next(input.BillId, 1000000).ToString(),
                    FinancialYearId = current.Id,
                    StationId = input.StationId,
                    BillId = input.BillId,
                    IssuedDate = input.IssuedDate

                };
                _licenseRepository.Insert(license);
            }
            else
            {
                throw new UserFriendlyException("No Active Financial Year");
            }
        }

        public async Task DeleteLicenseAsync(LicenseDto input)
        {
            var license = _licenseRepository.FirstOrDefault(input.Id);
            if (license == null)
            {
                throw new UserFriendlyException("license  not Found!");
            }
            await _licenseRepository.DeleteAsync(license);
        }

        public LicenseDto GetLicense(int id)
        {
            var license = _licenseRepository.FirstOrDefault(id);

            return license.MapTo<LicenseDto>();
        }

        public List<LicenseDto> GetLicenses(FinancialYearDto FinancialYear)
        {
            var licenses = _licenseRepository
           .GetAll()
           .Where(p => p.FinancialYearId == FinancialYear.Id)
           .Where(p => p.ReceiptNumber != null)
           .OrderBy(p => p.PaidDate)
           .ToList();

            return new List<LicenseDto>(licenses.MapTo<List<LicenseDto>>());
        }

        public List<LicenseDto> GetPendingLicenses(FinancialYearDto FinancialYear)
        {
            var licenses = _licenseRepository
             .GetAll()
             .Where(p => p.FinancialYearId == FinancialYear.Id)
             .Where(p => p.ReceiptNumber == null)
             .OrderBy(p => p.IssuedDate)
             .ToList();

            return new List<LicenseDto>(licenses.MapTo<List<LicenseDto>>());
        }

        public int GetTotalLicenseByStationId(int id)
        {
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);
            var license = _licenseRepository.GetAll()
                .Where(x => x.StationId == id)
                .Where(x => x.FinancialYearId == current.Id)
                .Where(x => x.ReceiptNumber != null)
                .Count();

            return license;
        }

        public Task UpdateLicense(LicenseDto input)
        {
            throw new NotImplementedException();
        }

        public LicenseDto GetLicenseByBill(int billId)
        {
            var license = _licenseRepository.GetAll()
                .Where( x => x.BillId == billId)
                .First();
            return license.MapTo<LicenseDto>();
        }
    }
}
