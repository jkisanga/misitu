using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Misitu.Activities.Dto;
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

        public ActivityAppService(IRepository<Activity> activityRepository)
        {
            _activityRepository = activityRepository;
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

        //create new Activity

        public async Task CreateActivity(CreateActivityInput input)
        {
            var activity = new Activity {
                    Description = input.Description,
                    Fee = input.Fee,
                    RegistrationFee = input.RegistrationFee
                    
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
            activity.Description = input.Description;
            activity.Fee = input.Fee;
            activity.RegistrationFee = input.RegistrationFee;

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
