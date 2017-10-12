using Abp.Application.Services;
using Misitu.PlotScalling.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.PlotScalling
{
    public interface ITallySheetAppService : IApplicationService
    {
        List<TallySheetDto> GetTallySheets(PlotDto plot);

        TallySheetDto GetTallySheet(int id);

        void UploadTallySheet(CreateTallySheetInput input,DataTable table);
    }
}
