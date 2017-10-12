using Abp.Application.Services;
using Misitu.FinancialYears.Dto;
using Misitu.Licensing.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Licensing
{
    public interface ILicenseAppService: IApplicationService
    {
        List<LicenseDto> GetLicenses(FinancialYearDto FinancialYear);

        List<LicenseDto> GetPendingLicenses(FinancialYearDto FinancialYear);

        void CreateLicense(CreateLicenseInput input);

        LicenseDto GetLicense(int id);

        LicenseDto GetLicenseByBill(int billId);

        Task ConfirmPayment(LicenseDto input, string PaymentReference);

        Task UpdateLicense(LicenseDto input);

        Task DeleteLicenseAsync(LicenseDto input);

        int GetTotalLicenseByStationId(int id);
    }
}
