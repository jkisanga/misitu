using Abp.Runtime.Validation;
using Misitu.FinancialYears;
using Misitu.PlotScalling;
using Misitu.PlotScalling.Dto;
using Misitu.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.PlotScalling
{
    [DisableValidation]
    public class PlotsController : Controller
    {

        private readonly IPlotAppService _plotAppService;
        private readonly ICompartmentAppService _compartmentAppService;
        private readonly IDealerAppService _dealerAppService;
        private readonly IFinancialYearAppService _financialYearAppService;

        public PlotsController(IPlotAppService plotAppService, 
            ICompartmentAppService compartmentAppService,
            IDealerAppService dealerAppService,
             IFinancialYearAppService financialYearAppService
            )
        {
            _plotAppService = plotAppService;
            _compartmentAppService = compartmentAppService;
            _dealerAppService = dealerAppService;
            _financialYearAppService = financialYearAppService;
        }

        // GET: Plots
        public ActionResult Index(int id)
        {
            var compartment = _compartmentAppService.GetCompartment(id);
            var plots = _plotAppService.GetPlotsByCompartment(id);
            ViewBag.Compartment = compartment;
            return View(plots);
        }

        // GET: Plots
        public ActionResult Tallied(int id)
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var dealers = _dealerAppService.GetRegisteredDealers(finacialYear).Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Applicant.Name });
            var compartment = _compartmentAppService.GetCompartment(id);
            var plots = _plotAppService.GetTalliedPlotsByCompartment(id);

            ViewBag.Compartment = compartment;
            ViewBag.DealerId = dealers;
            return View(plots);
        }

      
        // GET: Plots/Create
        public ActionResult Create(int id)
        {
            var compartment = _compartmentAppService.GetCompartment(id);
            ViewBag.Compartment = compartment;
            return View();
        }

        // POST: Plots/Create
        [HttpPost]
        public  ActionResult Create(int id, CreatePlotInput input)
        {
            if (ModelState.IsValid)
            {
                 _plotAppService.CreatePlot(input);
                return RedirectToAction("Index", new {Id = id });
            }
            else
            {
                var compartment = _compartmentAppService.GetCompartment(id);
                ViewBag.Compartment = compartment;
                return View(input);
            }
        }

        // GET: Plots/Edit/5
        public ActionResult Edit(int id)
        {
            var plot = _plotAppService.GetPlot(id);
            return View(plot);
        }

        // POST: Plots/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PlotDto input)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                await _plotAppService.UpdatePlot(input);
                return RedirectToAction("Index", new { id = input.CompartmentId});
            }
            else
            {
                return View(input);
            }
        }

        // GET: Plots/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var plot = _plotAppService.GetPlot(id);
            await _plotAppService.DeletePlotAsync(plot);
            return RedirectToAction("Index", new { id = plot.CompartmentId});
        }

      
    }
}
