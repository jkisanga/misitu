using Microsoft.Reporting.WebForms;
using Misitu.FinancialYears;
using Misitu.Licensing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
  
    public class LicenseController : MisituControllerBase
    {

        private readonly LicenseAppService _licenseAppService;
        private readonly IFinancialYearAppService _financialYearAppService;

        public LicenseController(
                  IFinancialYearAppService financialYearAppService,
                  LicenseAppService licenseAppService
                  )
        {

            _financialYearAppService = financialYearAppService;
            _licenseAppService = licenseAppService;
        }

        // GET:  License/Dashboard

        public ActionResult Dashboard()
        {
            return View();
        }
        // GET: License
        public ActionResult Index()
        {
            var Fyear = _financialYearAppService.GetActiveFinancialYear();
            var Licenses = _licenseAppService.GetLicenses(Fyear);
            return View(Licenses);
        }

        // GET: License
        public ActionResult Pending()
        {
            var Fyear = _financialYearAppService.GetActiveFinancialYear();
            var Licenses = _licenseAppService.GetPendingLicenses(Fyear);
            return View(Licenses);
        }

        //View bill in report viewer

        public ActionResult LicenceViewer(int id)
        {
            var licence = _licenseAppService.GetLicense(id);
            return View(licence);

        }

        public ActionResult LicenceReportViewer(int id)
        {
            
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\rptHarvestLicence.rdlc";

            ReportParameter licenceId = new ReportParameter("LicenceId", id.ToString());
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { licenceId });
            reportViewer.LocalReport.DataSources.Clear();

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("dsHarvestingLicence", _licenseAppService.PrintLicence(id)));

            reportViewer.LocalReport.Refresh();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.Width = 1200;
            reportViewer.Height = 500;
            reportViewer.ShowPrintButton = true;
            reportViewer.ZoomMode = ZoomMode.FullPage;

            ViewBag.rptLicence = reportViewer;
            ViewBag.LicenceId = id;

            return PartialView("_LicenceReportViewer");
        }
    }
}
