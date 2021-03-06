﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Billing.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;

namespace Misitu.Billing
{
    public class BillItemAppService : MisituAppServiceBase, IBillItemAppService
    {


        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<BillItem> _billItemRepository;

        public BillItemAppService(IRepository<Bill> billRepository,
            IRepository<BillItem> billItemRepository)
        {
            _billRepository = billRepository;
            _billItemRepository = billItemRepository;
        }

        public void CreateBillItem(CreateBillItemInput input)
        {
            double total = input.Total + input.Loyality + input.LMDA + input.TFF + input.VAT + input.CESS + input.TP + input.DataSheet + input.Others;

            var billItem = new BillItem
            {
                BillId = input.BillId,
                ActivityId = input.ActivityId,
                Description = input.Description,
                Loyality = input.Loyality,
                TFF = input.TFF,
                LMDA= input.LMDA,
                VAT = input.VAT,
                CESS = input.CESS,
                TP = input.TP,
                DataSheet =input.DataSheet,
                Others =input.Others,
                EquvAmont = input.EquvAmont,
                MiscAmont = input.MiscAmont,
                GfsCode = input.GfsCode,
                Total = total,
                Quantity = input.Quantity

            };

             _billItemRepository.InsertAsync(billItem);
        }




        public int CreateBillItemAPI(CreateBillItemInput input)
        {
            double total = input.Total + input.Loyality + input.LMDA + input.TFF + input.VAT + input.CESS + input.TP + input.DataSheet + input.Others;

            var billItem = new BillItem
            {
                BillId = input.BillId,
                ActivityId = input.ActivityId,
                Description = input.Description,
                Loyality = input.Loyality,
                TFF = input.TFF,
                LMDA = input.LMDA,
                VAT = input.VAT,
                CESS = input.CESS,
                TP = input.TP,
                DataSheet = input.DataSheet,
                Others = input.Others,
                EquvAmont = input.EquvAmont,
                MiscAmont = input.MiscAmont,
                GfsCode = input.GfsCode,
                Total = total,
                Quantity = input.Quantity

            };

          int obj =  _billItemRepository.InsertAndGetId(billItem);
            return obj;
        }


        public async Task DeleteBillItemAsync(BillItemDto input)
        {
            var billItem = _billItemRepository.FirstOrDefault(input.Id);
            if (billItem == null)
            {
                throw new UserFriendlyException("Bill Item not Found!");
            }
            await _billItemRepository.DeleteAsync(billItem);
        }

        public BillItemDto GetBillItem(int id)
        {
            var item = _billItemRepository.FirstOrDefault(id);

            return item.MapTo<BillItemDto>();
        }

        public List<BillItemDto> GetBillItems(BillDto bill)
        {
            var items = _billItemRepository
               .GetAll()
               .Where(p => p.BillId == bill.Id)
               .OrderBy(p => p.Description)
               .ToList();

            return new List<BillItemDto>(items.MapTo<List<BillItemDto>>());
        }


        public List<BillItemDto> GetBillItems()
        {
            var items = _billItemRepository
               .GetAll()
               
               .OrderBy(p => p.Description)
               .ToList();

            return new List<BillItemDto>(items.MapTo<List<BillItemDto>>());
        }



        public List<BillItemDto> GetBillItems(int billId)
        {
            var items = _billItemRepository
              
               .GetAll()
               .Where(p => p.BillId == billId)
               .OrderBy(p => p.Description)
               .ToList();

            return new List<BillItemDto>(items.MapTo<List<BillItemDto>>());
        }

        public async Task UpdateBillItem(BillItemDto input)
        {
            var item = _billItemRepository.FirstOrDefault(input.Id);
            item.BillId = input.BillId;
            item.Description = input.Description;
            item.Loyality = input.Loyality;
            item.TFF = input.TFF;
            item.LMDA = input.LMDA;
            item.VAT = input.VAT;
            item.CESS = input.CESS;
            item.TP = input.TP;
            item.DataSheet = input.DataSheet;
            item.Others = input.Others;
            item.Total = input.Total;


            await _billItemRepository.UpdateAsync(item);
        }
     
    }
}
