namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RevenueSourceId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Fee = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RegistrationFee = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RevenueSources", t => t.RevenueSourceId, cascadeDelete: true)
                .Index(t => t.RevenueSourceId);
            
            CreateTable(
                "dbo.RevenueSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainRevenueSourceId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainRevenueSources", t => t.MainRevenueSourceId, cascadeDelete: true)
                .Index(t => t.MainRevenueSourceId);
            
            CreateTable(
                "dbo.MainRevenueSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        ParentId = c.Int(nullable: false),
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
                    { "DynamicFilter_MainRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
                        StationId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        BillControlNumber = c.String(),
                        Amount = c.Double(nullable: false),
                        IssuedDate = c.DateTime(),
                        PrintedUserId = c.Int(),
                        IsSubmitted = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsDenied = c.Boolean(nullable: false),
                        ApprovedUserId = c.Int(),
                        Remark = c.String(),
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
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.ApplicantId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Adress = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(),
                        TIN = c.String(),
                        FinancialYearId = c.Int(nullable: false),
                        IsTanzanian = c.Boolean(nullable: false),
                        IDtype = c.String(),
                        IDNumber = c.String(),
                        IDIssuePlace = c.String(),
                        IDExpiryDate = c.DateTime(),
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
                    { "DynamicFilter_Applicant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
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
                        ParentId = c.Int(nullable: false),
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
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        IssuedDate = c.DateTime(nullable: false),
                        ControlNumber = c.String(),
                        BillAmount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        EquvAmont = c.Double(nullable: false),
                        MiscAmont = c.Double(nullable: false),
                        Currency = c.String(),
                        PaidDate = c.DateTime(),
                        ExpiredDate = c.DateTime(nullable: false),
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
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
                .ForeignKey("dbo.Stations", t => t.StationId)
                .Index(t => t.ApplicantId)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.BillTransitPasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransitPassId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        AdditionInformation = c.String(),
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
                    { "DynamicFilter_BillTransitPass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.TransitPasses", t => t.TransitPassId)
                .Index(t => t.TransitPassId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.TransitPasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        OrginalCountry = c.String(),
                        NoOfConsignment = c.String(),
                        LisenceNo = c.String(),
                        TransitPassNo = c.String(),
                        SourceForest = c.Int(nullable: false),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        SourceName = c.String(),
                        DestinationId = c.Int(nullable: false),
                        DestinationName = c.String(),
                        VehcleNo = c.String(),
                        IssuerOfficer = c.Int(nullable: false),
                        HummerNo = c.String(),
                        HummerMaker = c.String(),
                        HummerStationId = c.String(),
                        AdditionInformation = c.String(),
                        Status = c.Boolean(nullable: false),
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
                    { "DynamicFilter_TransitPass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.BillId);
            
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
                "dbo.CheckPointTransitPasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransitPassId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                        InspectorId = c.Int(nullable: false),
                        InspectionStatus = c.Boolean(nullable: false),
                        AdditionInformation = c.String(),
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
                    { "DynamicFilter_CheckPointTransitPass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.TransitPasses", t => t.TransitPassId)
                .Index(t => t.TransitPassId)
                .Index(t => t.StationId);
            
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
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionId = c.Int(nullable: false),
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
                    { "DynamicFilter_District_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.ForestProduceAppliedForests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForestProduceRegistrationId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        Status = c.String(),
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
                    { "DynamicFilter_ForestProduceAppliedForest_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.ForestProduceRegistrations", t => t.ForestProduceRegistrationId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.ForestProduceRegistrationId)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.ForestProduceRegistrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequirementTitle = c.String(nullable: false),
                        RequirementDescription = c.String(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        HasForestBusinessLicense = c.Boolean(nullable: false),
                        TIN = c.String(),
                        BussinessLicenseNo = c.String(),
                        RegNumber = c.String(),
                        HasSawmill = c.Boolean(nullable: false),
                        IsSawmillInstalled = c.Boolean(nullable: false),
                        SawmillInstalledLocation = c.String(),
                        InstallationPermitNo = c.String(),
                        TypeOfSawmill = c.String(),
                        SawmillCapacityPerYear = c.String(),
                        HasTrucks = c.Boolean(nullable: false),
                        TrucksOwnerAttachment = c.String(),
                        SawmillHasLoadingBench = c.Boolean(nullable: false),
                        SawmillHasBreakDownSaw = c.Boolean(nullable: false),
                        SawmillHasReceivingBench = c.Boolean(nullable: false),
                        HasChainSawTechnician = c.Boolean(nullable: false),
                        HasTrainedOperator = c.Boolean(nullable: false),
                        OperatorInstitutionName = c.String(),
                        OperatorCertificateAttachment = c.String(),
                        HasSawmillOperator = c.Boolean(nullable: false),
                        TaxClearance = c.String(),
                        CertifiedAudted = c.String(),
                        CertificatePrented = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Resoan = c.String(),
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
                    { "DynamicFilter_ForestProduceRegistration_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
                .Index(t => t.ApplicantId)
                .Index(t => t.FinancialYearId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.ForestProduceAppliedSpecieCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForestProduceRegistrationId = c.Int(nullable: false),
                        SpecieCategoryId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        Status = c.String(),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                    { "DynamicFilter_ForestProduceAppliedSpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.ForestProduceRegistrations", t => t.ForestProduceRegistrationId, cascadeDelete: true)
                .ForeignKey("dbo.SpecieCategories", t => t.SpecieCategoryId, cascadeDelete: true)
                .Index(t => t.ForestProduceRegistrationId)
                .Index(t => t.SpecieCategoryId)
                .Index(t => t.FinancialYearId);
            
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
                "dbo.HarvestingLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlotId = c.Int(nullable: false),
                        DealerId = c.Int(nullable: false),
                        LicenseId = c.Int(nullable: false),
                        TruckNumber = c.String(nullable: false),
                        DriverName = c.String(nullable: false),
                        AccumulativeCBM = c.Double(nullable: false),
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
                    { "DynamicFilter_HarvestingLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dealers", t => t.DealerId)
                .ForeignKey("dbo.Licenses", t => t.LicenseId)
                .ForeignKey("dbo.Plots", t => t.PlotId)
                .Index(t => t.PlotId)
                .Index(t => t.DealerId)
                .Index(t => t.LicenseId);
            
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
                        ExpiredDate = c.DateTime(nullable: false),
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
                "dbo.InspectionAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckPointTransitPassId = c.Int(nullable: false),
                        Action = c.String(),
                        AdditionInformation = c.String(),
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
                    { "DynamicFilter_InspectionAudit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CheckPointTransitPasses", t => t.CheckPointTransitPassId)
                .Index(t => t.CheckPointTransitPassId);
            
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
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        PaymentControlNo = c.String(),
                        BillAmount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        PayOption = c.String(),
                        Currency = c.String(),
                        TranDate = c.DateTime(nullable: false),
                        UsedPayChannel = c.String(),
                        PayerCellNo = c.String(),
                        PayerName = c.String(),
                        PayerEmail = c.String(),
                        PspReceiptNo = c.String(),
                        PspName = c.String(),
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
                    { "DynamicFilter_Payment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.RefApplicantTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
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
                    { "DynamicFilter_RefApplicantType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefIdentityTypes",
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
                    { "DynamicFilter_RefIdentityType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefSubRevenueSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RevenueResourceId = c.Int(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Royalty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaFF = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CESS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TREE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LMDA = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                    { "DynamicFilter_RefSubRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            
            CreateTable(
                "dbo.ExportDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        SpecieCategoryId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                        FinancialYearId = c.Int(nullable: false),
                        BankName = c.String(nullable: false),
                        BankAddress = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        ShipmentDate = c.DateTime(nullable: false),
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
                    { "DynamicFilter_ExportDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId)
                .ForeignKey("dbo.SpecieCategories", t => t.SpecieCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.SpecieCategoryId)
                .Index(t => t.StationId)
                .Index(t => t.FinancialYearId);
            
            CreateTable(
                "dbo.ExportSpecies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExportDetailId = c.Int(nullable: false),
                        SpecieId = c.Int(nullable: false),
                        Quantity = c.String(nullable: false),
                        Size = c.String(nullable: false),
                        Price = c.Double(nullable: false),
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
                    { "DynamicFilter_ExportSpecie_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExportDetails", t => t.ExportDetailId, cascadeDelete: true)
                .ForeignKey("dbo.Species", t => t.SpecieId)
                .Index(t => t.ExportDetailId)
                .Index(t => t.SpecieId);
            
            AddColumn("dbo.AbpUsers", "StationId", c => c.Int(nullable: false));
            AddColumn("dbo.AbpUsers", "ApplicantId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExportSpecies", "SpecieId", "dbo.Species");
            DropForeignKey("dbo.ExportSpecies", "ExportDetailId", "dbo.ExportDetails");
            DropForeignKey("dbo.ExportDetails", "StationId", "dbo.Stations");
            DropForeignKey("dbo.ExportDetails", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.ExportDetails", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.ExportDetails", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.TallySheets", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.TallySheets", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.Species", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.Payments", "BillId", "dbo.Bills");
            DropForeignKey("dbo.InspectionAudits", "CheckPointTransitPassId", "dbo.CheckPointTransitPasses");
            DropForeignKey("dbo.HarvestingPlans", "StationId", "dbo.Stations");
            DropForeignKey("dbo.HarvestingPlans", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.HarvestingLogs", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.HarvestingLogs", "LicenseId", "dbo.Licenses");
            DropForeignKey("dbo.Licenses", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Licenses", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Licenses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.HarvestingLogs", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.ForestProduceAppliedSpecieCategories", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.ForestProduceAppliedSpecieCategories", "ForestProduceRegistrationId", "dbo.ForestProduceRegistrations");
            DropForeignKey("dbo.ForestProduceAppliedSpecieCategories", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.ForestProduceAppliedForests", "StationId", "dbo.Stations");
            DropForeignKey("dbo.ForestProduceAppliedForests", "ForestProduceRegistrationId", "dbo.ForestProduceRegistrations");
            DropForeignKey("dbo.ForestProduceRegistrations", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.ForestProduceRegistrations", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.ForestProduceRegistrations", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ForestProduceAppliedForests", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Districts", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.DealerActivities", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.DealerActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.CheckPointTransitPasses", "TransitPassId", "dbo.TransitPasses");
            DropForeignKey("dbo.CheckPointTransitPasses", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Candidates", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Candidates", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.BillTransitPasses", "TransitPassId", "dbo.TransitPasses");
            DropForeignKey("dbo.TransitPasses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.TransitPasses", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.BillTransitPasses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Bills", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Bills", "ApplicantId", "dbo.Applicants");
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
            DropForeignKey("dbo.Dealers", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Applicants", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Activities", "RevenueSourceId", "dbo.RevenueSources");
            DropForeignKey("dbo.RevenueSources", "MainRevenueSourceId", "dbo.MainRevenueSources");
            DropIndex("dbo.ExportSpecies", new[] { "SpecieId" });
            DropIndex("dbo.ExportSpecies", new[] { "ExportDetailId" });
            DropIndex("dbo.ExportDetails", new[] { "FinancialYearId" });
            DropIndex("dbo.ExportDetails", new[] { "StationId" });
            DropIndex("dbo.ExportDetails", new[] { "SpecieCategoryId" });
            DropIndex("dbo.ExportDetails", new[] { "ApplicantId" });
            DropIndex("dbo.TallySheets", new[] { "SpecieCategoryId" });
            DropIndex("dbo.TallySheets", new[] { "PlotId" });
            DropIndex("dbo.Species", new[] { "SpecieCategoryId" });
            DropIndex("dbo.Payments", new[] { "BillId" });
            DropIndex("dbo.InspectionAudits", new[] { "CheckPointTransitPassId" });
            DropIndex("dbo.HarvestingPlans", new[] { "FinancialYearId" });
            DropIndex("dbo.HarvestingPlans", new[] { "StationId" });
            DropIndex("dbo.Licenses", new[] { "BillId" });
            DropIndex("dbo.Licenses", new[] { "FinancialYearId" });
            DropIndex("dbo.Licenses", new[] { "StationId" });
            DropIndex("dbo.HarvestingLogs", new[] { "LicenseId" });
            DropIndex("dbo.HarvestingLogs", new[] { "DealerId" });
            DropIndex("dbo.HarvestingLogs", new[] { "PlotId" });
            DropIndex("dbo.ForestProduceAppliedSpecieCategories", new[] { "FinancialYearId" });
            DropIndex("dbo.ForestProduceAppliedSpecieCategories", new[] { "SpecieCategoryId" });
            DropIndex("dbo.ForestProduceAppliedSpecieCategories", new[] { "ForestProduceRegistrationId" });
            DropIndex("dbo.ForestProduceRegistrations", new[] { "DistrictId" });
            DropIndex("dbo.ForestProduceRegistrations", new[] { "FinancialYearId" });
            DropIndex("dbo.ForestProduceRegistrations", new[] { "ApplicantId" });
            DropIndex("dbo.ForestProduceAppliedForests", new[] { "FinancialYearId" });
            DropIndex("dbo.ForestProduceAppliedForests", new[] { "StationId" });
            DropIndex("dbo.ForestProduceAppliedForests", new[] { "ForestProduceRegistrationId" });
            DropIndex("dbo.Districts", new[] { "RegionId" });
            DropIndex("dbo.DealerActivities", new[] { "ActivityId" });
            DropIndex("dbo.DealerActivities", new[] { "DealerId" });
            DropIndex("dbo.CheckPointTransitPasses", new[] { "StationId" });
            DropIndex("dbo.CheckPointTransitPasses", new[] { "TransitPassId" });
            DropIndex("dbo.Candidates", new[] { "FinancialYearId" });
            DropIndex("dbo.Candidates", new[] { "StationId" });
            DropIndex("dbo.TransitPasses", new[] { "BillId" });
            DropIndex("dbo.TransitPasses", new[] { "ApplicantId" });
            DropIndex("dbo.BillTransitPasses", new[] { "BillId" });
            DropIndex("dbo.BillTransitPasses", new[] { "TransitPassId" });
            DropIndex("dbo.Bills", new[] { "FinancialYearId" });
            DropIndex("dbo.Bills", new[] { "StationId" });
            DropIndex("dbo.Bills", new[] { "ApplicantId" });
            DropIndex("dbo.Divisions", new[] { "StationId" });
            DropIndex("dbo.Ranges", new[] { "DivisionId" });
            DropIndex("dbo.Compartments", new[] { "FinancialYearId" });
            DropIndex("dbo.Compartments", new[] { "RangeId" });
            DropIndex("dbo.Plots", new[] { "CompartmentId" });
            DropIndex("dbo.Stations", new[] { "RegionId" });
            DropIndex("dbo.Stations", new[] { "ZoneId" });
            DropIndex("dbo.Applicants", new[] { "FinancialYearId" });
            DropIndex("dbo.Dealers", new[] { "FinancialYearId" });
            DropIndex("dbo.Dealers", new[] { "ApplicantId" });
            DropIndex("dbo.Dealers", new[] { "StationId" });
            DropIndex("dbo.AllocatedPlots", new[] { "FinancialYearId" });
            DropIndex("dbo.AllocatedPlots", new[] { "PlotId" });
            DropIndex("dbo.AllocatedPlots", new[] { "DealerId" });
            DropIndex("dbo.RevenueSources", new[] { "MainRevenueSourceId" });
            DropIndex("dbo.Activities", new[] { "RevenueSourceId" });
            DropColumn("dbo.AbpUsers", "ApplicantId");
            DropColumn("dbo.AbpUsers", "StationId");
            DropTable("dbo.ExportSpecies",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExportSpecie_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExportDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExportDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
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
            DropTable("dbo.RefSubRevenueSources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefSubRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RefIdentityTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefIdentityType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RefApplicantTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefApplicantType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Payments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Payment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.LicenseCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LicenseCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.InspectionAudits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InspectionAudit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.HarvestingPlans",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HarvestingPlan_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Licenses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_License_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.HarvestingLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HarvestingLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.GnTreeVolumeRates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GnTreeVolumeRate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SpecieCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ForestProduceAppliedSpecieCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ForestProduceAppliedSpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ForestProduceRegistrations",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ForestProduceRegistration_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ForestProduceAppliedForests",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ForestProduceAppliedForest_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Districts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_District_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.DealerActivities",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DealerActivity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CheckPointTransitPasses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CheckPointTransitPass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Candidates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Candidate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.TransitPasses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TransitPass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BillTransitPasses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillTransitPass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Bills",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Bill_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.Applicants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Applicant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.MainRevenueSources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MainRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RevenueSources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Activities",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Activity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
