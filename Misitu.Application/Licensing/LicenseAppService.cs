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
using Misitu.Billing;
using Misitu.Registration;

namespace Misitu.Licensing
{
    public class LicenseAppService : MisituAppServiceBase, ILicenseAppService
    {
        private readonly IRepository<License> _licenseRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<Dealer> _dealerRepository;

        public LicenseAppService(IRepository<License> licenseRepository,
            IRepository<FinancialYear> financialYearRepository,
               IRepository<Bill> billRepository,
               IRepository<Dealer> dealerRepository
            )
        {
            _licenseRepository = licenseRepository;
            _financialYearRepository = financialYearRepository;
            _billRepository = billRepository;
            _dealerRepository = dealerRepository;
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

        public List<LicenseView> GetLicenses(FinancialYearDto FinancialYear)
        {
            var licenses = (from l in _licenseRepository.GetAll()
                            join bill in _billRepository.GetAll() on l.BillId equals bill.Id
                            join dealer in _dealerRepository.GetAll() on bill.DealerId equals dealer.Id
                            where bill.PaidAmount > 0
                            where bill.PaidDate != null
                            where l.FinancialYearId == FinancialYear.Id

                            orderby l.IssuedDate
                            select new LicenseView{

                                    SerialNumber = l.serialNumber,
                                    Dealer = dealer.Name,
                                    Description = bill.Description,
                                    Amount = bill.PaidAmount,
                                    IssuedDate = l.IssuedDate

                            }).ToList();

            return new List<LicenseView>(licenses.MapTo<List<LicenseView>>());
        }

        public List<LicenseView> GetPendingLicenses(FinancialYearDto FinancialYear)
        {
            var licenses = (from l in _licenseRepository.GetAll()
                            join bill in _billRepository.GetAll() on l.BillId equals bill.Id
                            join dealer in _dealerRepository.GetAll() on bill.DealerId equals dealer.Id
                            where bill.PaidAmount == 0
                            where bill.PaidDate == null
                            where l.FinancialYearId == FinancialYear.Id

                            orderby l.IssuedDate
                            select new LicenseView
                            {

                                SerialNumber = l.serialNumber,
                                Dealer = dealer.Name,
                                Description = bill.Description,
                                Amount = bill.BillAmount,
                                IssuedDate = bill.IssuedDate

                            }).ToList();
           

            return new List<LicenseView>(licenses.MapTo<List<LicenseView>>());
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
