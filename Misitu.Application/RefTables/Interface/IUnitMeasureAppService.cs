using Abp.Application.Services;
using Misitu.RefTables.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.RefTables.Interface
{
   public interface IUnitMeasureAppService: IApplicationService
    {
        List<UnitMeasureDto> GetUnitMeasures();

        Task CreateUnitMeasure(CreateUnitMeasureInput input);

        UnitMeasureDto GetUnitMeasure(int id);

        Task UpdateUnitMeasure(UnitMeasureDto input);

        Task DeleteUnitMeasure(UnitMeasureDto input);
    }
}
