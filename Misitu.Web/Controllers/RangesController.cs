using Abp.Runtime.Validation;
using Misitu.Divisions;
using Misitu.Ranges;
using Misitu.Ranges.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class RangesController : MisituControllerBase
    {
        private readonly IDivisionAppService _divisinAppService;
        private readonly IRangeAppService _rangeAppService;

        public RangesController(IDivisionAppService divisionAppSerice, IRangeAppService rangeAppSerice)
        {
            _divisinAppService = divisionAppSerice;
            _rangeAppService = rangeAppSerice;

        }
        // GET: Ranges
        public ActionResult Index()
        {

            var ranges = _rangeAppService.GetRanges();
            return View(ranges);
        }
      

        // GET: Ranges/Create
        public ActionResult Create()
        {
            var divisions = _divisinAppService.GetDivisions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.DivisionId = divisions;
            return View();
        }

        // POST: Ranges/Create
        [HttpPost]
        public async Task <ActionResult> Create(CreateRangeInput input)
        {
            if (ModelState.IsValid)
            {
                await _rangeAppService.CreateRange(input);
                return RedirectToAction("Index");
            }
            else
            {
                var divisions = _divisinAppService.GetDivisions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.DivisionId = divisions;
                return View(input);
            }
        }

        // GET: Ranges/Edit/5
        public ActionResult Edit(int id)
        {
            var range = _rangeAppService.GetRange(id);
            var divisions = _divisinAppService.GetDivisions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.DivisionId = divisions;
            return View(range);
        }

        // POST: Ranges/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RangeDto input)
        {
            if(ModelState.IsValid)
            {
                // TODO: Add update logic here
                await _rangeAppService.UpdateRange(input);
                return RedirectToAction("Index");
            }
            else
            {
                var divisions = _divisinAppService.GetDivisions().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.DivisionId = divisions;
                return View(input);
            }
        }

        // GET: Ranges/Delete/5
        public async Task< ActionResult> Delete(int id)
        {
            var range = _rangeAppService.GetRange(id);
            await _rangeAppService.DeleteRangeAsync(range);
            return RedirectToAction("Index");
        }
       
    }
}
