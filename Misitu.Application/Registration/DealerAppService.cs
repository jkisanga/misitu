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
using Misitu.Billing;

namespace Misitu.Registration
{
    public class DealerAppService : MisituAppServiceBase, IDealerAppService
    {
        private readonly IRepository<Dealer> _dealerRepository;
        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<DealerActivity> _dealerActivityRepository;
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<User, long> _userRepository;

        public DealerAppService(IRepository<Dealer> dealerRepository,
            IRepository<Bill> billRepository,
            IRepository<FinancialYear> financialYearRepository,
            IRepository<DealerActivity> dealerActivityRepository,
            IRepository<Activity> activityRepository,
             IRepository<User, long> userRepository
            )
        {
            _dealerRepository = dealerRepository;
            _billRepository = billRepository;
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
                    ApplicantId = input.ApplicantId,
                    StationId = input.StationId,            
                    FinancialYearId = current.Id,               
                };

                var existingDealer = _dealerRepository.FirstOrDefault(p => p.SerialNumber == input.SerialNumber && p.FinancialYearId == current.Id);
                if (existingDealer == null)
                {
                    var dealerId =  _dealerRepository.InsertAndGetId(dealer);
                    return dealerId;
                }
                else
                {
                    throw new UserFriendlyException("There is already a Dealer with given name");
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
            .OrderBy(p => p.Applicant.Name)
            .ToList();

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }

        //Is application exists for current financial Year
        public bool IsApplicationExists(int id, FinancialYearDto FinancialYear)
        {
            var dealer = _dealerRepository.GetAll()
               .Where(p => p.ApplicantId == id)
               .Where(p => p.FinancialYearId == FinancialYear.Id)
               .FirstOrDefault();
            if(dealer != null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        //Is Registered exists for current financial Year

        public bool IsRegistered(int id, FinancialYearDto FinancialYear)  // Add Registration Criteria
        {
            var dealer = _dealerRepository.GetAll()
               .Where(p => p.ApplicantId == id)
               .Where(p => p.FinancialYearId == FinancialYear.Id)
               .Where(p => p.IsApproved == true)
               .FirstOrDefault();

            if (dealer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //submitted application for registrtation for online user
        public DealerDto GetRegApplication(int applicantId, FinancialYearDto FinancialYear) 
        {
            var dealer = _dealerRepository.GetAll()
                .Where(p => p.ApplicantId == applicantId)
                .Where(p => p.FinancialYearId == FinancialYear.Id)
                .FirstOrDefault();

            return dealer.MapTo<DealerDto>();
        }

        public List<DealerDto> GetRegApplicationByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository
            .GetAll()  
            .Where(p => p.IsSubmitted == true)
            .Where(p => p.IsApproved == false)
            .Where(p => p.IsDenied == false)
            .Where (p => p.StationId == Station.Id)
            .Where (p => p.FinancialYearId == FinancialYear.Id)
            .OrderBy(p => p.CreationTime)
            .ToList();

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }

        public List<DealerDto> GetDeniedRegApplicationByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository
            .GetAll()
            .Where(p => p.IsSubmitted == true)
            .Where(p => p.IsApproved == false)
            .Where(p => p.IsDenied == true)
            .Where(p => p.StationId == Station.Id)
            .Where(p => p.FinancialYearId == FinancialYear.Id)
            .OrderBy(p => p.CreationTime)
            .ToList();

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }



        public List<DealerDto> GetDealers(FinancialYearDto FinancialYear)
        {
            var dealers = from l in _dealerRepository.GetAll()
                          join b in _billRepository.GetAll() on l.ApplicantId equals b.ApplicantId
                          where l.FinancialYearId == FinancialYear.Id
                          where b.PaidAmount == 0 && b.PaidDate == null
                          orderby l.SerialNumber
                          select new DealerDto
                          {
                              Id = l.Id,   
                              ApplicantId = l.ApplicantId,                          
                              Amount = l.Amount,                             
                              BillControlNumber = l.BillControlNumber,                       
                              FinancialYearId = l.FinancialYearId,
                              IssuedDate = l.IssuedDate,
                              StationId = l.StationId,
                              SerialNumber = l.SerialNumber,                        
                          };

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }

        public void UpdateDealer(DealerDto input)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);

            dealer.IsSubmitted = input.IsSubmitted;
    
             _dealerRepository.Update(dealer);
            
        }

        public void UpdateBillControlNumber(DealerDto input,string BillControlNumber)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);
            if (dealer != null)
            {
                dealer.BillControlNumber = BillControlNumber;
                _dealerRepository.UpdateAsync(dealer);
            }
            else
            {
                throw new UserFriendlyException("Dealer not Found!");
            }

        }

        public List<DealerDto> GetRegisteredDealers(FinancialYearDto FinancialYear)
        {
        

            var dealers = from l in _dealerRepository.GetAll()
                       join b in _billRepository.GetAll() on l.ApplicantId equals b.ApplicantId
                       where l.FinancialYearId == FinancialYear.Id
                       where b.PaidAmount > 0 && b.PaidDate != null
                       orderby l.SerialNumber
                       select new DealerDto {
                            Id = l.Id,
                            ApplicantId = l.ApplicantId,
                            Amount = b.PaidAmount,
                            BillControlNumber = l.BillControlNumber,
                            FinancialYearId = l.FinancialYearId,
                            IssuedDate = l.IssuedDate,
                            StationId = l.StationId,
                            SerialNumber= l.SerialNumber
                            
                       };

            return new List<DealerDto>(dealers.MapTo<List<DealerDto>>());
        }

        // Sum of Pending  Dealers in a Financial Year
        public int GetTotalDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.BillControlNumber != null)
                .Count();

            return dealers;
        }

        // Sum of Pending  Dealers in Current Month in a Financial Year
        public int GetTotalMonthDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.BillControlNumber != null)
                .Where(x => x.CreationTime.Month == DateTime.Today.Month && x.CreationTime.Year == DateTime.Today.Year)
                .Count();

            return dealers;
        }

        // Sum of Registered Dealers in a Financial Year
        public int GetTotalPendingDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x=> x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.BillControlNumber == null)
                .Count();

            return dealers;
        }

        // Sum of Registered Dealers in a Month in  Financial Year
        public int GetTotalMonthPendingDealerByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {
            var dealers = _dealerRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(x => x.BillControlNumber == null)
                .Where(x => x.CreationTime.Month == DateTime.Today.Month && x.CreationTime.Year == DateTime.Today.Year)
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
                       //where l.ReceiptNumber != null
                       where l.BillControlNumber != null
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
                       //where l.ReceiptNumber != null
                       where(l.CreationTime.Month == DateTime.Today.Month && l.CreationTime.Year == DateTime.Today.Year)
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
                                Name = d.Applicant.Name,
                                Address = d.Applicant.Adress,
                                Email = d.Applicant.Email,
                                Phone = d.Applicant.Name,                     
                                Amount = d.Amount,
                                IssuedDate = d.IssuedDate,
                                Station = d.Station.Name,
                                ExpireYear = (d.FinancialYear.Name).Substring(0,4),
                                AutholizedOfficer = u.Name+" "+u.Surname
                          
                         };

            return dealer.ToList();
        }

        //Pre selected  dealer's allocated volume

        public double DealerAllocatedCBM(DealerDto input)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);
             
            return 99;
        }

        //approve application for registration
        public int ApproveRegistration(int id)
        {
            var dealer = _dealerRepository.FirstOrDefault(id);
            if(dealer != null && dealer.IsSubmitted == true)
            {
                dealer.IsApproved = true;
                dealer.ApprovedUserId = (int)AbpSession.UserId;

                return _dealerRepository.InsertOrUpdateAndGetId(dealer);

            }else
            {
                throw new UserFriendlyException("Application for Registration not Found!");
            }
        }

        //Deny Registration
        public void DenyRegistration(DealerDto input)
        {
            var dealer = _dealerRepository.FirstOrDefault(input.Id);
            if (dealer != null && dealer.IsSubmitted == true)
            {
                dealer.IsApproved = false;
                dealer.IsDenied = true;
                dealer.ApprovedUserId = (int)AbpSession.UserId;
                dealer.Remark = input.Remark;

              _dealerRepository.Update(dealer);

            }
            else
            {
                throw new UserFriendlyException("Application for Registration not Found!");
            }
        }
    }
}
