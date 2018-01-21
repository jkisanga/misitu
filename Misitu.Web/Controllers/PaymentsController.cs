using Abp.UI;
using Misitu.Billing;
using Misitu.Billing.Dto;
using Misitu.FinancialYears;
using Misitu.Layout.Dto;
using Misitu.Registration;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    public class PaymentsController : Controller
    {

        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IBillAppService _billAppService;
        private readonly IDealerAppService _dealerAppService;
        private readonly IStationAppService _stationAppService;
        private readonly IUserAppService _userAppService;

        public PaymentsController(
          IBillAppService billAppService,
           IDealerAppService dealerAppService,
           FinancialYearAppService financialYEarAppService,
            IUserAppService userAppService,
             IStationAppService stationAppService

          )
        {
            _billAppService = billAppService;
            _dealerAppService = dealerAppService;
            _financialYearAppService = financialYEarAppService;
            _userAppService = userAppService;
            _stationAppService = stationAppService;

        }
        // GET: Payments
        public ActionResult Dashboard()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var userInfo = _userAppService.GetLoggedInUser();

            var Dashboard = new PaymentsDashboard
            {              
                TotalCollection = _billAppService.GetTotalPaymentsAmountByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear).Sum(),
                CollectionPerMonth = _billAppService.GetTotalMonthPaymentsAmountByStation(_stationAppService.GetStation(userInfo.StationId), finacialYear).Sum()
            };

            return View(Dashboard);
        }

        public ActionResult Index()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var bills = _billAppService.GetBills(finacialYear);
            return View(bills);
        }


        // GET: paid/ Bills
        public ActionResult Paid()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var bills = _billAppService.GetPayedBills(finacialYear);
            return View(bills);
        }
        // GET: Bills/confirm/5
        public ActionResult Confirm(int id)
        {
            var bill = _billAppService.GetBill(id);
            return View(bill);
        }

        // Post: Bills/confirm/5
        [HttpPost]
        public ActionResult Confirm(int id, BillDto input, double Amount)
        {
            var bill = _billAppService.GetBill(id);
            var dealer = _dealerAppService.GetDealer(input.ApplicantId);// change

            if (bill != null)
            {
                _billAppService.ConfirmBill(bill, Amount);

            }
            else
            {
                throw new UserFriendlyException("Enter the paid amount");
            }
            return RedirectToAction("Index");
        }

    }
}
