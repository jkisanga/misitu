using Misitu.Billing.Dto;
using Misitu.FinancialYears.Dto;
using Misitu.Stations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing
{
    public interface IBillAppService
    {
        List<BillPrint> Print(int id);

        List<BillDto> GetBills(FinancialYearDto FinancialYear);

        List<BillDto> GetPayedBills(FinancialYearDto FinancialYear);

        int CreateBill(CreateBillInput input);

        BillDto GetBill(int id);

        Task ConfirmBill(BillDto input, double PaidAmount);

        Task UpdateBill(BillDto input);

        Task DeleteBillAsync(BillDto input);

        int GetTotalBillsByStation(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalPaidBillsByStation(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalPendingBillsByStation(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalMonthBillsByStation(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalMonthPendingBillsByStation(StationDto Station, FinancialYearDto FinancialYear);

        List<double> GetTotalBillsAmountByStation(StationDto Station, FinancialYearDto FinancialYear);

        List<double> GetTotalMonthBillsAmountByStation(StationDto Station, FinancialYearDto FinancialYear);
    }
}
