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
        private readonly IRepository<BillItem> _billItemRepository;
        private readonly IRepository<Dealer> _dealerRepository;

        public LicenseAppService(IRepository<License> licenseRepository,
            IRepository<FinancialYear> financialYearRepository,
               IRepository<Bill> billRepository,
               IRepository<Dealer> dealerRepository,
               IRepository<BillItem> billItemRepository
            )
        {
            _licenseRepository = licenseRepository;
            _financialYearRepository = financialYearRepository;
            _billRepository = billRepository;
            _dealerRepository = dealerRepository;
            _billItemRepository = billItemRepository;
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
                            //join dealer in _dealerRepository.GetAll() on bill.ApplicantId equals dealer.Id
                            where bill.PaidAmount > 0
                            where bill.PaidDate != null
                            where l.FinancialYearId == FinancialYear.Id

                            orderby l.IssuedDate
                            select new LicenseView{
                                Id = l.Id,
                                SerialNumber = l.serialNumber,
                               // Dealer = dealer.Name,
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
                            join dealer in _dealerRepository.GetAll() on bill.ApplicantId equals dealer.Id
                            where bill.PaidAmount == 0
                            where bill.PaidDate == null
                            where l.FinancialYearId == FinancialYear.Id

                            orderby l.IssuedDate
                            select new LicenseView
                            {

                                SerialNumber = l.serialNumber,
                                //Dealer = dealer.Name,
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

        public List<LicenceCertDto> PrintLicence(int id)
        {
            var licence = from l in _licenseRepository.GetAll()
                          join b in _billRepository.GetAll() on l.BillId equals b.Id
                          join item in _billItemRepository.GetAll() on b.Id equals item.BillId
                       where l.Id == id
                       select new LicenceCertDto
                       {
                           Id = l.Id,
                           SerialNumber = l.serialNumber,
                           Name = b.Applicant.Name,
                           Address = b.Applicant.Adress,
                           Phone = b.Applicant.Phone,
                           Station = b.Station.Name,
                           StationAddress = b.Station.Address,
                           IssuedDate = l.IssuedDate,
                           ExpireDate = l.ExpiredDate,
                           Currency = b.Currency,
                           Description = b.Description,
                           BillId = item.BillId,
                           ItemDescription = item.Description,
                           Royality = item.Loyality,
                           OtherCharges = item.TFF + item.LMDA+ item.VAT+ item.CESS+ item.TP+ item.DataSheet,
                           Total = item.Total
                       };

            return new List<LicenceCertDto>(); 
        }

    }
}
