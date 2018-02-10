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

        // Transit Pass Item list
        public List<TransitPassItemDto> GetTransitPassItems()
        {
            var transitPassItem = _transitPassItemRepository.GetAll().ToList();

            return new List<TransitPassItemDto>(transitPassItem.MapTo<List<TransitPassItemDto>>());
        }

        //get transit pass items by transit pass
       public List<CustomTransitPassItemDto> GetItemsByTransitPassId(int id)
        {
            var transitPassItems = (from item in _transitPassItemRepository.GetAll()
                                   where item.TransitPassId == id
                                   select new CustomTransitPassItemDto
                                   {
                                       Id = item.Id,
                                       TransitPassId = item.TransitPassId,
                                       Item = item.Activity.Name,
                                       UnitMeasure = item.UnitMeasure.Name,
                                       Specie = item.Specie.CommonName,
                                       Quantity = item.Quantity,
                                       Size = item.Size
                                   }).ToList();

            return transitPassItems;
        }

        //create new Transit Pass Item
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
                    Quantity = input.Quantity,
                    Size = input.Size
                };
                _transitPassItemRepository.Insert(transitPassItem);
            }
            else
            {
                throw new UserFriendlyException("Item already exists");
            }

        }

        //get edit Transit Pass Item
        public TransitPassItemDto GetTransitPassItem(int id)
        {
            var transitPassItem = _transitPassItemRepository.FirstOrDefault(id);

            return transitPassItem.MapTo<TransitPassItemDto>();

        }

        //update Transit Pass Item
        public void UpdateTransitPassItem(TransitPassItemDto input)
        {
            // here aoutomapping can be done;
            var transitPassItem = _transitPassItemRepository.FirstOrDefault(input.Id);
                transitPassItem.TransitPassId = input.TransitPassId;
                transitPassItem.ActivityId = input.ActivityId;
                transitPassItem.UnitMeasureId = input.UnitMeasureId;
                transitPassItem.SpecieId = input.SpecieId;
                transitPassItem.Quantity = input.Quantity;
                transitPassItem.Size = input.Size;

            _transitPassItemRepository.Update(transitPassItem);
        }


        //delete Transit Pass Item
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
