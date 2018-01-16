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
    
    public class InspectionAuditAppService : IInspectionAudit
    {
        private readonly IRepository<InspectionAudit> repositoryInspectionAudit;

        public InspectionAuditAppService(IRepository<InspectionAudit> repositoryInspectionAudit)
        {
            this.repositoryInspectionAudit = repositoryInspectionAudit;
        }

        public int CreateInspectionAudit(CreateInspectionAudit input)
        {
            var obj = new InspectionAudit
            {
                CheckPointTransitPassId = input.CheckPointTransitPassId,
                Action = input.Action,
                AdditionInformation = input.AdditionInformation

            };
          
                return this.repositoryInspectionAudit.InsertAndGetId(obj);
           
        }

        public async Task DeleteInspectionAuditAsync(InspectionAuditDto input)
        {
            var obj = this.repositoryInspectionAudit.FirstOrDefault(input.Id);
            if (obj == null)
            {
                throw new UserFriendlyException("Item not Found!");
            }

            await this.repositoryInspectionAudit.DeleteAsync(obj);
        }

        public InspectionAuditDto GetInspectionAudit(int id)
        {
            var obj = this.repositoryInspectionAudit.FirstOrDefault(id);

            return obj.MapTo<InspectionAuditDto>();
        }

        public List<InspectionAuditDto> GetInspectionAudites()
        {
            var values = this.repositoryInspectionAudit
           .GetAll()
           .OrderBy(p => p.CreationTime)
           .ToList();

            return new List<InspectionAuditDto>(values.MapTo<List<InspectionAuditDto>>());
        }

        public async Task UpdateInspectionAudit(InspectionAuditDto input)
        {
            var obj = this.repositoryInspectionAudit.FirstOrDefault(input.Id);
            obj.AdditionInformation = input.AdditionInformation;
            obj.CheckPointTransitPassId = input.CheckPointTransitPassId;
            obj.Action = input.Action;
            await this.repositoryInspectionAudit.UpdateAsync(obj);
        }
    }
}
