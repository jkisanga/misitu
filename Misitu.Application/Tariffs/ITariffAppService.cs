using Abp.Application.Services;
using Misitu.Tariffs.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Misitu.Tariffs
{
    public interface ITariffAppService: IApplicationService
    {
        List<TariffDto> GetTariffs();

        TariffDto GetTariff(int id);

        void UploadTariff(DataTable table);
    }
}
