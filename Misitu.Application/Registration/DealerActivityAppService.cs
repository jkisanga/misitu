using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Registration.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.Registration
{
    public class DealerActivityAppService : MisituAppServiceBase, IDealerActivityAppService
    {
        private readonly IRepository<DealerActivity> _dealerActivityRepository;

        public DealerActivityAppService(IRepository<DealerActivity> dealerActivityRepository)
        {
            _dealerActivityRepository = dealerActivityRepository;
        }


        public void CreateDealerActivity(CreateDealerActivityInput input)
        {
            var dealerActivity = new DealerActivity
            {
                ActivityId = input.ActivityId,
                DealerId = input.DealerId
            };

         
            _dealerActivityRepository.InsertAsync(dealerActivity);
           
        }

        public async Task DeleteDealerActivityAsync(DealerActivityDto input)
        {
            var dealerActivity = _dealerActivityRepository.FirstOrDefault(input.Id);
            if (dealerActivity == null)
            {
                throw new UserFriendlyException("Plot Year not Found!");
            }
            await _dealerActivityRepository.DeleteAsync(dealerActivity);
        }

        public DealerActivityDto GetDealerActivity(int id)
        {
            var dealerActivity = _dealerActivityRepository.FirstOrDefault(id);

            return dealerActivity.MapTo<DealerActivityDto>();
        }

        public List<DealerActivityDto> GetDealerActivities(DealerDto dealer)
        {
            var activities = _dealerActivityRepository.GetAllList(p => p.DealerId == dealer.Id).OrderBy(p => p.Activity.Description)
                 .ToList();
            return new List<DealerActivityDto>(activities.MapTo<List<DealerActivityDto>>());
        }
    }
}
