﻿using Abp.Application.Services;
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
        List<BillItemDto> GetBillItems();

        List<BillItemDto> GetBillItems(int billId);

        void CreateBillItem(CreateBillItemInput input);
        int CreateBillItemAPI(CreateBillItemInput input);

        BillItemDto GetBillItem(int id);

        Task UpdateBillItem(BillItemDto input);

        Task DeleteBillItemAsync(BillItemDto input);
    }
}
