using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Misitu.Users.Dto;
using Misitu.Roles.Dto;

namespace Misitu.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input, string[] roles);

        UserLoginDto GetUserLogidInInfo();

        UserDto GetLoggedInUser();

        UserDto Get(int id);

        Task<ListResultDto<RoleDto>> GetRoles();
    }
}