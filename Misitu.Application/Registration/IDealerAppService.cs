using Misitu.FinancialYears.Dto;
using Misitu.Registration.Dto;
using Misitu.Stations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Registration
{
    public interface IDealerAppService
    {
        List<DealerDto> GetDealers(FinancialYearDto FinancialYear);

        List<DealerDto> GetAllDealers();

        List<DealerDto> GetRegisteredDealers(FinancialYearDto FinancialYear);

        int CreateDealer(CreateDealerInput input);

        DealerDto GetDealer(int id);

        List<RegistrationCertDto> PrintDealer(int id);

        void UpdateBillControlNumber(DealerDto input, string BillControlNumber);

        Task UpdateDealer(DealerDto input);

        Task DeleteDealerAsync(DealerDto input);

        double DealerAllocatedCBM(DealerDto input);

        int GetTotalDealerByStationId(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalMonthDealerByStationId(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalPendingDealerByStationId(StationDto Station, FinancialYearDto FinancialYear);

        int GetTotalMonthPendingDealerByStationId(StationDto Station, FinancialYearDto FinancialYear);

        List< double> GetTotalDealerFeesByStation(StationDto Station, FinancialYearDto FinancialYear);

        List< double> GetTotalMonthDealerFeesByStation(StationDto Station, FinancialYearDto FinancialYear);
    }
}
