using Abp.Application.Services;
using Misitu.FinancialYears.Dto;
using Misitu.Registration.Dto;
using Misitu.Stations.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Registration
{
    public interface ICandidateAppService: IApplicationService
    {
        List<CandidateDto> GetCandidates(FinancialYearDto FinancialYear,StationDto Station);

        CandidateDto GetCandidate(int id);

        void UploadCandidates(DataTable table, FinancialYearDto FinancialYear, StationDto Station);

        Task RegisterCandidate(CandidateDto input);

        Task UpdateCandidate(CandidateDto input);

        Task DeleteCandidateAsync(CandidateDto input);

        int GetTotalCandidatesByStationId(StationDto Station, FinancialYearDto FinancialYear);

        double GetTotalAppliedVolumeByStation(StationDto Station, FinancialYearDto FinancialYear);
    }
}
