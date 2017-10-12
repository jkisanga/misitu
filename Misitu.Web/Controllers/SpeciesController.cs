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
    public class SpeciesController : MisituControllerBase
    {

        private readonly ISpecieAppService _specieAppService;
        private readonly ISpecieCategoryAppService _specieCategoryAppService;

        public SpeciesController(ISpecieCategoryAppService specieCategoryAppService, ISpecieAppService specieAppService)
        {
            _specieCategoryAppService = specieCategoryAppService;
            _specieAppService = specieAppService;
        }

        // GET: Specie
        public ActionResult Index()
        {
            var species = _specieAppService.GetSpecies();
            return View(species);
        }

      

        // GET: Specie/Create
        public ActionResult Create()
        {
            var categories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.SpecieCategoryId = categories;
            return View();
        }

        // POST: Specie/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateSpecieInput input)
        {
            if (ModelState.IsValid)
            {
                await _specieAppService.CreateSpecie(input);
                return RedirectToAction("Index");

            }
            else
            {
                var categories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.SpecieCategoryId = categories;
                return View(input);
            }
        }

        // GET: Specie/Edit/5
        public ActionResult Edit(int id)
        {
            var categories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.SpecieCategoryId = categories;
            return View();
        }

        // POST: Specie/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, SpecieDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    await _specieAppService.UpdateSpecie(input);
                    return RedirectToAction("Index");

                }
                else
                {
                    var categories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                    ViewBag.SpecieCategoryId = categories;
                    return View(input);
                }

            }
            catch
            {
                return View(input);
            }
        }

        // GET: Specie/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var specie = _specieAppService.GetSpecie(id);
            await _specieAppService.DeleteSpecieAsync(specie);
            return RedirectToAction("Index");
         
        }

     
    }
}
