using System.Threading.Tasks;
using Abp.Application.Services;
using Misitu.Roles.Dto;

namespace Misitu.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
