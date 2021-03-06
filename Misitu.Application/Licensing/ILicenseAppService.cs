﻿using Abp.Application.Services;
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
        List<LicenseView> GetLicenses(FinancialYearDto FinancialYear);

        List<LicenseView> GetPendingLicenses(FinancialYearDto FinancialYear);

        void CreateLicense(CreateLicenseInput input);

        LicenseDto GetLicense(int id);

        LicenseDto GetLicenseByBill(int billId);
        List<LicenceCertDto> PrintLicence(int id);

        Task UpdateLicense(LicenseDto input);

        Task DeleteLicenseAsync(LicenseDto input);

        int GetTotalLicenseByStationId(int id);
    }
}
