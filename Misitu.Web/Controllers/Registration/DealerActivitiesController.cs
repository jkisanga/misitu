using Abp.Runtime.Validation;
using Misitu.Activities;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.Registration;
using Misitu.Registration.Dto;
using Misitu.RevenueSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.Registration
{
    [DisableValidation]
    public class DealerActivitiesController : Controller
    {

        private readonly IDealerAppService _dealerAppService;
        private readonly IDealerActivityAppService _dealerActivityAppService;
        private readonly IActivityAppService _activityAppService;
        private readonly IBillAppService _billAppService;
        private readonly IBillItemAppService _billItemAppService;
        private readonly IRevenueSourceAppService _revenueSourceAppService;


        public DealerActivitiesController(
            IBillAppService billAppService,
            IBillItemAppService billItemAppService,
            IDealerAppService dealerAppService, 
            IDealerActivityAppService dealerActivityAppService,
            IActivityAppService activityAppService,
              IRevenueSourceAppService revenueSourceAppService
            )
        {
            _dealerAppService = dealerAppService;
            _dealerActivityAppService = dealerActivityAppService;
            _activityAppService = activityAppService;
            _billAppService = billAppService;
            _billItemAppService = billItemAppService;
            _revenueSourceAppService = revenueSourceAppService;
        }
        // GET: DealerActivities
        public ActionResult List(int id)
        {
            var dealer = _dealerAppService.GetDealer(id);
            var activities = _dealerActivityAppService.GetDealerActivities(dealer);

            ViewBag.Dealer = dealer;
            return View(activities);
        }

        // GET: DealerActivities Partialn view
        [HttpPost]
        public ActionResult Bill(int id, CreateBillInput billInput, int RevenueSourceId)
        {
            var dealer = _dealerAppService.GetDealer(id);


            if (dealer != null)
            {
                var activities = _dealerActivityAppService.GetDealerActivities(dealer);
                var revenue = _revenueSourceAppService.GetRevenueResource(RevenueSourceId);

                //Dealer Registration Bill

                int bill = _billAppService.CreateBill(billInput);
                if (bill > 0)
                {
          
                    foreach (var activity in activities)
                    {
                        var item = new CreateBillItemInput
                        {
                            BillId = bill,
                            RevenueResourceId = RevenueSourceId,
                            Description = revenue.Description,
                            Loyality = activity.Activity.Fee + activity.Activity.RegistrationFee,
                            Total = activity.Activity.Fee + activity.Activity.RegistrationFee
                        };

                         _billItemAppService.CreateBillItem(item);
                        _dealerAppService.UpdateBillControlNumber(dealer, _billAppService.GetBill(bill).ControlNumber);

                    }
                }
                return RedirectToAction("ApplicationBill", "DealerActivities",new { id = bill});
            }

            return RedirectToAction("Create", new { id = dealer.Id });
        }

        public ActionResult ApplicationBill(int id)
        {
            var bill = _billAppService.GetBill(id);

            return View(bill);
        }



        // GET: DealerActivities/Create
        public ActionResult Create(int id)
        {
            var dealer = _dealerAppService.GetDealer(id);
            var activities = _activityAppService.GetActivities();
            var DealerActivities = _dealerActivityAppService.GetDealerActivities(dealer);
            var sources = _revenueSourceAppService.GetRevenueResources().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
            ViewBag.RevenueSourceId = sources;
            ViewBag.Dealer = dealer;
            ViewBag.Activities = activities;
            ViewBag.DealerActivities = DealerActivities;
            return View();
        }

        // POST: DealerActivities/Create
        [HttpPost]
        public async Task< ActionResult> Create(CreateDealerActivityInput input, int[] ActivityId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach( var activityId in ActivityId){
                        
                        input.ActivityId = activityId;

                        await _dealerActivityAppService.CreateDealerActivity(input);

                    }
                    return RedirectToAction("Create", new { id = input.DealerId });
                }
                else
                {
                    return View("Create", new { id = input.DealerId});
                }

              
            }
            catch
            {
                return View();
            }
        }

    

        // GET: DealerActivities/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var activity = _dealerActivityAppService.GetDealerActivity(id);
            var dealerId = activity.DealerId;

            await _dealerActivityAppService.DeleteDealerActivityAsync(activity);

            return RedirectToAction("Create", new { id = dealerId });
        }

       
    }
}
