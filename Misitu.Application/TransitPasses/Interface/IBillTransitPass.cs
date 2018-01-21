using Abp.Application.Services;
using Abp.Domain.Entities.Auditing;
using Misitu.Billing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
  public  interface IBillTransitPass : IApplicationService
    {

        List<BillTransitPassDto> GetBillTransitPasses();

        int CreateBillTransitPass(CreateBillTransitPass input);

        BillTransitPassDto GetBillTransitPass(int id);

        Task UpdateBillTransitPass(BillTransitPassDto input);

        Task DeleteBillTransitPassAsync(BillTransitPassDto input);

    }
}
