using Abp.Runtime.Validation;
using Excel;
using Misitu.Tariffs;
using Misitu.Tariffs.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers
{
    [DisableValidation]
    public class TariffsController : MisituControllerBase
    {

        private readonly ITariffAppService _tariffAppService;

        public TariffsController(ITariffAppService tariffAppService)
        {
            _tariffAppService = tariffAppService;
        }

        // GET: Tariffs
        public ActionResult Index()
        {
            var tariffs = _tariffAppService.GetTariffs();
            return View(tariffs);
        }



        // GET: Tariffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tariffs/Create
      
        [HttpPost]
        public  ActionResult Create(HttpPostedFileBase file)
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

                     _tariffAppService.UploadTariff(result.Tables[0]);
                    //return View();
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }

            return View();


        }

        // GET: Tariffs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tariffs/Edit/5
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

        // GET: Tariffs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tariffs/Delete/5
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
