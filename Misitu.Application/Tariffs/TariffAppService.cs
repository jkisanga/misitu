using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misitu.Tariffs.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web;
using System.Data;
using LinqToExcel;
using LinqToExcel.Attributes;
using LinqToExcel.Domain;
using System.IO;
using Abp.UI;

namespace Misitu.Tariffs
{
    public class TariffAppService : MisituAppServiceBase, ITariffAppService
    {

        private readonly IRepository<Tariff> _tariffAppSerive;

        public TariffAppService(IRepository<Tariff> tariffAppSerice)
        {
            _tariffAppSerive = tariffAppSerice;
        }

        public TariffDto GetTariff(int id)
        {
            var tariff = _tariffAppSerive.FirstOrDefault(i => i.Id == id);

            return tariff.MapTo<TariffDto>();
        }
        public List<TariffDto> GetTariffs()
        {
            var plots = _tariffAppSerive
                .GetAll()
                .OrderBy(p => p.DBH)
                .ToList();

            return new List<TariffDto>(plots.MapTo<List<TariffDto>>());
        }

        public  void UploadTariff(DataTable table)
        {
           
            foreach (DataRow row in table.Rows)
            {

                var tariff = new Tariff();
                tariff.DBH = Convert.ToInt32(row["DBH"].ToString());
                    tariff.T40 = Convert.ToDouble(row["T40"].ToString());
                    tariff.T41 = Convert.ToDouble(row["T41"].ToString());
                    tariff.T42 = Convert.ToDouble(row["T42"].ToString());
                    tariff.T43 = Convert.ToDouble(row["T43"].ToString());
                    tariff.T44 = Convert.ToDouble(row["T44"].ToString());
                    tariff.T45 = Convert.ToDouble(row["T45"].ToString());
                    tariff.T46 = Convert.ToDouble(row["T46"].ToString());
                    tariff.T47 = Convert.ToDouble(row["T47"].ToString());
                    tariff.T48 = Convert.ToDouble(row["T48"].ToString());
                    tariff.T49 = Convert.ToDouble(row["T49"].ToString());
                    tariff.T50 = Convert.ToDouble(row["T50"].ToString());
                    tariff.T51 = Convert.ToDouble(row["T51"].ToString());
                    tariff.T52 = Convert.ToDouble(row["T52"].ToString());
                    tariff.T53 = Convert.ToDouble(row["T53"].ToString());
                    tariff.T54 = Convert.ToDouble(row["T54"].ToString());
                    tariff.T55 = Convert.ToDouble(row["T55"].ToString());
                    tariff.T56 = Convert.ToDouble(row["T56"].ToString());
                    tariff.T57 = Convert.ToDouble(row["T57"].ToString());
                    tariff.T58 = Convert.ToDouble(row["T58"].ToString());
                    tariff.T59 = Convert.ToDouble(row["T59"].ToString());
                    tariff.T60 = Convert.ToDouble(row["T60"].ToString());
                    tariff.T61 = Convert.ToDouble(row["T61"].ToString());
                    tariff.T62 = Convert.ToDouble(row["T62"].ToString());
                    tariff.T63 = Convert.ToDouble(row["T63"].ToString());
                    tariff.T64 = Convert.ToDouble(row["T64"].ToString());
                    tariff.T65 = Convert.ToDouble(row["T65"].ToString());
                    tariff.T66 = Convert.ToDouble(row["T66"].ToString());
                    tariff.T67 = Convert.ToDouble(row["T67"].ToString());
                    tariff.T68 = Convert.ToDouble(row["T68"].ToString());
                    tariff.T69 = Convert.ToDouble(row["T69"].ToString());
                    tariff.T70 = Convert.ToDouble(row["T70"].ToString());
                    tariff.T71 = Convert.ToDouble(row["T71"].ToString());
                    tariff.T72 = Convert.ToDouble(row["T72"].ToString());

                _tariffAppSerive.InsertAsync(tariff);

            }

          
        }
    }
}
