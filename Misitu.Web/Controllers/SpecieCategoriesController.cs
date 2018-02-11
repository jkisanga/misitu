using Abp.Runtime.Validation;
using Misitu.Species;
using Misitu.Species.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class SpecieCategoriesController : MisituControllerBase
    {

        private readonly ISpecieCategoryAppService _specieCategoryAppService;

        public SpecieCategoriesController(ISpecieCategoryAppService specieCategoryAppService)
        {
            _specieCategoryAppService = specieCategoryAppService;
        }

        public ActionResult Index()
        {
            var categories = _specieCategoryAppService.GetSpecieCategories();
            return View(categories);
        }

        // GET: Zone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zone/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateSpecieCategoryInput input)
        {

            // TODO: Add insert logic here

            if (ModelState.IsValid)
            {
                await _specieCategoryAppService.CreateSpecieCategory(input);
                return RedirectToAction("Index");

            }
            else
            {
                return View(input);
            }

        }


        // GET: Zone/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _specieCategoryAppService.GetSpecieCategory(id);
            return View(category);
        }

        // POST: Zone/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, SpecieCategoryDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    await _specieCategoryAppService.UpdateSpecieCategory(input);
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

        // GET: Zone/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var category = _specieCategoryAppService.GetSpecieCategory(id);
            await _specieCategoryAppService.DeleteSpecieCategoryAsync(category);
            return RedirectToAction("Index");
        }
    }
}
