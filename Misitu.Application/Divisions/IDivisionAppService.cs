using Abp.Application.Services;
using Misitu.Divisions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Divisions
{
    public interface IDivisionAppService: IApplicationService
    {
        List<DivisionDto> GetDivisions();

        Task CreateDivision(CreateDivisionInput input);

        DivisionDto GetDivision(int id);

        Task UpdateDivision(DivisionDto input);

        Task DeleteDivisionAsync(DivisionDto input);
    }
}
