using Abp.Application.Services;
using Misitu.Registration.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Registration
{
    public interface IDealerActivityAppService: IApplicationService
    {
        List<DealerActivityDto> GetDealerActivities(DealerDto dealer);

        Task CreateDealerActivity(CreateDealerActivityInput input);

        DealerActivityDto GetDealerActivity(int id);

        Task DeleteDealerActivityAsync(DealerActivityDto input);
    }
}
