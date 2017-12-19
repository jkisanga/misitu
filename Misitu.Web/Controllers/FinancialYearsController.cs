using Misitu.FinancialYears;
using Misitu.FinancialYears.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class FinancialYearsController : MisituControllerBase
    {
        private readonly IFinancialYearAppService _financialYearAppService;

        public FinancialYearsController(FinancialYearAppService financialYEarAppService)
        {
            _financialYearAppService = financialYEarAppService;
        }

        public ActionResult Index()
        {
            var years = _financialYearAppService.GetFinancialYears();
            return View(years);
        }

        // GET: Zone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zone/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateFinancialYear input)
        {

            // TODO: Add insert logic here

            if (ModelState.IsValid)
            {
                await _financialYearAppService.CreateFinancialYear(input);
                return RedirectToAction("Index");

            }
            else
            {
                return View(input);
            }

        }

        // GET: FinancialYears/Edit/5
        public ActionResult Edit(int id)
        {
            var year = _financialYearAppService.GetFinancialYear(id);
            return View(year);
  
        }

        // POST: FinancialYears/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FinancialYearDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    await _financialYearAppService.UpdateFinancialYear(input);
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

        // GET: FinancialYears/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var year = _financialYearAppService.GetFinancialYear(id);
            await _financialYearAppService.DeleteFinancialYearAsync(year);

            return RedirectToAction("Index");
        }

        // GET: FinancialYears/Delete/5
        public async Task<ActionResult> Activate(int id)
        {
            var year = _financialYearAppService.GetFinancialYear(id);
            await _financialYearAppService.ActivateFinancialYearAsync(year);
            return RedirectToAction("Index");
        }

    }
}
