using Abp.Runtime.Validation;
using Misitu.Activities;
using Misitu.Activities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class ActivitiesController : MisituControllerBase
    {
        private readonly IActivityAppService _activityAppService;

        public ActivitiesController(IActivityAppService activityAppService)
        {
            _activityAppService = activityAppService;
        }

        public ActionResult Index()
        {
            var activities = _activityAppService.GetActivities();
            return View(activities);
        }

        // GET: Zone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Zone/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateActivityInput input)
        {

            // TODO: Add insert logic here

            if (ModelState.IsValid)
            {
                await _activityAppService.CreateActivity(input);
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
            var activity = _activityAppService.GetActivity(id);
            return View(activity);
        }

        // POST: Zone/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ActivityDto input)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    await _activityAppService.UpdateActivity(input);
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
            var activity = _activityAppService.GetActivity(id);
            await _activityAppService.DeleteActivityAsync(activity);

            return RedirectToAction("Index");
        }
    }
}
