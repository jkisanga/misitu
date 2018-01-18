using Abp.Application.Services;
using Misitu.Billing.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.Billing
{
    public interface IBillItemAppService:IApplicationService
    {

        List<BillItemDto> GetBillItems(BillDto bill);

        int CreateBillItem(CreateBillItemInput input);

        BillItemDto GetBillItem(int id);

        Task UpdateBillItem(BillItemDto input);

        Task DeleteBillItemAsync(BillItemDto input);
    }
}
