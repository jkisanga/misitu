using System.Data.Common;
using Abp.Zero.EntityFramework;
using Misitu.Authorization.Roles;
using Misitu.MultiTenancy;
using Misitu.Users;
using Misitu.Zones;
using System.Data.Entity;
using Misitu.FinancialYears;
using Misitu.Regions;
using Misitu.Stations;
using Misitu.Activities;
using Misitu.Divisions;
using Misitu.Ranges;
using Misitu.Tariffs;
using Misitu.Registration;
using Misitu.Billing;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection.Emit;
using Misitu.PlotScalling;
using Misitu.GnTreeVolumeRates;
using Misitu.Licensing;
using Misitu.Species;
using Misitu.RevenueSources;

using Misitu.Harvesting;

using Misitu.RefereneceTables;
using Misitu.Applicants;
using Misitu.Applicants.ForestProduce;


namespace Misitu.EntityFramework
{
    public class MisituDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public virtual IDbSet<Zone> Zones { get; set; }
        public virtual IDbSet<FinancialYear> FinancialYears { get; set; }
        public virtual IDbSet<Region> Regions { get; set; }
        public virtual IDbSet<Statiton> Stations { get; set; }
        
        public virtual IDbSet<Division> Divisions { get; set; }
        public virtual IDbSet<Range> Ranges { get; set; }
        public virtual IDbSet<Tariff> Tariffs { get; set; }
        public virtual IDbSet<GnTreeVolumeRate> GnTreeVolumeRates { get; set; }
        public virtual IDbSet<SpecieCategory> SpecieCategories {get; set; }
        public virtual IDbSet<Specie> Species {get; set; }
        public virtual IDbSet<RevenueSource> RevenueSources { get; set; }

        //Registration Tables
        public virtual IDbSet<Candidate> Candidates { get; set; }
        public virtual IDbSet<Dealer> Dealers { get; set; }
       

        //Billing
        public virtual IDbSet<Bill> Bills { get; set; }
        public virtual IDbSet<BillItem> BillItems { get; set; }

        //Plot Scalling
        public virtual IDbSet<HarvestingPlan> HarvestingPlans { get; set; }
        public virtual IDbSet<Compartment> Compartments { get; set; }
        public virtual IDbSet<Plot> Plots { get; set; }
        public virtual IDbSet<TallySheet> TallySheets { get; set; }

        //Harvesting
        public virtual IDbSet<HarvestingLog> HarvestingLogs { get; set; }

        //Licensing
        public virtual IDbSet<AllocatedPlot> AllocatedPlots { get; set; }
        public virtual IDbSet<LicenseCategory> LicenseCategories { get; set; }
        public virtual IDbSet<License> Licenses { get; set; }

        public virtual IDbSet<RefSubRevenueSource> RefSubRevenueSources { get; set; }


        //Table za Online Application (Cliant Applicantion/Account/Profile)
        public virtual IDbSet<RefApplicantType> RefApplicantTypes { get; set; }
        public virtual IDbSet<RefIdentityType> RefIdentityTypes { get; set; }
        public virtual IDbSet<District> Districts { get; set; }

        public virtual IDbSet<RefServiceCategory> RefServiceCategories { get; set; }
        public virtual IDbSet<Activity> Activities { get; set; }
        public virtual IDbSet<DealerActivity> DealerActivities { get; set; }
        public virtual IDbSet<Applicant> Applicants { get; set; }
        public virtual IDbSet<ForestProduceRegistration> ForestProduceRegistrations { get; set; }
        public virtual IDbSet<ForestProduceAppliedSpecieCategory> ForestProduceAppliedSpecieCategories { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<License>().HasRequired(i => i.Station).WithMany().HasForeignKey(k => k.StationId).WillCascadeOnDelete(false);
            modelBuilder.Entity<License>().HasRequired(i => i.FinancialYear).WithMany().HasForeignKey(k => k.FinancialYearId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Bill>().HasRequired(i => i.Station).WithMany().HasForeignKey(k => k.StationId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Bill>().HasRequired(i => i.FinancialYear).WithMany().HasForeignKey(k => k.FinancialYearId).WillCascadeOnDelete(false);
            modelBuilder.Entity<AllocatedPlot>().HasRequired(i => i.FinancialYear).WithMany().HasForeignKey(k => k.FinancialYearId).WillCascadeOnDelete(false);
            modelBuilder.Entity<AllocatedPlot>().HasRequired(i => i.Plot).WithMany().HasForeignKey(k => k.PlotId).WillCascadeOnDelete(false);
<<<<<<< HEAD
=======

>>>>>>> 3c97757f263e81800f84b3ab94f066469c05d9f5
            modelBuilder.Entity<HarvestingLog>().HasRequired(i => i.License).WithMany().HasForeignKey(k => k.LicenseId).WillCascadeOnDelete(false);
            modelBuilder.Entity<HarvestingLog>().HasRequired(i => i.Dealer).WithMany().HasForeignKey(k => k.DealerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<HarvestingLog>().HasRequired(i => i.Plot).WithMany().HasForeignKey(k => k.PlotId).WillCascadeOnDelete(false);

            modelBuilder.Entity<ForestProduceRegistration>().HasRequired(i => i.Applicant).WithMany().HasForeignKey(k => k.ApplicantId).WillCascadeOnDelete(false);
            modelBuilder.Entity<ForestProduceRegistration>().HasRequired(i => i.FinancialYear).WithMany().HasForeignKey(k => k.FinancialYearId).WillCascadeOnDelete(false);
            modelBuilder.Entity<ForestProduceRegistration>().HasRequired(i => i.District).WithMany().HasForeignKey(k => k.DistrictId).WillCascadeOnDelete(false);



        }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MisituDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MisituDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MisituDbContext since ABP automatically handles it.
         */
        public MisituDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public MisituDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public MisituDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
