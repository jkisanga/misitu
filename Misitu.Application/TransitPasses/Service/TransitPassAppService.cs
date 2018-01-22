﻿using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Misitu.Applicants;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.TransitPasses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Service
{
    public class TransitPassAppService : ITransitPass
    {
        private readonly IRepository<Applicant> reporitaryApplicant;
        private readonly IRepository<TransitPass> repositoryTransitpass;
        private readonly IRepository<BillItem> billItemRepository;
        private readonly IRepository<Bill> repositoryBill;

        public TransitPassAppService(IRepository<Applicant> reporitaryApplicant,
            IRepository<TransitPass> repositoryTransitpass, 
            IRepository<Bill> repositoryBill,
            IRepository<BillItem> billItemRepository)
        {
            this.reporitaryApplicant = reporitaryApplicant;
            this.repositoryTransitpass = repositoryTransitpass;
            this.billItemRepository = billItemRepository;
            this.repositoryBill = repositoryBill;
        }

        public int CreateTransitPass(CreateTransitPassInput input)
        {
            var obj = new TransitPass
            {
                ApplicantId = input.ApplicantId,
                BillId = input.BillId,
                LisenceNo = input.LisenceNo,
                IssuedDate = input.IssuedDate,
                OrginalCountry = input.OrginalCountry,
                NoOfConsignment = input.NoOfConsignment,
                TransitPassNo = input.TransitPassNo,
                SourceForest = input.SourceForest,
                ExpireDate = input.ExpireDate,
                SourceName = input.SourceName,
                DestinationId = input.DestinationId,
                DestinationName = input.DestinationName,
                VehcleNo = input.VehcleNo,
                IssuerOfficer = input.IssuerOfficer,
                HummerNo = input.HummerNo,
                HummerMaker = input.HummerMaker,
                HummerStationId = input.HummerStationId,
                AdditionInformation = input.AdditionInformation
            };
            var objExist = this.repositoryTransitpass.FirstOrDefault(a => a.TransitPassNo == input.TransitPassNo);
            if (objExist == null)
            {
                return this.repositoryTransitpass.InsertAndGetId(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }
        }

        public async Task DeleteTransitPassAsync(TransitPassDto input)
        {
            var obj = this.repositoryTransitpass.FirstOrDefault(input.Id);
            if (obj == null)
            {
                throw new UserFriendlyException("Item not Found!");
            }

            await this.repositoryTransitpass.DeleteAsync(obj);
        }

        public TransitPassDto GetTransitPass(int id)
        {
            var obj = this.repositoryTransitpass.FirstOrDefault(id);

            return obj.MapTo<TransitPassDto>();
        }


        //get bill
        public List<BillPrint> getBillByTp(int id)
        {
            var bill = from tp in this.repositoryTransitpass.GetAll()
                       join b in this.repositoryBill.GetAll() on tp.BillId equals b.Id
                       join item in this.billItemRepository.GetAll() on b.Id equals item.BillId
                       where tp.Id == id
                       select new BillPrint
                       {
                           Id = b.Id,
                           PayerName = b.Applicant.Name,
                           PayerAddress = b.Applicant.Adress,
                           PayerPhone = b.Applicant.Phone,
                           Station = b.Station.Name,
                           StationAddress = b.Station.Address,
                           ControlNumber = b.ControlNumber,
                           IssuedDate = b.IssuedDate,
                           ExpireDate = b.ExpiredDate,
                           BilledAmount = b.BillAmount,
                           Currency = b.Currency,
                           Description = b.Description,
                           BillId = item.BillId,
                           ItemDescription = item.Description,
                           Amount = item.Total
                       };

            return new List<BillPrint>(bill.MapTo<List<BillPrint>>());
        }

        //


        public List<TransitPassPrintout> GetTransitPassPrintout(int id)
        {
            
            var printout = from tp in this.repositoryTransitpass.GetAll()
                         join item in this.billItemRepository.GetAll() on tp.BillId equals item.BillId
                         where tp.Id == id
                         select  new TransitPassPrintout
                            {
                                 Id = tp.Id,
                                 Applicant = tp.Applicant.Name,
                                 OrginalCountry = tp.OrginalCountry,
                                 NoOfConsignment = tp.NoOfConsignment,
                                 LisenceNo  = tp.LisenceNo,
                                 TransitPassNo = tp.TransitPassNo,
                                 IssuedDate = tp.IssuedDate,
                                 ExpireDate = tp.ExpireDate,
                                 SourceName = tp.SourceName,
                                 DestinationName = tp.DestinationName,
                                 VehcleNo = tp.VehcleNo,
                                 HummerNo = tp.HummerNo,
                                 HummerMaker = tp.HummerMaker,
                                 AdditionInformation = tp.AdditionInformation,
                                 CreationTime = tp.CreationTime,
                                 ItemDescription = item.Description                                
                            };

            return new List<TransitPassPrintout>(printout.MapTo<List<TransitPassPrintout>>());
        }

        public List<TransitPassDto> GetTransitPasses()
        {
            var values = this.repositoryTransitpass
            .GetAll()
            .OrderBy(p => p.IssuedDate)
            .ToList();

            return new List<TransitPassDto>(values.MapTo<List<TransitPassDto>>());
        }

        public async Task UpdateTransitPass(TransitPassDto input)
        {
            var obj = this.repositoryTransitpass.FirstOrDefault(input.Id);
                obj.ApplicantId = input.ApplicantId;
                obj.LisenceNo = input.LisenceNo;
                obj.IssuedDate = input.IssuedDate;
                obj.OrginalCountry = input.OrginalCountry;
                obj.NoOfConsignment = input.NoOfConsignment;
                obj.TransitPassNo = input.TransitPassNo;
                obj.SourceForest = input.SourceForest;
                obj.ExpireDate = input.ExpireDate;
                obj.SourceName = input.SourceName;
                obj.DestinationId = input.DestinationId;
                obj.DestinationName = input.DestinationName;
                obj.VehcleNo = input.VehcleNo;
                obj.IssuerOfficer = input.IssuerOfficer;
                obj.HummerNo = input.HummerNo;
                obj.HummerMaker = input.HummerMaker;
                obj.HummerStationId = input.HummerStationId;
                obj.AdditionInformation = input.AdditionInformation;
            await this.repositoryTransitpass.UpdateAsync(obj);
        }
    }
}
