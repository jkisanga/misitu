using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Misitu.MultiTenancy.Dto;

namespace Misitu.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
