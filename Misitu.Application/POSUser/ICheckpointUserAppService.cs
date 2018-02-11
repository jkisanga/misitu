using Abp.Application.Services;
using Misitu.POSUser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.POSUser
{
  public  interface ICheckpointUserAppService :IApplicationService
    {
        List<CheckpointUserDto> GetCheckpoitUsers();

        List<CheckpointUserDto> GetCheckpointUserById(int id);

        Task CreateCheckpointUser(CreateCheckpointUser input);

        CheckpointUserDto GetCheckpointUser(int id);

        Task UpdateCheckpointUser(CheckpointUserDto input);


        Task DeleteCheckpointUserAsync(CheckpointUserDto input);
    }
}
