using Abp.Application.Services;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses
{
  public  interface IInspectionAudit : IApplicationService
    {

        List<InspectionAuditDto> GetInspectionAudites();

        int CreateInspectionAudit(CreateInspectionAudit input);

        InspectionAuditDto GetInspectionAudit(int id);

        Task UpdateInspectionAudit(InspectionAuditDto input);

        Task DeleteInspectionAuditAsync(InspectionAuditDto input);
    }
}
