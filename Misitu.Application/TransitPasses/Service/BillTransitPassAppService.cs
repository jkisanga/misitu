using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Service
{
    public class BillTransitPassAppService : IBillTransitPass
    {
        private readonly IRepository<BillTransitPass> repositoryBillTransitpass;

        public BillTransitPassAppService(IRepository<BillTransitPass> repositoryBillTransitpass)
        {
            this.repositoryBillTransitpass = repositoryBillTransitpass;
        }

        public int CreateBillTransitPass(CreateBillTransitPass input)
        {
            var obj = new BillTransitPass
            {
                TransitPassId = input.TransitPassId,
                BillId = input.BillId,
                AdditionInformation = input.AdditionInformation
            };
            var objExist = this.repositoryBillTransitpass.FirstOrDefault(a => a.BillId == input.BillId && a.TransitPassId == input.TransitPassId);
            if (objExist == null)
            {
                return this.repositoryBillTransitpass.InsertAndGetId(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Exist");
            }


        }

        public async Task DeleteBillTransitPassAsync(BillTransitPassDto input)
        {
            var obj = this.repositoryBillTransitpass.FirstOrDefault(input.Id);
            if (obj == null)
            {
                throw new UserFriendlyException("Item not Found!");
            }

            await this.repositoryBillTransitpass.DeleteAsync(obj);
        }

        public BillTransitPassDto GetBillTransitPass(int id)
        {
            var obj = this.repositoryBillTransitpass.FirstOrDefault(id);

            return obj.MapTo<BillTransitPassDto>();
        }

        public List<BillTransitPassDto> GetBillTransitPasses()
        {
            var values = this.repositoryBillTransitpass
            .GetAll()
            .OrderBy(p => p.CreationTime)
            .ToList();

            return new List<BillTransitPassDto>(values.MapTo<List<BillTransitPassDto>>());
        }

        public async Task UpdateBillTransitPass(BillTransitPassDto input)
        {
            var obj = this.repositoryBillTransitpass.FirstOrDefault(input.Id);
            obj.AdditionInformation = input.AdditionInformation;
            obj.BillId = input.BillId;
            obj.TransitPassId = input.TransitPassId;
            await this.repositoryBillTransitpass.UpdateAsync(obj);
        }
    }
}
