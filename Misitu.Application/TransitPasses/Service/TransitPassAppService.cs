using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Misitu.Applicants;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.Regions;
using Misitu.TransitPasses.Dto;
using Misitu.Users;
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
        private readonly IRepository<Payment> paymentRepository;
        private readonly IRepository<CheckPointTransitPass> repositoryCheckpointTransitpass;
        private readonly IRepository<TransitPassItem> transitPassItemRepository;
        private readonly IRepository<User, long> userRepository;
        private readonly IRepository<District> _districtRepository;

        public TransitPassAppService(IRepository<Applicant> reporitaryApplicant,
            IRepository<TransitPass> repositoryTransitpass, 
            IRepository<Bill> repositoryBill,
            IRepository<Payment> paymentRepository,
            IRepository<CheckPointTransitPass> repositoryCheckpointTransitpass,
            IRepository<BillItem> billItemRepository,
            IRepository<TransitPassItem> transitPassItemRepository,
            IRepository<User, long> userRepository,
            IRepository<District> districtRepository)
        {
            this.reporitaryApplicant = reporitaryApplicant;
            this.repositoryTransitpass = repositoryTransitpass;
            this.billItemRepository = billItemRepository;
            this.repositoryBill = repositoryBill;
            this.paymentRepository = paymentRepository;
            this.repositoryCheckpointTransitpass = repositoryCheckpointTransitpass;
            this.transitPassItemRepository = transitPassItemRepository;
            this.userRepository = userRepository;
            _districtRepository = districtRepository;
        }

        public int CreateTransitPass(CreateTransitPassInput input)
        {
            var obj = new TransitPass
            {
                ApplicantId = input.ApplicantId,
                BillId = input.BillId,
                LisenceNo = input.LisenceNo,
                RegistrationNo = input.RegistrationNo,
                IssuedDate = DateTime.Now,
                OrginalCountry = input.OrginalCountry,
                NoOfConsignment = input.NoOfConsignment,
                TransitPassNo = input.TransitPassNo,
                StationId = input.StationId,
                ExpireDate = input.ExpireDate,
                ExpireDays = input.ExpireDays,
                SourceName = input.SourceName,
                DistrictId = input.DistrictId,
                DestinationName = input.DestinationName,
                VehcleNo = input.VehcleNo,
                IssuerOfficer = input.IssuerOfficer,
                HummerNo = input.HummerNo,
                HummerMaker = input.HummerMaker,
                HummerStationId = input.HummerStationId,
                AdditionInformation = input.AdditionInformation,
                QRCode = input.QRCode
                
            };
         
                return this.repositoryTransitpass.InsertAndGetId(obj);        
        }

        //Delete Transit pass
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


        public List<TransitPassDto> GetTransitPasses()
        {
            var values = this.repositoryTransitpass
            .GetAll()
            .OrderBy(p => p.IssuedDate)
            .ToList();

            return new List<TransitPassDto>(values.MapTo<List<TransitPassDto>>());
        }

        //Unpaid transit passes
        public List<TransitPassPrintout> GetUnPaidTransitPasses() {

            var _a = this.repositoryTransitpass.GetAll();
            var _b = this.paymentRepository.GetAll();
            var tps = (from tp in _a
                       join app in this.reporitaryApplicant.GetAll() on tp.ApplicantId equals app.Id
                       where !(_b.Any( x => x.BillId == tp.BillId))
                       select new TransitPassPrintout
                       {
                           Id = tp.Id,
                           Applicant = app.Name,
                           OrginalCountry = tp.OrginalCountry,
                           NoOfConsignment = tp.NoOfConsignment,
                           LisenceNo = tp.LisenceNo,
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
    

                       }).ToList();
            return new List<TransitPassPrintout>(tps.MapTo<List<TransitPassPrintout>>());
        }

        //Paid Transit Passes
        public List<TransitPassPrintout> GetPaidTransitPasses()
        {

            var tps = (from tp in this.repositoryTransitpass.GetAll()
                      join p in this.paymentRepository.GetAll() on tp.BillId equals p.BillId
                      join app in this.reporitaryApplicant.GetAll() on tp.ApplicantId equals app.Id
                       join user in this.userRepository.GetAll() on tp.CreatorUserId equals user.Id
                       select new TransitPassPrintout
                       {
                           Id = tp.Id,
                           Applicant = app.Name,
                           OrginalCountry = tp.OrginalCountry,
                           NoOfConsignment = tp.NoOfConsignment,
                           LisenceNo = tp.LisenceNo,
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
                           CreatedUser = user.Name,
                           ControlNumber = p.PaymentControlNo,

                       }).ToList();

            return new List<TransitPassPrintout>(tps.MapTo<List<TransitPassPrintout>>());
        }

        //get list of exipired transit passes
        public List<TransitPassPrintout> GetExpiredTransitPasses()
        {
            var tps = (from tp in this.repositoryTransitpass.GetAll()
                       join p in this.paymentRepository.GetAll() on tp.BillId equals p.BillId
                       join app in this.reporitaryApplicant.GetAll() on tp.ApplicantId equals app.Id
                       join user in this.userRepository.GetAll() on tp.CreatorUserId equals user.Id
                       where DateTime.Now > tp.ExpireDate
                       select new TransitPassPrintout
                       {
                           Id = tp.Id,
                           Applicant = app.Name,
                           OrginalCountry = tp.OrginalCountry,
                           NoOfConsignment = tp.NoOfConsignment,
                           LisenceNo = tp.LisenceNo,
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
                           CreatedUser = user.Name,
                           ControlNumber = p.PaymentControlNo,

                       }).ToList();

            return new List<TransitPassPrintout>(tps.MapTo<List<TransitPassPrintout>>());
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

       
        public BillPrint getBillByTPId(int id)
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

            return bill.MapTo<BillPrint>();
        }

        //Get Transit Pass By Id
        public TransitPassPrintout GetTransitPassById(int id)
        {
            var transitPass = (from tp in this.repositoryTransitpass.GetAll()
                               join p in this.paymentRepository.GetAll() on tp.BillId equals p.BillId
                               join app in this.reporitaryApplicant.GetAll() on tp.ApplicantId equals app.Id
                               where tp.Id == id
                               select new TransitPassPrintout
                               {
                                   Id = tp.Id,
                                   Applicant = app.Name,
                                   OrginalCountry = tp.OrginalCountry,
                                   NoOfConsignment = tp.NoOfConsignment,
                                   LisenceNo = tp.LisenceNo,
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

                               }).FirstOrDefault();
            return transitPass.MapTo<TransitPassPrintout>();
        }

        //get Transit Pass By Id with associated checkpoints and Items
        public List<TransitPassPrintout> GetTransitPassPrintout(int id)
        {
            
            var printout = from tp in this.repositoryTransitpass.GetAll()
                           join route in this.repositoryCheckpointTransitpass.GetAll() on tp.Id equals route.TransitPassId
                           join p in this.paymentRepository.GetAll() on tp.BillId equals p.BillId
                           join item in this.transitPassItemRepository.GetAll() on tp.Id equals item.TransitPassId
                           join user in this.userRepository.GetAll() on tp.CreatorUserId equals user.Id
                           join app in this.reporitaryApplicant.GetAll() on tp.ApplicantId equals app.Id
                           join dist in _districtRepository.GetAll() on tp.DistrictId equals dist.Id
                           where tp.Id == id
                            select  new TransitPassPrintout
                            {
                                 Id = tp.Id,
                                 Applicant = app.Name,
                                 OrginalCountry = tp.OrginalCountry,
                                 NoOfConsignment = tp.NoOfConsignment,
                                 LisenceNo  = tp.LisenceNo,
                                 RegistrationNo = tp.RegistrationNo,
                                 TransitPassNo = tp.TransitPassNo,
                                 IssuedDate = tp.IssuedDate,
                                 ExpireDate = tp.ExpireDate,
                                 SourceName = tp.SourceName,
                                 DestinationDistrict = dist.Name,
                                 DestinationName = tp.DestinationName,
                                 VehcleNo = tp.VehcleNo,
                                 HummerNo = tp.HummerNo,
                                 HummerMaker = tp.HummerMaker,
                                 AdditionInformation = tp.AdditionInformation,
                                 QRCode = tp.QRCode,
                                 CreationTime = tp.CreationTime,
                                 CreatedUser = user.Name+" "+user.Surname,
                                 ControlNumber = p.PaymentControlNo,
                                 ItemId = item.Id,
                                 ItemDescription = item.Activity.Description,
                                 Quantity = item.Quantity,
                                 UnitMeasure = item.UnitMeasure.Name,
                                 Specie = item.Specie.CommonName,
                                 CheckpointId = route.Id,
                                 CheckpointName = route.Station.Name
                                                               
                            };

            return new List<TransitPassPrintout>(printout.MapTo<List<TransitPassPrintout>>());
        }

      

        public async Task UpdateTransitPass(TransitPassDto input)
        {
            var obj = this.repositoryTransitpass.FirstOrDefault(input.Id);
                obj.ApplicantId = input.ApplicantId;
                obj.LisenceNo = input.LisenceNo;
                obj.RegistrationNo = input.RegistrationNo;
                obj.IssuedDate = input.IssuedDate;
                obj.OrginalCountry = input.OrginalCountry;
                obj.NoOfConsignment = input.NoOfConsignment;
                obj.TransitPassNo = input.TransitPassNo;
                obj.StationId = input.StationId;
                obj.ExpireDate = input.ExpireDate;
                obj.SourceName = input.SourceName;
                obj.DistrictId = input.DistrictId;
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
