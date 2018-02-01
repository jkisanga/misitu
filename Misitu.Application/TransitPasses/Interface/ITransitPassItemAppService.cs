using Abp.Application.Services;
using Misitu.TransitPasses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Interface
{
    public interface  ITransitPassItemAppService:IApplicationService
    {
        List<TransitPassItemDto> GetTransitPassItems();

        void CreateTransitPassItem(CreateTransitPassItem input);

        TransitPassItemDto GetTransitPassItem(int id);

        void UpdateTransitPassItem(TransitPassItemDto input);

        Task DeleteTransitPassItem(TransitPassItemDto input);
    }
}
