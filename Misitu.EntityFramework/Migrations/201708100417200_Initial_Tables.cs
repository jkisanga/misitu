namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Fee = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Activity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AllocatedPlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealerId = c.Int(nullable: false),
                        PlotId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AllocatedPlot_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dealers", t => t.DealerId, cascadeDelete: true)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .Index(t => t.DealerId)
                .Index(t => t.PlotId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.Dealers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(),
                        Phone = c.String(nullable: false),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        ReceiptNumber = c.String(),
                        Amount = c.Double(nullable: false),
                        RegisteredDate = c.DateTime(nullable: false),
                        IssuedDate = c.DateTime(),
                        TIN = c.String(),
                        BusinessLicense = c.String(),
                        PaymentReferenceNumber = c.String(),
                        PrintedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Dealer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.FinancialYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FinancialYear_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ZoneId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Statiton_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.Zones", t => t.ZoneId, cascadeDelete: true)
                .Index(t => t.ZoneId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Region_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Zone_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompartmentId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        IsAllocated = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Plot_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compartments", t => t.CompartmentId, cascadeDelete: true)
                .Index(t => t.CompartmentId);
            
            CreateTable(
                "dbo.Compartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RangeId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        Species = c.String(nullable: false),
                        PlantedYear = c.String(nullable: false),
                        Age = c.Single(nullable: false),
                        Area = c.Single(nullable: false),
                        EstimatedVolume = c.Single(nullable: false),
                        Season = c.String(nullable: false),
                        TariffNumber = c.Single(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Compartment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.Ranges", t => t.RangeId, cascadeDelete: true)
                .Index(t => t.RangeId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.Ranges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DivisionId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Range_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StationId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Division_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId);
            
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        RevenueResourceId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Loyality = c.Double(nullable: false),
                        TFF = c.Double(nullable: false),
                        LMDA = c.Double(nullable: false),
                        VAT = c.Double(nullable: false),
                        CESS = c.Double(nullable: false),
                        TP = c.Double(nullable: false),
                        DataSheet = c.Double(nullable: false),
                        Others = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.RevenueSources", t => t.RevenueResourceId, cascadeDelete: true)
                .Index(t => t.BillId)
                .Index(t => t.RevenueResourceId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealerId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        ControlNumber = c.String(),
                        BillAmount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        Currency = c.String(),
                        PaidDate = c.DateTime(),
                        IsCanceled = c.Boolean(nullable: false),
                        Description = c.String(),
                        Reason = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Bill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dealers", t => t.DealerId, cascadeDelete: true)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .Index(t => t.DealerId)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.RevenueSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Adress = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(),
                        AllocatedCubicMetres = c.Double(nullable: false),
                        Species = c.String(),
                        IsRegistered = c.Boolean(nullable: false),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Candidate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.DealerActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealerId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DealerActivity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Dealers", t => t.DealerId, cascadeDelete: true)
                .Index(t => t.DealerId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.GnTreeVolumeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dbh = c.Int(nullable: false),
                        Class = c.String(),
                        Amount = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GnTreeVolumeRate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HarvestingPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        Path = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HarvestingPlan_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.LicenseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LicenseCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        serialNumber = c.String(),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        Location = c.String(),
                        PaidDate = c.DateTime(),
                        ReceiptNumber = c.String(),
                        IssuedDate = c.DateTime(nullable: false),
                        IssuedBy = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_License_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.SpecieCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecieCategoryId = c.Int(nullable: false),
                        EnglishName = c.String(nullable: false),
                        CommonName = c.String(),
                        SwahiliName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Specie_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SpecieCategories", t => t.SpecieCategoryId, cascadeDelete: true)
                .Index(t => t.SpecieCategoryId);
            
            CreateTable(
                "dbo.TallySheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlotId = c.Int(nullable: false),
                        SpecieCategoryId = c.Int(nullable: false),
                        DBH = c.Int(nullable: false),
                        TreesNumber = c.Int(nullable: false),
                        GnAmount = c.Double(nullable: false),
                        Volume = c.Double(nullable: false),
                        Loyality = c.Double(nullable: false),
                        TFF = c.Double(nullable: false),
                        LMDA = c.Double(nullable: false),
                        CESS = c.Double(nullable: false),
                        VAT = c.Double(nullable: false),
                        TP = c.Double(nullable: false),
                        TOTAL = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TallySheet_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plots", t => t.PlotId, cascadeDelete: true)
                .ForeignKey("dbo.SpecieCategories", t => t.SpecieCategoryId, cascadeDelete: true)
                .Index(t => t.PlotId)
                .Index(t => t.SpecieCategoryId);
            
            CreateTable(
                "dbo.Tariffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DBH = c.Int(nullable: false),
                        T40 = c.Double(nullable: false),
                        T41 = c.Double(nullable: false),
                        T42 = c.Double(nullable: false),
                        T43 = c.Double(nullable: false),
                        T44 = c.Double(nullable: false),
                        T45 = c.Double(nullable: false),
                        T46 = c.Double(nullable: false),
                        T47 = c.Double(nullable: false),
                        T48 = c.Double(nullable: false),
                        T49 = c.Double(nullable: false),
                        T50 = c.Double(nullable: false),
                        T51 = c.Double(nullable: false),
                        T52 = c.Double(nullable: false),
                        T53 = c.Double(nullable: false),
                        T54 = c.Double(nullable: false),
                        T55 = c.Double(nullable: false),
                        T56 = c.Double(nullable: false),
                        T57 = c.Double(nullable: false),
                        T58 = c.Double(nullable: false),
                        T59 = c.Double(nullable: false),
                        T60 = c.Double(nullable: false),
                        T61 = c.Double(nullable: false),
                        T62 = c.Double(nullable: false),
                        T63 = c.Double(nullable: false),
                        T64 = c.Double(nullable: false),
                        T65 = c.Double(nullable: false),
                        T66 = c.Double(nullable: false),
                        T67 = c.Double(nullable: false),
                        T68 = c.Double(nullable: false),
                        T69 = c.Double(nullable: false),
                        T70 = c.Double(nullable: false),
                        T71 = c.Double(nullable: false),
                        T72 = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tariff_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AbpUsers", "StationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TallySheets", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.TallySheets", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Species", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.Licenses", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Licenses", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Licenses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.HarvestingPlans", "StationId", "dbo.Stations");
            DropForeignKey("dbo.HarvestingPlans", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.DealerActivities", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.DealerActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Candidates", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Candidates", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.BillItems", "RevenueResourceId", "dbo.RevenueSources");
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Bills", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Bills", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.AllocatedPlots", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Plots", "CompartmentId", "dbo.Compartments");
            DropForeignKey("dbo.Compartments", "RangeId", "dbo.Ranges");
            DropForeignKey("dbo.Ranges", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Compartments", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.AllocatedPlots", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.AllocatedPlots", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.Dealers", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Stations", "ZoneId", "dbo.Zones");
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Dealers", "FinancialYearId", "dbo.FinancialYears");
            DropIndex("dbo.TallySheets", new[] { "SpecieCategoryId" });
            DropIndex("dbo.TallySheets", new[] { "PlotId" });
            DropIndex("dbo.Species", new[] { "SpecieCategoryId" });
            DropIndex("dbo.Licenses", new[] { "BillId" });
            DropIndex("dbo.Licenses", new[] { "FinancialYearId" });
            DropIndex("dbo.Licenses", new[] { "StationId" });
            DropIndex("dbo.HarvestingPlans", new[] { "FinancialYearId" });
            DropIndex("dbo.HarvestingPlans", new[] { "StationId" });
            DropIndex("dbo.DealerActivities", new[] { "ActivityId" });
            DropIndex("dbo.DealerActivities", new[] { "DealerId" });
            DropIndex("dbo.Candidates", new[] { "FinancialYearId" });
            DropIndex("dbo.Candidates", new[] { "StationId" });
            DropIndex("dbo.Bills", new[] { "FinancialYearId" });
            DropIndex("dbo.Bills", new[] { "StationId" });
            DropIndex("dbo.Bills", new[] { "DealerId" });
            DropIndex("dbo.BillItems", new[] { "RevenueResourceId" });
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropIndex("dbo.Divisions", new[] { "StationId" });
            DropIndex("dbo.Ranges", new[] { "DivisionId" });
            DropIndex("dbo.Compartments", new[] { "FinancialYearId" });
            DropIndex("dbo.Compartments", new[] { "RangeId" });
            DropIndex("dbo.Plots", new[] { "CompartmentId" });
            DropIndex("dbo.Stations", new[] { "RegionId" });
            DropIndex("dbo.Stations", new[] { "ZoneId" });
            DropIndex("dbo.Dealers", new[] { "FinancialYearId" });
            DropIndex("dbo.Dealers", new[] { "StationId" });
            DropIndex("dbo.AllocatedPlots", new[] { "FinancialYearId" });
            DropIndex("dbo.AllocatedPlots", new[] { "PlotId" });
            DropIndex("dbo.AllocatedPlots", new[] { "DealerId" });
            DropColumn("dbo.AbpUsers", "StationId");
            DropTable("dbo.Tariffs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tariff_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.TallySheets",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TallySheet_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Species",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Specie_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SpecieCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Licenses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_License_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LicenseCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LicenseCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.HarvestingPlans",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HarvestingPlan_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.GnTreeVolumeRates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GnTreeVolumeRate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.DealerActivities",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DealerActivity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Candidates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Candidate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RevenueSources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Bills",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Bill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BillItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Divisions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Division_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Ranges",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Range_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Compartments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Compartment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Plots",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Plot_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Zones",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Zone_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Regions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Region_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Stations",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Statiton_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.FinancialYears",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FinancialYear_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Dealers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Dealer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AllocatedPlots",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AllocatedPlot_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Activities",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Activity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
