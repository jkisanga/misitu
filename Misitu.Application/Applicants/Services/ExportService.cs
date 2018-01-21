using Misitu.Applicants.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Applicants.Dto.ExportImport;
using Misitu.Applicants.ExportImport;
using Abp.Domain.Repositories;
using Misitu.FinancialYears;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.Applicants.Services
{
    public class ExportService : IExportService
    {
        private readonly IRepository<ExportDetail> _exportDetailRepository;
        private readonly IRepository<FinancialYear> _financialYearRepository;
        private readonly IRepository<ExportSpecie> _exportSpecieRepository;
        private readonly IRepository<ExportAttachment> _exportAttachmentRepository;

        public ExportService(IRepository<ExportDetail> exportDetailRepository,
            IRepository<ExportSpecie> exportSpecieRepository,
            IRepository<ExportAttachment> exportAttachmentRepository,
            IRepository<FinancialYear> financialYearRepository)
        {
            _exportDetailRepository = exportDetailRepository;
            _financialYearRepository = financialYearRepository;
            _exportSpecieRepository = exportSpecieRepository;
            _exportAttachmentRepository = exportAttachmentRepository;
        }

     

        //Create
        public int CreateAndReturnId(CreateExportDetail input)
        {
            try
            {
                //get current active financial year;
                var currentYr = _financialYearRepository.FirstOrDefault(c => c.IsActive == true);

                var export = new ExportDetail
                {
                    ApplicantId = input.ApplicantId,
                    SpecieCategoryId = input.SpecieCategoryId,
                    StationId = input.StationId,
                    FinancialYearId = currentYr.Id,
                    BankName = input.BankName,
                    BankAddress = input.BankAddress,
                    Destination = input.Destination,
                    ShipmentDate = input.ShipmentDate

                };

                return _exportDetailRepository.InsertAndGetId(export);
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        //get export detail by id
        public ExportDetailDto getExportDetailById(int id)
        {
            try
            {
                var export = _exportDetailRepository.FirstOrDefault(id);
                return export.MapTo<ExportDetailDto>();
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        //Add species
        public void AddSpecies(CreateExportSpecie input)
        {
            try
            {
                var exportSpecie = new ExportSpecie
                {
                   ExportDetailId = input.ExportDetailId,
                   SpecieId = input.SpecieId,
                   Quantity = input.Quantity,
                   Size = input.Size,
                   Price = input.Price

                };

                _exportSpecieRepository.Insert(exportSpecie);
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        // get list of exported species  
        public List<ExportSpecieDto> getExportSpeciesByExportDetailId(int Id)
        {
            try
            {
                var species = _exportSpecieRepository.GetAll().Where(x => x.ExportDetailId == Id).ToList();
                return species.MapTo<List<ExportSpecieDto>>();
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

        }

        // Add export attachmnents
        public void AddAttachments(CreateExportAttachment input)
        {
            try
            {
                var attachments = new ExportAttachment
                {
                    ExportDetailId = input.ExportDetailId,
                    BrelaRegistrationCert = input.BrelaRegistrationCert,
                    LicenceCert = input.LicenceCert,
                    TaxClearanceCert = input.TaxClearanceCert,
                    EnquiryOrder = input.EnquiryOrder,
                    ExportReturns = input.ExportReturns,
                    ForestProduceRegCert = input.ForestProduceRegCert,
                    AutholizedLetter = input.AutholizedLetter,
                    SawMillerContract = input.SawMillerContract,
                    MouCert = input.MouCert
                   
                };

                _exportAttachmentRepository.Insert(attachments);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            };
        }
        
       public  List<ExportDetailDto> getExportsByApplicantId(int Id)
        {
            try
            {
                var permits = _exportDetailRepository.GetAll().Where(x => x.ApplicantId == Id).ToList();
                return permits.MapTo<List<ExportDetailDto>>();
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }
    }
}
