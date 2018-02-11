using Abp.Runtime.Validation;
using Excel;
using Misitu.FinancialYears;
using Misitu.Registration;
using Misitu.Stations;
using Misitu.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.Registration
{
    [DisableValidation]
    public class CandidatesController : Controller
    {

        private readonly ICandidateAppService _candidateAppService;
        private readonly IFinancialYearAppService _financialYearAppService;
        private readonly IStationAppService _stationAppService;
        private readonly IUserAppService _userAppService;

        public CandidatesController(
            ICandidateAppService candidateAppService, 
            FinancialYearAppService financialYEarAppService,
             IStationAppService stationAppService,
             IUserAppService userAppService
            )
        {
            _candidateAppService = candidateAppService;
            _financialYearAppService = financialYEarAppService;
            _stationAppService = stationAppService;
            _userAppService = userAppService;
        }

        // GET: Candidates
        public ActionResult Index()
        {
            var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var candidates = _candidateAppService.GetCandidates(finacialYear,_stationAppService.GetStation(_userAppService.GetLoggedInUser().StationId));
            return View(candidates);
        }

        // GET: Candidates/Register/5
        public ActionResult Register(int id)
        {

            ViewBag.Candidate = _candidateAppService.GetCandidate(id);
            return View();
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    //ExcelDataReader works on binary excel file
                    Stream stream = file.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (file.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        //Shows error if uploaded file is not Excel file
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result = reader.AsDataSet();
                    reader.Close();
                    //Sending result data to database

                    var finacialYear = _financialYearAppService.GetActiveFinancialYear();
                    var station = _stationAppService.GetStation(_userAppService.GetLoggedInUser().StationId);// get logged user station

                    _candidateAppService.UploadCandidates(result.Tables[0],finacialYear,station);
                    //return View();
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }

            return RedirectToAction("Create");
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Candidates/Edit/5
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

        // GET: Candidates/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Candidates/Delete/5
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
