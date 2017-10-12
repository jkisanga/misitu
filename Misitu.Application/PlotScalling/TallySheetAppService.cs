using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.PlotScalling.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Misitu.Tariffs;
using Misitu.GnTreeVolumeRates;
using Misitu.Species;
using Abp.UI;

namespace Misitu.PlotScalling
{
    public class TallySheetAppService : MisituAppServiceBase, ITallySheetAppService
    {
        private readonly IRepository<TallySheet> _tallySheetRepository;
        private readonly IRepository<GnTreeVolumeRate> _gnRepository;
        private readonly IRepository<Tariff> _tariffRepository;
        private readonly IRepository<SpecieCategory> _specieCategoryRepository;
        private readonly IRepository<Plot> _plotRepository;

        public TallySheetAppService(
            IRepository<TallySheet> tallySheetRepository,
            IRepository<GnTreeVolumeRate> gnRepository,
            IRepository<Tariff> tariffRepository,
            IRepository<SpecieCategory> specieCategoryRepository,
            IRepository<Plot> plotRepository
            )
        {
            _tallySheetRepository = tallySheetRepository;
            _gnRepository = gnRepository;
            _tariffRepository = tariffRepository;
            _specieCategoryRepository = specieCategoryRepository;
            _plotRepository = plotRepository;
        }


        public TallySheetDto GetTallySheet(int id)
        {
            var tallySheet = _tallySheetRepository.FirstOrDefault(i => i.Id == id);

            return tallySheet.MapTo<TallySheetDto>();
        }

        public List<TallySheetDto> GetTallySheets(PlotDto plot)
        {
            var tallySheets = _tallySheetRepository
               .GetAll()
               .Where(x => x.PlotId == plot.Id)
               .OrderBy(p => p.DBH)
               .ToList();

            return new List<TallySheetDto>(tallySheets.MapTo<List<TallySheetDto>>());
        }

        public void UploadTallySheet(CreateTallySheetInput input, DataTable table)
        {
            //Iterate data row from excel tally sheet 

            var category = _specieCategoryRepository.FirstOrDefault(input.SpecieCategoryId);
            var plot = _plotRepository.FirstOrDefault(input.PlotId);

            foreach (DataRow row in table.Rows)
            {
                double tarifValue;
                //int number;
                //if (Int32.TryParse(row["DBH"].ToString(), out number))
                //{
                //    throw new Exception("THE ISSUE IS HERE " + row["DBH"].ToString() + "sheet" + table.ToString());
                //}
                 
                int dbh =  Convert.ToInt32(row["DBH"].ToString());

                var gnAmount = _gnRepository.FirstOrDefault(i => i.Dbh == dbh);//GN amount for each DBH
                var tarif = _tariffRepository.FirstOrDefault(i => i.DBH == dbh);// tariff value for each DBH
                

               // determine row value of  each dbh from the selected compartment tarifff number 
                tarifValue = 0;
                switch (input.TariffNumber)
                {
                    case 40:
                        tarifValue = tarif.T40;
                        break;
                    case 41:
                        tarifValue = tarif.T41;
                        break;
                    case 42:
                        tarifValue = tarif.T42;
                        break;
                    case 43:
                        tarifValue = tarif.T43;
                        break;
                    case 44:
                        tarifValue = tarif.T44;
                        break;
                    case 45:
                        tarifValue = tarif.T45;
                        break;
                    case 46:
                        tarifValue = tarif.T46;
                        break;
                    case 47:
                        tarifValue = tarif.T47;
                        break;
                    case 48:
                        tarifValue = tarif.T48;
                        break;
                    case 49:
                        tarifValue = tarif.T49;
                        break;
                    case 50:
                        tarifValue = tarif.T50;
                        break;
                    case 51:
                        tarifValue = tarif.T51;
                        break;
                    case 52:
                        tarifValue = tarif.T52;
                        break;
                    case 53:
                        tarifValue = tarif.T53;
                        break;
                    case 54:
                        tarifValue = tarif.T54;
                        break;
                    case 55:
                        tarifValue = tarif.T55;
                        break;
                    case 56:
                        tarifValue = tarif.T56;
                        break;
                    case 57:
                        tarifValue = tarif.T57;
                        break;
                    case 58:
                        tarifValue = tarif.T58;
                        break;
                    case 59:
                        tarifValue = tarif.T59;
                        break;
                    case 60:
                        tarifValue = tarif.T60;
                        break;
                    case 61:
                        tarifValue = tarif.T61;
                        break;
                    case 62:
                        tarifValue = tarif.T62;
                        break;
                    case 63:
                        tarifValue = tarif.T63;
                        break;
                    case 64:
                        tarifValue = tarif.T64;
                        break;
                    case 65:
                        tarifValue = tarif.T65;
                        break;
                    case 66:
                        tarifValue = tarif.T66;
                        break;
                    case 67:
                        tarifValue = tarif.T67;
                        break;
                    case 68:
                        tarifValue = tarif.T68;
                        break;
                    case 69:
                        tarifValue = tarif.T69;
                        break;
                    case 70:
                        tarifValue = tarif.T70;
                        break;
                    case 71:
                        tarifValue = tarif.T71;
                        break;
                    case 72:
                        tarifValue = tarif.T72;
                        break;
                }

                //Distribution calculations
                double volume = (tarifValue * Convert.ToInt32(row["NO_OF_TREES"].ToString()));
                double lmda = (volume * category.Amount);
                double loyality1 = (volume * gnAmount.Amount);
                double tff = (0.03 * loyality1);
                double loyality = (0.97 * loyality1);
                double vat = (0.18 * loyality);
                double cess = (0.05 * loyality);           
                double tp = (volume * 7500) / 12.234;
                double total = loyality + lmda + vat + cess + tff + tp;

                if (plot != null)
                {
                    var sheet = new TallySheet
                    {
                        DBH = Convert.ToInt32(row["DBH"].ToString()),
                        PlotId = input.PlotId,
                        SpecieCategoryId = input.SpecieCategoryId,
                        TreesNumber = Convert.ToInt32(row["NO_OF_TREES"].ToString()),
                        GnAmount = gnAmount.Amount,
                        Volume = volume,
                        Loyality = loyality,
                        LMDA = lmda,
                        VAT = vat,
                        CESS = cess,
                        TFF = tff,
                        TP = tp,
                        TOTAL = total
                    };
                    _tallySheetRepository.Insert(sheet);
                }else
                {
                    throw new UserFriendlyException("There is already a TallySheet for selected Plot");
                }
            }
        }

    }
}
