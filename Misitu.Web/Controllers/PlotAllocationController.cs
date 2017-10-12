using Abp.Runtime.Validation;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.FinancialYears;
using Misitu.Licensing;
using Misitu.Licensing.Dto;
using Misitu.PlotScalling;
using Misitu.Registration;
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

        public PlotAllocationController(
            IAllocatedPlotAppService allocatedPlotAppService,
            IPlotAppService plotAppService,
            IDealerAppService dealerAppService,
            IBillAppService billAppService,
            IUserAppService userAppService,
            IFinancialYearAppService financialYearAppService,
            IBillItemAppService billItemAppService,
            LicenseAppService licenseAppService
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
        }

        // GET: PlotAllocation
        public ActionResult Index(int id)
        {
            var dealer = _dealerAppService.GetDealer(id);
            var allocatedPlots = _allocatedPlotAppService.GetAllocatedPlotsByDealer(dealer);

            ViewBag.Dealer = dealer;
            return View(allocatedPlots);
        }

        // GET: PlotAllocation/Details/5
        public ActionResult Bill(int id)
        {
           try
            {
                // TODO: Add insert logic here

                var dealer = _dealerAppService.GetDealer(id);
                if(dealer != null)
                {
                    var items = _allocatedPlotAppService.GetAllocatedPlotsByDealer(dealer);
                    var user = _userAppService.GetLoggedInUser();
                    var Fyear = _financialYearAppService.GetActiveFinancialYear();

                    var billInput = new CreateBillInput
                    {
                        DealerId = dealer.Id,
                        StationId = user.StationId,
                        FinancialYearId = Fyear.Id,
                        IssuedDate = DateTime.Now
                    };

                    int bill = _billAppService.CreateBill(billInput);// Create and Save Bill
                    if (bill > 0)
                    {
                        //Get Bill Items
                        foreach (var bilIitem in items)
                        {
                            var item = new CreateBillItemInput
                            {
                                BillId = bill,
                                Description = bilIitem.Name,
                                Loyality = bilIitem.Loyality,
                                TFF = bilIitem.TFF,
                                LMDA = bilIitem.LMDA,
                                CESS = bilIitem.CESS,
                                VAT = bilIitem.VAT,
                                TP = bilIitem.TP,                                
                                Total = bilIitem.TOTAL
                            };

                            _billItemAppService.CreateBillItem(item);//Save Bill Items
                         
                        }

                        var License = new CreateLicenseInput
                        {
                            BillId = bill,
                            FinancialYearId = Fyear.Id,
                            StationId = user.StationId,
                            IssuedDate = DateTime.Now

                        };

                        _licenseAppService.CreateLicense(License);// Create Licence under pending status
                    }
                    return RedirectToAction("Tallied","Compartments");
                }

                return RedirectToAction("Index", new { id = id});
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
                //return RedirectToAction("Index", new { id = id });
            }
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
