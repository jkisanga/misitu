using Misitu.RevenueSources;
using Misitu.RevenueSources.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class RevenueSourcesController : Controller
    {

        private readonly IRevenueSourceAppService _revenueSourceAppService;
        private readonly IRefSubRevenueSourceAppService _refSubRevenueSourceAppService;

        public RevenueSourcesController(IRevenueSourceAppService revenueSourceAppService, IRefSubRevenueSourceAppService refSubRevenueSourceAppService)
        {
            _revenueSourceAppService = revenueSourceAppService;
            _refSubRevenueSourceAppService = refSubRevenueSourceAppService;
        }
        // GET: RevenueSources
        public ActionResult Index()
        {
            var sources = _revenueSourceAppService.GetRevenueResources();
            return View(sources);
        }
    

        // GET: RevenueSources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RevenueSources/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateRevenueSourcesInput input)
        {
            if (ModelState.IsValid)
            {
                await _revenueSourceAppService.CreateRevenueResource(input);
                return RedirectToAction("Index");

            }
            else
            {
                return View(input);
            }
        }

        // GET: RevenueSources/Edit/5
        public ActionResult Edit(int id)
        {
            var resource = _revenueSourceAppService.GetRevenueResource(id);
            return View(resource);
        }

        // POST: RevenueSources/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RevenueSourcesDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    await _revenueSourceAppService.UpdateRevenueResource(input);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(input);
                }

            }
            catch
            {
                return View(input);
            }
        }

        // GET: RevenueSources/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var resource = _revenueSourceAppService.GetRevenueResource(id);
            await _revenueSourceAppService.DeleteRevenueResourceAsync(resource);

            return RedirectToAction("Index");
        }


        public ActionResult SubRevenue(int Id)
        {
            var resource = _refSubRevenueSourceAppService.GetRefSubRevenueResources(Id);

            return View(resource);
        }



    }
}
