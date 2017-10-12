using Abp.Runtime.Validation;
using Excel;
using Misitu.PlotScalling;
using Misitu.PlotScalling.Dto;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.PlotScalling
{
    [DisableValidation]
    public class TallySheetsController : Controller
    {
        private readonly ITallySheetAppService _tallySheetAppService;
        private readonly IPlotAppService _plotAppService;
        private readonly ISpecieCategoryAppService _specieCategoryAppService;

        public TallySheetsController(
            ITallySheetAppService tallySheetAppService, 
            IPlotAppService plotAppService,
            ISpecieCategoryAppService specieCategoryAppService
            )
        {
            _tallySheetAppService = tallySheetAppService;
            _plotAppService = plotAppService;
            _specieCategoryAppService = specieCategoryAppService;
        }

      
        // GET: TallySheets/Create
        public ActionResult Create(int id)
        {
            var plot = _plotAppService.GetPlot(id);
            var sheets = _tallySheetAppService.GetTallySheets(plot);
            var specieCategories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.SpecieCategoryId = specieCategories;
            ViewBag.Plot = plot;
            ViewBag.Sheets = sheets;
            return View();
        }

        // POST: TallySheets/Create
        [HttpPost]
        public ActionResult Create(CreateTallySheetInput input, HttpPostedFileBase file)
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
                        var specieCategories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                        ViewBag.SpecieCategoryId = specieCategories;
                        return View(input);
                    }
                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result = reader.AsDataSet();
                    reader.Close();
                    //Sending result data to database

                    _tallySheetAppService.UploadTallySheet(input, result.Tables[0]);
                    //return View();
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }

            return RedirectToAction("Create", new { id = input.PlotId});


        }

        // GET: TallySheets/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TallySheets/Edit/5
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

        // GET: TallySheets/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TallySheets/Delete/5
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
