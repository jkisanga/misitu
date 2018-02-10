using Abp.Application.Services;
using Abp.Domain.Entities.Auditing;
using Misitu.Stations;
using Misitu.TransitPasses.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
  public  interface ICheckPointTransitPass : IApplicationService
    {

        List<CheckPointTransitPassDto> GetCheckPointTransitPasses();

        List<CustomTransitPassCheckpointDto> GetCheckPointsByTransitPassId(int id);

        int CreateCheckPointTransitPass(CreateCheckPointTransitPass input);

        CheckPointTransitPassDto GetCheckPointTransitPass(int id);

        Task UpdateCheckPointTransitPass(CheckPointTransitPassDto input);

        Task DeleteCheckPointTransitPassAsync(CheckPointTransitPassDto input);

    }
}
