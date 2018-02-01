using System;
using Abp.WebApi.Controllers;
using Misitu.RevenueSources;
using System.Net;
using System.Web.Http;

namespace Misitu.Api.Controllers
{
    public class RevenueSourcesController: AbpApiController
    {

        private readonly IRevenueSourceAppService _revenueSourceAppService;

        public RevenueSourcesController(IRevenueSourceAppService revenueSourceAppService)
        {
            _revenueSourceAppService = revenueSourceAppService;

        }
        [HttpGet()]
        [Route("/api/RevenueSources/getRevenueSources")]
        public IHttpActionResult getRevenueSources()
        {
            var sources = _revenueSourceAppService.GetRevenueResources();
            return Json(sources);
        }
    }
}
