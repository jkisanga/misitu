using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Misitu.Activities.Dto;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Activities
{
    public class ActivityAppService: MisituAppServiceBase, IActivityAppService
    {
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<RevenueSource> _revenueResourceRepository;

        public ActivityAppService(IRepository<Activity> activityRepository,
            IRepository<RevenueSource> revenueResourceRepository)
        {
            _activityRepository = activityRepository;
            _revenueResourceRepository = revenueResourceRepository;
        }

        // Activity list
        public List<ActivityDto> GetActivities()
        {
            var activities = _activityRepository
                            .GetAll()
                            .OrderBy(p => p.Description)
                            .ToList();

            return new List<ActivityDto>(activities.MapTo<List<ActivityDto>>());
        }

        //List of activities by revenue source Id
        public List<ActivityDto> GetActivitiesByRevenueSourceId(int Id) {
            var activities = _activityRepository
                           .GetAll()
                           .Where(p => p.RevenueSourceId == Id)
                           .OrderBy(p => p.Description)
                           .ToList();

            return new List<ActivityDto>(activities.MapTo<List<ActivityDto>>());
        }

        //create new Activity
        public async Task CreateActivity(CreateActivityInput input)
        {
            var activity = new Activity {
                RevenueSourceId = input.RevenueSourceId,
                Name = input.Name,
                Description = input.Description,
                Fee = input.Fee,
                RegistrationFee = input.RegistrationFee,
                Flag = input.Flag
                    
            };

            var ActivityExist = _activityRepository.FirstOrDefault(p => p.Description == input.Description);
            if (ActivityExist == null)
            {
                await _activityRepository.InsertAsync(activity);
            }
            else
            {
                throw new UserFriendlyException("There is already a Activity with given name");
            }

        }

        //get edit Activity
        public ActivityDto GetActivity(int id)
        {
            var activity = _activityRepository.FirstOrDefault(id);

            return activity.MapTo<ActivityDto>();

        }

        //update Activity
        public async Task UpdateActivity(ActivityDto input)
        {
            // here aoutomapping can be done;
            var activity = _activityRepository.FirstOrDefault(input.Id);
            activity.RevenueSourceId = input.RevenueSourceId;
            activity.Description = input.Description;
            activity.Fee = input.Fee;
            activity.RegistrationFee = input.RegistrationFee;
            activity.Flag = input.Flag;

            await _activityRepository.UpdateAsync(activity);
        }


        //delete Activity
        public async Task DeleteActivityAsync(ActivityDto input)
        {
            var activity = _activityRepository.FirstOrDefault(input.Id);
            if (activity == null)
            {
                throw new UserFriendlyException("Activity not Found!");
            }

            await _activityRepository.DeleteAsync(activity);

        }
    }
}
