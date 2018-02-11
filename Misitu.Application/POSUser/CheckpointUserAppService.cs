using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.POSUser.Dto;
using Abp.Domain.Repositories;
using Misitu.TransitPasses;
using Abp.AutoMapper;
using Abp.UI;

namespace Misitu.POSUser
{
    class CheckpointUserAppService : ICheckpointUserAppService
    {
        private readonly IRepository<CheckpointUser> repositoryCheckpointUser;

        public CheckpointUserAppService(IRepository<CheckpointUser> repositoryCheckpointUser)
        {
            this.repositoryCheckpointUser = repositoryCheckpointUser;
        }

        public async Task CreateCheckpointUser(CreateCheckpointUser input)
        {
            var cu = new CheckpointUser
            {
                UserId = input.UserId,
                CheckpointId = input.CheckpointId,
                CheckpointName = input.CheckpointName,
                OfficerName = input.OfficerName,
                Addtioninfo = input.Addtioninfo,
                Username = input.Username,
                Password = input.Password,
                POSId = input.POSId,


            };

            var UserExist = this.repositoryCheckpointUser.FirstOrDefault(p => p.UserId == input.UserId);
            if (UserExist == null)
            {
                await this.repositoryCheckpointUser.InsertAsync(cu);
            }
            else
            {
                throw new UserFriendlyException("There is already a User with given name");
            }
        }

        public async Task DeleteCheckpointUserAsync(CheckpointUserDto input)
        {
            var cu = this.repositoryCheckpointUser.FirstOrDefault(input.Id);
            if (cu == null)
            {
                throw new UserFriendlyException("Activity not Found!");
            }

            await this.repositoryCheckpointUser.DeleteAsync(cu);
        }

        public CheckpointUserDto GetCheckpointUser(int id)
        {
            var cu = this.repositoryCheckpointUser.FirstOrDefault(id);

            return cu.MapTo<CheckpointUserDto>();
        }

        public List<CheckpointUserDto> GetCheckpointUserById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CheckpointUserDto> GetCheckpoitUsers()
        {

            var checkpointUsers = this.repositoryCheckpointUser
            .GetAll()
            .OrderBy(p => p.OfficerName)
            .ToList();

            return new List<CheckpointUserDto>(checkpointUsers.MapTo<List<CheckpointUserDto>>());
        }

        public async Task UpdateCheckpointUser(CheckpointUserDto input)
        {
            var cu = this.repositoryCheckpointUser.FirstOrDefault(input.Id);
            cu.UserId = input.UserId;
            cu.CheckpointId = input.CheckpointId;
            cu.POSId = input.POSId;
            cu.Addtioninfo = input.Addtioninfo;
            

            await this.repositoryCheckpointUser.UpdateAsync(cu);
        }
    }
}
