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
    public class CheckPointTransitPassAppService : ICheckPointTransitPass
    {
        private readonly IRepository<CheckPointTransitPass> repositoryCheckpointTransitpass;
        private readonly IRepository<TransitPass> repositoryTransitpass;

        public CheckPointTransitPassAppService(IRepository<CheckPointTransitPass> repositoryCheckpointTransitpass, IRepository<TransitPass> repositoryTransitpass)
        {
            this.repositoryCheckpointTransitpass = repositoryCheckpointTransitpass;
            this.repositoryTransitpass = repositoryTransitpass;
        }

        public int CreateCheckPointTransitPass(CreateCheckPointTransitPass input)
        {
            var obj = new CheckPointTransitPass
            {
                TransitPassId = input.TransitPassId,
                StationId = input.StationId
                //InspectionStatus = input.InspectionStatus,
                //InspectorId = input.InspectorId,
                //AdditionInformation = input.AdditionInformation

            };
            var objExist = this.repositoryCheckpointTransitpass.FirstOrDefault(a => a.StationId == input.StationId && a.TransitPassId == input.TransitPassId);
            if (objExist == null)
            {
                return this.repositoryCheckpointTransitpass.InsertAndGetId(obj);
            }
            else
            {
                throw new UserFriendlyException("Item Alredy Inspected");
            }
        }

        public async Task DeleteCheckPointTransitPassAsync(CheckPointTransitPassDto input)
        {
            var obj = this.repositoryCheckpointTransitpass.FirstOrDefault(input.Id);
            if (obj == null)
            {
                throw new UserFriendlyException("Item not Found!");
            }

            await this.repositoryCheckpointTransitpass.DeleteAsync(obj);
        }

        public CheckPointTransitPassDto GetCheckPointTransitPass(int id)
        {
            var obj = this.repositoryCheckpointTransitpass.FirstOrDefault(id);

            return obj.MapTo<CheckPointTransitPassDto>();
        }

        public List<CheckPointTransitPassDto> GetCheckPointTransitPasses()
        {
            var values = this.repositoryCheckpointTransitpass
            .GetAll()
            .OrderBy(p => p.CreationTime)
            .ToList();

            return new List<CheckPointTransitPassDto>(values.MapTo<List<CheckPointTransitPassDto>>());
        }

        public async Task UpdateCheckPointTransitPass(CheckPointTransitPassDto input)
        {
            var obj = this.repositoryCheckpointTransitpass.FirstOrDefault(input.Id);
            obj.AdditionInformation = input.AdditionInformation;
            obj.InspectionStatus = input.InspectionStatus;
            obj.InspectorId = input.InspectorId;
            await this.repositoryCheckpointTransitpass.UpdateAsync(obj);
        }
    }
}
