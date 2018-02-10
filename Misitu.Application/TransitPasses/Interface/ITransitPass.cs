using Abp.Application.Services;
using Abp.Domain.Entities.Auditing;
using Misitu.Applicants;
using Misitu.Billing.Dto;
using Misitu.TransitPasses.Dto;
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

        List<TransitPassPrintout> GetUnPaidTransitPasses();

        List<TransitPassPrintout> GetPaidTransitPasses();

        List<TransitPassPrintout> GetExpiredTransitPasses();

        int CreateTransitPass(CreateTransitPassInput input);

        TransitPassDto GetTransitPass(int id);

        List<BillPrint> getBillByTp(int id);

        BillPrint getBillByTPId(int id);

        List<TransitPassPrintout>  GetTransitPassPrintout(int id);

        TransitPassPrintout GetTransitPassById(int id);

        Task UpdateTransitPass(TransitPassDto input);

        Task DeleteTransitPassAsync(TransitPassDto input);


    }
}
