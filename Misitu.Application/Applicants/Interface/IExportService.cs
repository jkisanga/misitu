using Abp.Application.Services;
using Misitu.Applicants.Dto.ExportImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Applicants.Interface
{
    public interface IExportService: IApplicationService
    {
        int CreateAndReturnId(CreateExportDetail input);

        ExportDetailDto getExportDetailById(int id);

        void AddSpecies(CreateExportSpecie input);

        List<ExportSpecieDto> getExportSpeciesByExportDetailId(int Id);

        void AddAttachments(CreateExportAttachment input);

        List<ExportDetailDto> getExportsByApplicantId(int Id);
    }
}
