using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using Misitu.Sessions.Dto;
using Misitu.Stations.Dto;
using Abp.Domain.Repositories;
using Misitu.Stations;

namespace Misitu.Sessions
{
    public class SessionAppService : MisituAppServiceBase, ISessionAppService
    {
      
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput();

            if (AbpSession.UserId.HasValue)
            {
                output.User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>();
            }

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            return output;
        }
    }
}