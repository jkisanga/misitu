using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.WebApi.Controllers;
using Misitu.TransitPasses;
using Misitu.TransitPasses.Interface;

namespace Misitu.Api.Controllers
{
   public class TransitPassController: AbpApiController
    {
        private readonly ITransitPass _transitPassAppService;
        private readonly ICheckPointTransitPass _checkPointTransitPassAppService;
        private readonly ITransitPassItemAppService _transitPassItemsAppService;

         public TransitPassController(
             ITransitPass transitPassAppService,
             ICheckPointTransitPass checkPointTransitPassAppService,
             ITransitPassItemAppService transitPassItemsAppService
             ){
            _transitPassAppService = transitPassAppService;
             _checkPointTransitPassAppService = checkPointTransitPassAppService;
            _transitPassItemsAppService = transitPassItemsAppService;
         }
        //get all issued transit passes
        [HttpGet()]
        public IHttpActionResult getIssuedTransitPasses()
        {
            var transitPasses = _transitPassAppService.GetPaidTransitPasses();
            return Json(transitPasses);
        }

        //get all expired transit passes
        [HttpGet()]
        public IHttpActionResult getExpiredTransitPasses()
        {
            var transitPasses = _transitPassAppService.GetExpiredTransitPasses();
            return Json(transitPasses);
        }

        [HttpGet()]
        public IHttpActionResult getTransitPassById(int id) {
            var transitPass = _transitPassAppService.GetTransitPassById(id);
            return Json(transitPass);
        }

        [HttpGet()]
        public IHttpActionResult getCheckPointsByTransitPassId(int id){
            var checkPoints =  _checkPointTransitPassAppService.GetCheckPointsByTransitPassId(id);
            return Json(checkPoints);
         }

        [HttpGet()]
        public IHttpActionResult getItemsByTransitPassId(int id)
        {
            var items = _transitPassItemsAppService.GetItemsByTransitPassId(id);
            return Json(items);
        }

    }
}
