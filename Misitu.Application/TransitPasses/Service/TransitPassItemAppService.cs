using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Misitu.TransitPasses.Dto;
using Misitu.TransitPasses.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misitu.TransitPasses.Service
{
   public class TransitPassItemAppService: MisituAppServiceBase, ITransitPassItemAppService
    {
        private readonly IRepository<TransitPassItem> _transitPassItemRepository;

        public TransitPassItemAppService(IRepository<TransitPassItem> transitPassItemRepository)
        {
            _transitPassItemRepository = transitPassItemRepository;
        }

        // Region list
        public List<TransitPassItemDto> GetTransitPassItems()
        {
            var transitPassItem = _transitPassItemRepository.GetAll().ToList();

            return new List<TransitPassItemDto>(transitPassItem.MapTo<List<TransitPassItemDto>>());
        }

        //create new Region
        public void CreateTransitPassItem(CreateTransitPassItem input)
        {         
            var IsExist = _transitPassItemRepository.FirstOrDefault(p => p.TransitPassId == input.TransitPassId && p.ActivityId == input.ActivityId);
            if (IsExist == null)
            {
                var transitPassItem = new TransitPassItem
                {
                    TransitPassId = input.TransitPassId,
                    ActivityId = input.ActivityId,
                    UnitMeasureId = input.UnitMeasureId,
                    SpecieId = input.SpecieId,
                    Quantity = input.Quantity
                };
                _transitPassItemRepository.Insert(transitPassItem);
            }
            else
            {
                throw new UserFriendlyException("Item already exists");
            }

        }

        //get edit Region
        public TransitPassItemDto GetTransitPassItem(int id)
        {
            var transitPassItem = _transitPassItemRepository.FirstOrDefault(id);

            return transitPassItem.MapTo<TransitPassItemDto>();

        }

        //update Region
        public void UpdateTransitPassItem(TransitPassItemDto input)
        {
            // here aoutomapping can be done;
            var transitPassItem = _transitPassItemRepository.FirstOrDefault(input.Id);
                transitPassItem.TransitPassId = input.TransitPassId;
                transitPassItem.ActivityId = input.ActivityId;
                transitPassItem.UnitMeasureId = input.UnitMeasureId;
                transitPassItem.SpecieId = input.SpecieId;
                transitPassItem.Quantity = input.Quantity;

            _transitPassItemRepository.Update(transitPassItem);
        }


        //delete Region
        public async Task DeleteTransitPassItem(TransitPassItemDto input)
        {
            var transitPassItem = _transitPassItemRepository.FirstOrDefault(input.Id);
            if (transitPassItem == null)
            {
                throw new UserFriendlyException("transitPassItem not Found!");
            }

            await _transitPassItemRepository.DeleteAsync(transitPassItem);

        }
    }
}
