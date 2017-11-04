using Abp.Runtime.Validation;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.FinancialYears;
using Misitu.Licensing;
using Misitu.Licensing.Dto;
using Misitu.PlotScalling;
using Misitu.Registration;
using Misitu.RevenueSources;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class PlotAllocationController : MisituControllerBase
    {
        private readonly IAllocatedPlotAppService _allocatedPlotAppService;
        private readonly IPlotAppService _plotAppService;
        private readonly IDealerAppService _dealerAppService;
        private readonly IBillAppService _billAppService;
        private readonly IUserAppService _userAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IBillItemAppService _billItemAppService;
        private readonly LicenseAppService _licenseAppService;
        private readonly IRevenueSourceAppService _revenueSourceAppService;

        public PlotAllocationController(
            IAllocatedPlotAppService allocatedPlotAppService,
            IPlotAppService plotAppService,
            IDealerAppService dealerAppService,
            IBillAppService billAppService,
            IUserAppService userAppService,
            IFinancialYearAppService financialYearAppService,
            IBillItemAppService billItemAppService,
            LicenseAppService licenseAppService,
             IRevenueSourceAppService revenueSourceAppService
            )
        {
            _allocatedPlotAppService = allocatedPlotAppService;
            _plotAppService = plotAppService;
            _dealerAppService = dealerAppService;
            _billAppService = billAppService;
            _userAppService = userAppService;
            _financialYearAppService = financialYearAppService;
            _billItemAppService = billItemAppService;
            _licenseAppService = licenseAppService;
            _revenueSourceAppService = revenueSourceAppService;
        }

        // GET: PlotAllocation
        public ActionResult Index(int id)
        {
            var dealer = _dealerAppService.GetDealer(id);
            var allocatedPlots = _allocatedPlotAppService.GetAllocatedPlotsByDealer(dealer);
            var sources = _revenueSourceAppService.GetRevenueResources().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Description });
            ViewBag.RevenueSourceId = sources;
            ViewBag.Dealer = dealer;
            ViewBag.Plots = allocatedPlots;
            return View();
        }


        [HttpPost]
        public ActionResult Bill(int id, CreateBillInput billInput, int RevenueSourceId)
        {
            var dealer = _dealerAppService.GetDealer(id);


            if (dealer != null)
            {
                var revenue = _revenueSourceAppService.GetRevenueResource(RevenueSourceId);
                var items = _allocatedPlotAppService.GetAllocatedPlotsByDealer(dealer);
                var user = _userAppService.GetLoggedInUser();
                var Fyear = _financialYearAppService.GetActiveFinancialYear();

                //Dealer Registration Bill

                int bill = _billAppService.CreateBill(billInput);
                if (bill > 0)
                {
                    foreach (var item in items)
                    {
                        var billItem = new CreateBillItemInput
                        {
                            BillId = bill,
                            RevenueResourceId = RevenueSourceId,
                            Description = revenue.Description,
                            Loyality = item.Loyality,
                            TFF = item.TFF,
                            LMDA = item.LMDA,
                            CESS = item.CESS,
                            VAT = item.VAT,
                            TP = item.TP,
                            Total = item.TOTAL
                        };

                        _billItemAppService.CreateBillItem(billItem);

                    }
                    var License = new CreateLicenseInput
                    {
                        BillId = bill,
                        FinancialYearId = Fyear.Id,
                        StationId = user.StationId,
                        IssuedDate = DateTime.Now

                    };

                  _licenseAppService.CreateLicense(License);// Create Licence under pending status

                    return RedirectToAction("ApplicationBill", "PlotAllocation", new { id = bill });
                }
             
        }

            return RedirectToAction("Index", new { id = id });
        }

        public ActionResult ApplicationBill(int id)
        {
            var bill = _billAppService.GetBill(id);

            return View(bill);
        }



        // GET: PlotAllocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlotAllocation/Create
        [HttpPost]
        public  ActionResult Create(int DealerId, int[] PlotId)
        {
            try
            {
                // TODO: Add insert logic here
                if(DealerId > 0 && PlotId.Length > 0)
                {
                    var plot = new CreateAllocatedPlotInput();

                    foreach(var Id in PlotId)
                    {
                        plot.DealerId = DealerId;
                        plot.PlotId = Id;

                        if(_allocatedPlotAppService.CreateAllocatedPlot(plot))
                        {
                            _plotAppService.UpdatePlotAllocation(_plotAppService.GetPlot(Id));
                        }

                    }

                    return RedirectToAction("Index",new { Id = DealerId});

                }
                else
                {
                    ModelState.AddModelError("", "Make sure Dealer and Plot are selected");

                    return RedirectToAction("Tallied","Compartments");
                }

              
            }
            catch
            {
                return View();
            }
        }

        // GET: PlotAllocation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlotAllocation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PlotAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlotAllocation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
