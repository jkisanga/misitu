using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Misitu.Authorization;
using Misitu.Users.Dto;
using Microsoft.AspNet.Identity;
using System;
using Misitu.Stations;
using Abp.Auditing;
using Misitu.Stations.Dto;

namespace Misitu.Users
{
    /* THIS IS JUST A SAMPLE. */

    [AbpAuthorize]
    public class UserAppService : MisituAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Statiton> _stationRepository;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager, IRepository<Statiton> stationRepository)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _stationRepository = stationRepository;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        public async Task CreateUser(CreateUserInput input, string[] roles)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Surname.ToUpper());
            user.IsEmailConfirmed = true;

            //IdentityResult chkUser = await UserManager.CreateAsync(user,user.Password);

            long userId = _userRepository.InsertAndGetId(user);

            if (userId > 0)
            {              
               await UserManager.AddToRolesAsync(userId, roles);
                           
            }
        }

        public UserDto GetLoggedInUser()
        {
            var loginUser = _userRepository.FirstOrDefault(Convert.ToInt32(AbpSession.UserId));

            return loginUser.MapTo<UserDto>();
        }

        [DisableAuditing]
        public UserLoginDto GetUserLogidInInfo()
        {
                 
            var output = new UserLoginDto();

            if (AbpSession.UserId.HasValue)
            {
                output.user = (GetLoggedInUser()).MapTo<UserDto>();
                output.station = (_stationRepository.FirstOrDefault(output.user.StationId)).MapTo<StationDto>();
            }          

            return output;
        }

    }
}