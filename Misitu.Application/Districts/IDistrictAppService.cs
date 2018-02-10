using Abp.Application.Services;
using Misitu.Districts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Districts
{
    public interface IDistrictAppService: IApplicationService
    {
        List<DistrictDto> GetDistricts();

        List<DistrictDto> GetDistrictsByRegionId(int Id);

        void CreateDistrict(CreateDistrictInput input);

        DistrictDto GetDistrict(int id);

        void UpdateDistrict(DistrictDto input);

        void DeleteDistrict(DistrictDto input);
    }
}
