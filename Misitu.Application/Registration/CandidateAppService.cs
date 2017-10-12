using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Registration.Dto;
using Abp.Domain.Repositories;
using Misitu.FinancialYears.Dto;
using Abp.AutoMapper;
using Misitu.Stations.Dto;

namespace Misitu.Registration
{
    public class CandidateAppService : MisituAppServiceBase, ICandidateAppService
    {

        private readonly IRepository<Candidate> _candidateRepository;


        public CandidateAppService(IRepository<Candidate> candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public void UploadCandidates(DataTable table, FinancialYearDto FinancialYear, StationDto Station)
        {
            foreach (DataRow row in table.Rows)
            {
                var candidate = new Candidate
                {
                    Name = row["NAME"].ToString(),
                    Adress = row["ADDRESS"].ToString(),
                    Phone = row["PHONE"].ToString(),
                    Email = row["EMAIL"].ToString(),
                    AllocatedCubicMetres = Convert.ToDouble(row["ALLOCATED_CUBIC_METRES"].ToString()),
                    Species = row["SPECIES"].ToString(),
                    FinancialYearId = FinancialYear.Id,
                    StationId = Station.Id
                };
                _candidateRepository.Insert(candidate);
            }
        }
       

        public CandidateDto GetCandidate(int id)
        {
            var candidate = _candidateRepository.FirstOrDefault(i => i.Id == id);

            return candidate.MapTo<CandidateDto>();
        }

        public List<CandidateDto> GetCandidates(FinancialYearDto FinancialYear, StationDto Station)
        {
            var candidates = _candidateRepository
               .GetAll()
               .OrderBy(p => p.Name)
               .Where(p => p.FinancialYearId == FinancialYear.Id)
               .Where(p => p.StationId == Station.Id)
               .Where(p => p.IsRegistered == false)
               .ToList();

            return new List<CandidateDto>(candidates.MapTo<List<CandidateDto>>());
        }

        public async Task RegisterCandidate(CandidateDto input)
        {
            var candidate = _candidateRepository.FirstOrDefault(input.Id);
            if(candidate != null)
            {
                candidate.IsRegistered = true;
                await _candidateRepository.UpdateAsync(candidate);
            }
        }

        public Task UpdateCandidate(CandidateDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCandidateAsync(CandidateDto input)
        {
            throw new NotImplementedException();
        }
        //tottal selected candidates
        public int GetTotalCandidatesByStationId(StationDto Station, FinancialYearDto FinancialYear)
        {

            var candidates = _candidateRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Count();

            return candidates; 
        }

        //get applied volume per station
        public double GetTotalAppliedVolumeByStation(StationDto Station, FinancialYearDto FinancialYear)
        {

            var Volume = _candidateRepository.GetAll()
                .Where(x => x.StationId == Station.Id)
                .Where(x => x.FinancialYearId == FinancialYear.Id)
                .Sum(x => (double?)x.AllocatedCubicMetres) ?? 0.00;

            return Volume;
        }
    }
}
