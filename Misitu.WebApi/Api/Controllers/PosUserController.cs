using Abp.WebApi.Controllers;
using Misitu.POSUser;
using Misitu.POSUser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Misitu.Api.Controllers
{
   public class PosUserController : AbpApiController
    {
        private readonly ICheckpointUserAppService checkpoitUserAppService;

        public PosUserController(ICheckpointUserAppService checkpoitUserAppService)
        {
            this.checkpoitUserAppService = checkpoitUserAppService;
        }

        [HttpGet()]
        public IHttpActionResult posUserList()
        {
            var users = this.checkpoitUserAppService.GetCheckpoitUsers();
            return Json(users);
        }

        [HttpPost]
        [Route("/api/PosUser/Login")]
        public IHttpActionResult Login(CheckpointUserDto input)
        {
            var cu = this.checkpoitUserAppService.PosUserLogin(input);
            return Json(cu);
        }
    }
}
