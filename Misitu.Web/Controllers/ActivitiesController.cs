using Abp.Runtime.Validation;
using Misitu.Activities;
using Misitu.Activities.Dto;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
   
    public class ActivitiesController : Controller
    {
        private readonly IActivityAppService _activityAppService;
        private readonly IRevenueSourceAppService _revenueSourceAppService;


        public ActivitiesController(IActivityAppService activityAppService, IRevenueSourceAppService revenueSourceAppService)
        {
            _activityAppService = activityAppService;
            _revenueSourceAppService = revenueSourceAppService;
        }

        //List of Actitvities
        public ActionResult Index()
        {
            var activities = _activityAppService.GetActivities();
            return View(activities);
        }

        //get activities By revenue source Id
        public ActionResult getActivities(int Id)
        {
            var activities = _activityAppService.GetActivitiesByRevenueSourceId(Id).Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }); ;
            return Json(activities,JsonRequestBehavior.AllowGet);
        }

        // GET: Zone/Create
        public ActionResult Create()
        {
            ViewBag.RevenueSourceId = _revenueSourceAppService.GetRevenueResources().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
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
                ViewBag.RevenueSourceId = _revenueSourceAppService.GetRevenueResources().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
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
