using Abp.Application.Services;
using Abp.Domain.Entities.Auditing;
using Misitu.Applicants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
   public interface ITransitPass: IApplicationService
    {

        List<TransitPassDto> GetTransitPasses();

        int CreateTransitPass(CreateTransitPassInput input);

        TransitPassDto GetTransitPass(int id);

        Task UpdateTransitPass(TransitPassDto input);

        Task DeleteTransitPassAsync(TransitPassDto input);


    }
}
