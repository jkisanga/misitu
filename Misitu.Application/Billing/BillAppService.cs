using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Billing.Dto;
using Abp.Domain.Repositories;
using Misitu.FinancialYears;
using Abp.UI;
using Abp.AutoMapper;
using Misitu.Users;
using Misitu.FinancialYears.Dto;
using Misitu.Stations.Dto;


namespace Misitu.Billing
{

    public class BillAppService : MisituAppServiceBase, IBillAppService
    {

        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<BillItem> _billItemRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<User, long> _userRepository;

        public BillAppService(
            IRepository<Bill> billRepository,
            IRepository<BillItem> billItemRepository,
            IRepository<FinancialYear> financialYearRepository,
            IRepository<User, long> userRepository
            )
        {
            _billRepository = billRepository;
            _billItemRepository = billItemRepository;
            _financialYearRepository = financialYearRepository;
            _userRepository = userRepository;
        }

    

        public int CreateBill(CreateBillInput input)
        {
            //get current active financial year;
            var current = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);
            var loginUser = _userRepository.FirstOrDefault(Convert.ToInt32(AbpSession.UserId));

            if (current != null & loginUser != null)
            {
                var bill = new Bill
                {
                    DealerId = input.DealerId,
                    Description = input.Description,
                    BillAmount = input.BillAmount,
                    Currency = input.Currency,
                    IssuedDate = DateTime.Now,
                    ExpiredDate = input.ExpiredDate,                     
                    FinancialYearId = current.Id,
                    StationId = loginUser.StationId
                    };
               
                    var billId = _billRepository.InsertAndGetId(bill);
                    return billId;           
            }
            else
            {
                throw new UserFriendlyException("No Active Financial Year");
            }
        }

        public async Task DeleteBillAsync(BillDto input)
        {
            var bill = _billRepository.FirstOrDefault(input.Id);
            if (bill == null)
            {
                throw new UserFriendlyException("Bill  not Found!");
            }
            await _billRepository.DeleteAsync(bill);
        }

        public BillDto GetBill(int id)
        {
            var bill = _billRepository.FirstOrDefault(id);

            return bill.MapTo<BillDto>();
        }

        public List<BillDto> GetBills(FinancialYearDto FinancialYear)
        {
            var bills = _billRepository
              .GetAll()
              .Where(p => p.PaidAmount == 0)
              .Where(p => p.FinancialYearId == FinancialYear.Id)
              .OrderByDescending(p => p.IssuedDate)
              .ToList();

            return new List<BillDto>(bills.MapTo<List<BillDto>>());
        }

        public List<BillDto> GetPayedBills(FinancialYearDto FinancialYear)
        {
            var bills = _billRepository
            .GetAll()
            .Where(p => p.PaidAmount > 0)
            .Where(p => p.FinancialYearId == FinancialYear.Id)
            .OrderByDescending(p => p.IssuedDate)
            .ToList();

            return new List<BillDto>(bills.MapTo<List<BillDto>>());
        }

        public async Task UpdateBill(BillDto input)
        {
            var bill = _billRepository.FirstOrDefault(input.Id);
            bill.DealerId = input.DealerId;
            bill.IssuedDate = DateTime.Now;
            bill.StationId = input.StationId;
            await _billRepository.UpdateAsync(bill);
        }

        public async Task ConfirmBill(BillDto input, double PaidAmount)
        {
            var bill = _billRepository.FirstOrDefault(input.Id);

            if(bill != null)
            {
                if(bill.BillAmount <= PaidAmount)
                {
                    bill.PaidDate = DateTime.Now;
                    bill.PaidAmount = PaidAmount;
                    await _billRepository.UpdateAsync(bill);
                }
                else
                {
                    throw new UserFriendlyException("Confirmed amount is less than billed amount!");
                }
               
            }
            else
            {
                throw new UserFriendlyException("Bill not Found!");
            }
            
        }
        // Sum of  Bills in a Financial Year by station
        public int GetTotalBillsByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
          
            var bills = _billRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Count();

            return bills;
        }

        public int GetTotalPaidBillsByStation(StationDto Station, FinancialYearDto FinancialYear)
        {

            var bills = _billRepository.GetAll()
                 .Where(p => p.PaidAmount > 0)
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Count();

            return bills;
        }

        public int GetTotalPendingBillsByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                 .Where(p => p.PaidAmount == 0)
                 .Where(p => p.FinancialYearId == FinancialYear.Id)
                 .Where(p => p.StationId == Station.Id)        
                 .Count();

            return bills;
        }

        public int GetTotalMonthBillsByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                    .Where(p => p.PaidAmount > 0)
                    .Where(p => p.FinancialYearId == FinancialYear.Id)
                    .Where(p => p.StationId == Station.Id)
                     .Where(x => x.IssuedDate.Month == DateTime.Today.Month && x.IssuedDate.Year == DateTime.Today.Year)
                    .Count();
            return bills;
        }

        public int GetTotalMonthPendingBillsByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Where(p => p.PaidAmount == 0)
                .Where(x => x.IssuedDate.Month == DateTime.Today.Month && x.IssuedDate.Year == DateTime.Today.Year)
                .Count();

            return bills;
        }

       public  List<double> GetTotalPendingBillsAmountByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                    .Where(p => p.PaidAmount == 0)
                    .Where(p => p.FinancialYearId == FinancialYear.Id)
                    .Where(p => p.StationId == Station.Id)
                    .Select(p => p.BillAmount)
                    .ToList();
            return bills;
        }

        public List<double> GetTotalPendingMonthBillsAmountByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                    .Where(p => p.PaidAmount == 0)
                    .Where(p => p.FinancialYearId == FinancialYear.Id)
                    .Where(p => p.StationId == Station.Id)
                     .Where(x => x.IssuedDate.Month == DateTime.Today.Month && x.IssuedDate.Year == DateTime.Today.Year)
                    .Select(p => p.BillAmount)
                    .ToList();
            return bills;
        }


        public List<BillPrint> Print(int id)
        {
            var bill = from b in _billRepository.GetAll()
                       join item in _billItemRepository.GetAll() on b.Id equals item.BillId                      
                       where b.Id == id
                       select new BillPrint {
                            Id = b.Id,
                            PayerName = b.Dealer.Name,
                            PayerAddress = b.Dealer.Address,
                            PayerPhone = b.Dealer.Phone,
                            Station = b.Station.Name,
                            StationAddress = b.Station.Address,
                            ControlNumber = b.ControlNumber,
                            IssuedDate = b.IssuedDate,
                            BilledAmount = b.BillAmount,
                            Currency = b.Currency,
                            BillId = item.BillId,
                            Description = item.Description,
                            Amount = item.Total                            
                       };   

            return new List<BillPrint>(bill.MapTo<List<BillPrint>>()); ;
        }


        public List<double> GetTotalPaymentsAmountByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                    .Where(p => p.PaidAmount > 0)
                    .Where(p => p.FinancialYearId == FinancialYear.Id)
                    .Where(p => p.StationId == Station.Id)
                    .Select(p => p.PaidAmount)
                    .ToList();
            return bills;
        }

        public List<double> GetTotalMonthPaymentsAmountByStation(StationDto Station, FinancialYearDto FinancialYear)
        {
            var bills = _billRepository.GetAll()
                    .Where(p => p.PaidAmount > 0)
                    .Where(p => p.FinancialYearId == FinancialYear.Id)
                    .Where(p => p.StationId == Station.Id)
                     .Where(x => x.IssuedDate.Month == DateTime.Today.Month && x.IssuedDate.Year == DateTime.Today.Year)
                    .Select(p => p.PaidAmount)
                    .ToList();
            return bills;
        }
    }
}
