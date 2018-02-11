using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Misitu.EntityFramework
{
    public class MisituDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MisituDbContext() : base("name=MisituDbContext")
        {
        }

        public System.Data.Entity.DbSet<Misitu.TransitPasses.TransitPassDto> TransitPassDtoes { get; set; }

        public System.Data.Entity.DbSet<Misitu.Applicants.Applicant> Applicants { get; set; }

        public System.Data.Entity.DbSet<Misitu.Applicants.ApplicantDto> ApplicantDtoes { get; set; }

        public System.Data.Entity.DbSet<Misitu.FinancialYears.FinancialYear> FinancialYears { get; set; }

        public System.Data.Entity.DbSet<Misitu.Billing.Dto.BillItemDto> BillItemDtoes { get; set; }

        public System.Data.Entity.DbSet<Misitu.Billing.Bill> Bills { get; set; }

        public System.Data.Entity.DbSet<Misitu.RevenueSources.RevenueSource> RevenueSources { get; set; }

        public System.Data.Entity.DbSet<Misitu.Billing.Dto.BillDto> BillDtoes { get; set; }

        public System.Data.Entity.DbSet<Misitu.Stations.Statiton> Statitons { get; set; }
    }
}
