using Abp.Runtime.Validation;
using Abp.UI;
using Excel;
using Misitu.FinancialYears;
using Misitu.PlotScalling;
using Misitu.PlotScalling.Dto;
using Misitu.Ranges;
using Misitu.Registration;
using Misitu.Species;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Misitu.Web.Controllers.PlotScalling
{
    [DisableValidation]
    public class CompartmentsController : Controller
    {
        private readonly ICompartmentAppService _compartmentAppService;
        private readonly IRangeAppService _rangeAppService;
        private readonly IPlotAppService _plotAppService;
        private readonly ITallySheetAppService _tallySheetAppService;
        private readonly ISpecieCategoryAppService _specieCategoryAppService;
      

        public CompartmentsController(ICompartmentAppService compartmentAppService,
            IRangeAppService rangeAppSerice, IPlotAppService plotAppService, 
            ITallySheetAppService tallySheetAppService,
            ISpecieCategoryAppService specieCategoryAppService,
            IDealerAppService dealerAppService,
             IFinancialYearAppService financialYearAppService
            )
        {
            _compartmentAppService = compartmentAppService;
            _rangeAppService = rangeAppSerice;
            _plotAppService = plotAppService;
            _tallySheetAppService = tallySheetAppService;
            _specieCategoryAppService = specieCategoryAppService;

        }
        // GET: Compartment/Dashboard

        public ActionResult Dashboard()
        {

            return View();
        }
        // GET: Compartment
        public ActionResult Index()
        {
            var compartments = _compartmentAppService.GetCompartments();
            return View(compartments);
        }

        // GET: Compartment
        public ActionResult Tallied()
        {
            var compartments = _compartmentAppService.GetCompartments();
            return View(compartments);
        }



        // GET: Compartment
        public ActionResult TalliedCompartments()
        {
            var compartments = _compartmentAppService.GetCompartments();
            return View(compartments);
        }



        // GET: Compartment
        public ActionResult RangeList()
        {
            var ranges = _rangeAppService.GetRanges();
            return View(ranges);
        }

        // GET: Compartment
        public ActionResult Allocation(int id)
        {
            var compartments = _compartmentAppService.GetTalliedPlotsByRange(id);
            return View(compartments);
        }
        // GET: Ranges/Create
        public ActionResult Create()
        {
            var YearSelectList = new SelectList(Enumerable.Range(1960, (DateTime.Now.Year - 1960) + 1));
            var ranges = _rangeAppService.GetRanges().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.RangeId = ranges;
            ViewBag.PlantedYear = YearSelectList;
            return View();
        }

        // POST: Ranges/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateCompartmentInput input)
        {
            if (ModelState.IsValid)
            {
                await _compartmentAppService.CreateCompartment(input);
                return RedirectToAction("Index");
            }
            else
            {
                var ranges = _rangeAppService.GetRanges().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.RangeId = ranges;
                return View(input);
            }
        }

        // GET: Ranges/Edit/5
        public ActionResult Edit(int id)
        {
            var compartment = _compartmentAppService.GetCompartment(id);
            var ranges = _rangeAppService.GetRanges().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.RangeId = ranges;
            return View(compartment);
        }

        // POST: Ranges/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, CompartmentDto input)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                await _compartmentAppService.UpdateCompartment(input);
                return RedirectToAction("Index");
            }
            else
            {
                var ranges = _rangeAppService.GetRanges().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                ViewBag.RangeId = ranges;
                return View(input);
            }
        }

        // GET: Ranges/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var compartment = _compartmentAppService.GetCompartment(id);
            await _compartmentAppService.DeleteCompartmentAsync(compartment);
            return RedirectToAction("Index");
        }

        public ActionResult Talling(int id)
        {
            var compartment = _compartmentAppService.GetCompartment(id);
            ViewBag.compartment = compartment;
            var specieCategories = _specieCategoryAppService.GetSpecieCategories().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.SpecieCategoryId = specieCategories;
            return View();
        }

        [HttpPost]
        public ActionResult Talling(int CompartmentId,int SpecieCategoryId, int TariffNumber, HttpPostedFileBase file)
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

                List<String> sheetNames = new List<String>();
                int plotId;

                //Loop through data sheet
                while (reader.Read())
                {


                    CreatePlotInput input = new CreatePlotInput();
                    CreateTallySheetInput tallyInput = new CreateTallySheetInput();


                    input.CompartmentId = CompartmentId;
                    input.Name = reader.Name;

                    plotId = _plotAppService.CreatePlot(input);

                    tallyInput.PlotId = plotId;
                    tallyInput.SpecieCategoryId = SpecieCategoryId;
                    tallyInput.TariffNumber = TariffNumber;


                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result1 = reader.AsDataSet();
                    reader.Close();
                    //Sending result data to database

                    _tallySheetAppService.UploadTallySheet(tallyInput, result1.Tables[reader.Name]) ;

                    while (reader.NextResult())
                    {
                        try
                        {
                            input.CompartmentId = CompartmentId;
                            input.Name = reader.Name;
                            plotId = _plotAppService.CreatePlot(input);

                            tallyInput.PlotId = plotId;
                            tallyInput.SpecieCategoryId = SpecieCategoryId;
                            tallyInput.TariffNumber = TariffNumber;

                            _tallySheetAppService.UploadTallySheet(tallyInput, result1.Tables[reader.Name]);
                        }
                        catch(Exception ex)
                        {
                            throw new UserFriendlyException(ex + "Error in sheet"+ reader);
                        }
                      
                    }
                    reader.Close();
                }


                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("File", "Please upload your file");
                return RedirectToAction("Index");
            }


          
        }


        public ActionResult TalledSummary(int id)
        {
          //  var finacialYear = _financialYearAppService.GetActiveFinancialYear();
            var compartment = _compartmentAppService.GetCompartment(id);
            var plots = _plotAppService.GetPlotsByCompartment(id);

            ViewBag.Compartment = compartment;
            return View(plots);
        }


        // GET: Plots
        public ActionResult PlotDetails(int id)
        {
            var plot = _plotAppService.GetPlot(id);
            var compartment = _compartmentAppService.GetCompartment(plot.CompartmentId);
            var sheets = _tallySheetAppService.GetTallySheets(plot);
            ViewBag.Plot = plot;
            ViewBag.Compartment = compartment;
            ViewBag.Sheets = sheets;
            return View();
        }



    }
}
