using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Registration.Dto;
using Abp.Domain.Repositories;
using Misitu.FinancialYears;
using Abp.UI;
using Abp.AutoMapper;
using Misitu.FinancialYears.Dto;
using Misitu.Stations.Dto;
using Misitu.Activities;
using System.Linq.Dynamic.Core;
using Misitu.Users;

namespace Misitu.Registration
{
    public class DealerAppService : MisituAppServiceBase, IDealerAppService
    {
        private readonly IRepository<Dealer> _dealerRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<DealerActivity> _dealerActivityRepository;
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<User, long> _userRepository;

        public DealerAppService(IRepository<Dealer> dealerRepository,
            IRepository<FinancialYear> financialYearRepository,
            IRepository<DealerActivity> dealerActivityRepository,
            IRepository<Activity> activityRepository,
             IRepository<User, long> userRepository
            )
        {
            _dealerRepository = dealerRepository;
            _financialYearRepository = financialYearRepository;
            _activityRepository = activityRepository;
            _dealerActivityRepository = dealerActivityRepository;
            _userRepository = userRepository; ;
        }

      

        public  int CreateDealer(CreateDealerInput input)
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

            if (current != null)
            {
                var dealer = new Dealer
                {
                    SerialNumber = input.SerialNumber,
                    Name = input.Name,                
                    FinancialYearId = current.Id,
                    Address = input.Address,
                    Email = input.Email,
                    Phone = input.Phone,
                    StationId = input.StationId,
                    RegisteredDate = input.RegisteredDate,
                    TIN = input.TIN,
                    BusinessLicense = input.BusinessLicense
                   
                };

                var existingDealer = _dealerRepository.FirstOrDefault(p => p.Name == input.Name);
                if (existingDealer == null)
                {
                    var dealerId =  _dealerRepository.InsertAndGetId(dealer);
                    return dealerId;
                }
                else
                {
                    throw new UserFriendlyException("There is already a Compartment with given name");
                }
            }
            else
            {
                throw new UserFriendlyException("No Active Financial Year");
            }

        }

        public async Task DeleteDealerAsync(DealerDto input)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);
            if (dealer == null)
            {
                throw new UserFriendlyException("Dealer Year not Found!");
            }
            await _dealerRepository.DeleteAsync(dealer);
        }

        public DealerDto GetDealer(int id)
        {
            var dealer = _dealerRepository.FirstOrDefault(id);

            return dealer.MapTo<DealerDto>();
        }

        public List<DealerDto> GetAllDealers()
        {
            var dealers = _dealerRepository
            .GetAll()      
            .OrderBy(p => p.Name)
            .ToList();

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }



        public List<DealerDto> GetDealers(FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository
             .GetAll()
             .Where(p => p.FinancialYearId == FinancialYear.Id)
             .Where(p => p.ReceiptNumber == null)
             .OrderBy(p => p.Name)
             .ToList();

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }

        public async Task UpdateDealer(DealerDto input)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);

            dealer.SerialNumber = input.SerialNumber;
            dealer.Name = input.Name;
            dealer.Address = input.Address;
            dealer.Email = input.Email;
            dealer.Phone = input.Phone;
            dealer.StationId = input.StationId;
            dealer.RegisteredDate = input.RegisteredDate;
            dealer.TIN = input.TIN;
            dealer.BusinessLicense = input.BusinessLicense;

            await _dealerRepository.UpdateAsync(dealer);
            
        }

        public async Task ConfirmPayment(DealerDto input, string PaymentReference)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);
            if (dealer != null)
            {
                dealer.PaymentReferenceNumber = PaymentReference;
                dealer.ReceiptNumber = PaymentReference;
                await _dealerRepository.UpdateAsync(dealer);
            }
            else
            {
                throw new UserFriendlyException("Dealer not Found!");
            }

        }

        public List<DealerDto> GetRegisteredDealers(FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository
             .GetAll()
             .Where(p => p.FinancialYearId == FinancialYear.Id)
             .Where(p => p.ReceiptNumber != null)
             .OrderBy(p => p.Name)
             .ToList();

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }

        // Sum of Pending  Dealers in a Financial Year
        public int GetTotalDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.PaymentReferenceNumber != null)
                .Count();

            return dealers;
        }

        // Sum of Pending  Dealers in Current Month in a Financial Year
        public int GetTotalMonthDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.PaymentReferenceNumber != null)
                .Where(x => x.RegisteredDate.Month == DateTime.Today.Month && x.RegisteredDate.Year == DateTime.Today.Year)
                .Count();

            return dealers;
        }

        // Sum of Registered Dealers in a Financial Year
        public int GetTotalPendingDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x=> x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.PaymentReferenceNumber == null)
                .Count();

            return dealers;
        }

        // Sum of Registered Dealers in a Month in  Financial Year
        public int GetTotalMonthPendingDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.PaymentReferenceNumber == null)
                .Where(x => x.RegisteredDate.Month == DateTime.Today.Month && x.RegisteredDate.Year == DateTime.Today.Year)
                .Count();

            return dealers;
        }

        //get total fees from dealer registration in a financial year per station

        public List<double> GetTotalDealerFeesByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var fees = from l in _dealerRepository.GetAll()
                       join a in _dealerActivityRepository.GetAll() on l.Id equals a.DealerId
                       join i in _activityRepository.GetAll() on a.ActivityId equals i.Id
                       where l.FinancialYearId == FinancialYear.Id
                       where l.StationId == Station.Id
                       where l.ReceiptNumber != null
                       where l.PaymentReferenceNumber != null
                       group i by l.StationId into g
                       select  g.Sum(x => x.Fee);

            return fees.ToList();
        }

        //get total fees from dealer registration in a financial year in a current month per station

        public List<double> GetTotalMonthDealerFeesByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var fees = from l in _dealerRepository.GetAll()
                       join a in _dealerActivityRepository.GetAll() on l.Id equals a.DealerId
                       join i in _activityRepository.GetAll() on a.ActivityId equals i.Id
                       where l.FinancialYearId == FinancialYear.Id
                       where l.StationId == Station.Id
                       where l.ReceiptNumber != null
                       where l.PaymentReferenceNumber != null
                       where(l.RegisteredDate.Month == DateTime.Today.Month && l.RegisteredDate.Year == DateTime.Today.Year)
                       group i by l.StationId into g
                       select g.Sum(x => x.Fee);

            return fees.ToList();
        }

        

        // Print Certificate

        public List<RegistrationCertDto> PrintDealer(int id)
        {
            var dealer = from d in _dealerRepository.GetAll()
                         join u in _userRepository.GetAll() on d.CreatorUserId equals u.Id
                         where d.Id == id
                         select new RegistrationCertDto {
                                Id = d.Id,
                                SerialNumber = d.SerialNumber,
                                Name = d.Name,
                                Address = d.Address,
                                Email = d.Email,
                                Phone = d.Phone,
                                ReceiptNumber = d.ReceiptNumber,
                                Amount = d.Amount,
                                RegisteredDate = d.RegisteredDate,
                                IssuedDate = d.IssuedDate,
                                Station = d.Station.Name,
                                ExpireYear = (d.FinancialYear.Name).Substring(0,4),
                                AutholizedOfficer = u.Name+" "+u.Surname
                          
                         };

            return dealer.ToList();
        }

     
    }
}
