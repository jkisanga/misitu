using Abp.Application.Services;
using Misitu.Activities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Activities
{
    public interface IActivityAppService:IApplicationService
    {
        List<ActivityDto> GetActivities();

        List<ActivityDto> GetActivitiesByRevenueSourceId(int Id);

        Task CreateActivity(CreateActivityInput input);

        ActivityDto GetActivity(int id);

        Task UpdateActivity(ActivityDto input);

        Task DeleteActivityAsync(ActivityDto input);
    }
}
