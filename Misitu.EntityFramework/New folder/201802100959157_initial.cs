namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RevenueSourceId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(nullable: false),
                        Fee = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Flag = c.String(),
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
                "dbo.AbpAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256),
                        MethodName = c.String(maxLength: 256),
                        Parameters = c.String(maxLength: 1024),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 256),
                        Exception = c.String(maxLength: 2000),
                        ImpersonatorUserId = c.Long(),
                        ImpersonatorTenantId = c.Int(),
                        CustomData = c.String(maxLength: 2000),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBackgroundJobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobType = c.String(nullable: false, maxLength: 512),
                        JobArgs = c.String(nullable: false),
                        TryCount = c.Short(nullable: false),
                        NextTryTime = c.DateTime(nullable: false),
                        LastTryTime = c.DateTime(),
                        IsAbandoned = c.Boolean(nullable: false),
                        Priority = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.IsAbandoned, t.NextTryTime });
            
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        EquvAmont = c.Double(nullable: false),
                        MiscAmont = c.Double(nullable: false),
                        GfsCode = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
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
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId)
                .Index(t => t.ActivityId);
            
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
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        OrginalCountry = c.String(),
                        NoOfConsignment = c.String(),
                        LisenceNo = c.String(),
                        TransitPassNo = c.String(),
                        SourceForest = c.Int(nullable: false),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        ExpireDays = c.Int(nullable: false),
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
                        QRCode = c.Binary(),
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
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
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
                "dbo.AbpFeatures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false, maxLength: 2000),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        EditionId = c.Int(),
                        TenantId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantFeatureSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
            CreateTable(
                "dbo.AbpEditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
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
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExportAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExportDetailId = c.Int(nullable: false),
                        BrelaRegistrationCert = c.String(),
                        LicenceCert = c.String(),
                        TaxClearanceCert = c.String(),
                        EnquiryOrder = c.String(),
                        ExportReturns = c.String(),
                        ForestProduceRegCert = c.String(),
                        AutholizedLetter = c.String(),
                        SawMillerContract = c.String(),
                        MouCert = c.String(),
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
                    { "DynamicFilter_ExportAttachment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExportDetails", t => t.ExportDetailId, cascadeDelete: true)
                .Index(t => t.ExportDetailId);
            
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
                        CertificatePrinted = c.Boolean(nullable: false),
                        Status = c.String(),
                        Resoan = c.String(),
                        IsSubmitted = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsRejected = c.Boolean(nullable: false),
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
                "dbo.AbpLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 10),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        Icon = c.String(maxLength: 128),
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
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpLanguageTexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        LanguageName = c.String(nullable: false, maxLength: 10),
                        Source = c.String(nullable: false, maxLength: 128),
                        Key = c.String(nullable: false, maxLength: 256),
                        Value = c.String(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.AbpNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NotificationName = c.String(nullable: false, maxLength: 96),
                        Data = c.String(),
                        DataTypeName = c.String(maxLength: 512),
                        EntityTypeName = c.String(maxLength: 250),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512),
                        EntityId = c.String(maxLength: 96),
                        Severity = c.Byte(nullable: false),
                        UserIds = c.String(),
                        ExcludedUserIds = c.String(),
                        TenantIds = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpNotificationSubscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        NotificationName = c.String(maxLength: 96),
                        EntityTypeName = c.String(maxLength: 250),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512),
                        EntityId = c.String(maxLength: 96),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotificationSubscriptionInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.NotificationName, t.EntityTypeName, t.EntityId, t.UserId });
            
            CreateTable(
                "dbo.AbpOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 95),
                        DisplayName = c.String(nullable: false, maxLength: 128),
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
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.ParentId)
                .Index(t => t.ParentId);
            
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
                "dbo.AbpPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsGranted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RolePermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserPermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
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
                "dbo.RefUnitMeasures",
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
                    { "DynamicFilter_RefUnitMeasure_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        AuthenticationSource = c.String(maxLength: 64),
                        UserName = c.String(nullable: false, maxLength: 32),
                        TenantId = c.Int(),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        Name = c.String(nullable: false, maxLength: 32),
                        Surname = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        EmailConfirmationCode = c.String(maxLength: 328),
                        PasswordResetCode = c.String(maxLength: 328),
                        LockoutEndDateUtc = c.DateTime(),
                        AccessFailedCount = c.Int(nullable: false),
                        IsLockoutEnabled = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        IsPhoneNumberConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(),
                        IsTwoFactorEnabled = c.Boolean(nullable: false),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LastLoginTime = c.DateTime(),
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
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUserClaims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserClaim_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLogin_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Setting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
                "dbo.AbpTenantNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        NotificationName = c.String(nullable: false, maxLength: 96),
                        Data = c.String(),
                        DataTypeName = c.String(maxLength: 512),
                        EntityTypeName = c.String(maxLength: 250),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512),
                        EntityId = c.String(maxLength: 96),
                        Severity = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EditionId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 128),
                        TenancyName = c.String(nullable: false, maxLength: 64),
                        ConnectionString = c.String(maxLength: 1024),
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
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.EditionId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.TransitPassItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransitPassId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        UnitMeasureId = c.Int(nullable: false),
                        SpecieId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
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
                    { "DynamicFilter_TransitPassItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Species", t => t.SpecieId, cascadeDelete: true)
                .ForeignKey("dbo.TransitPasses", t => t.TransitPassId, cascadeDelete: true)
                .ForeignKey("dbo.RefUnitMeasures", t => t.UnitMeasureId, cascadeDelete: true)
                .Index(t => t.TransitPassId)
                .Index(t => t.ActivityId)
                .Index(t => t.UnitMeasureId)
                .Index(t => t.SpecieId);
            
            CreateTable(
                "dbo.AbpUserAccounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        UserLinkId = c.Long(),
                        UserName = c.String(),
                        EmailAddress = c.String(),
                        LastLoginTime = c.DateTime(),
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
                    { "DynamicFilter_UserAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpUserLoginAttempts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        TenancyName = c.String(maxLength: 64),
                        UserId = c.Long(),
                        UserNameOrEmailAddress = c.String(maxLength: 255),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 256),
                        Result = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLoginAttempt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.UserId, t.TenantId })
                .Index(t => new { t.TenancyName, t.UserNameOrEmailAddress, t.Result });
            
            CreateTable(
                "dbo.AbpUserNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        TenantNotificationId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.UserId, t.State, t.CreationTime });
            
            CreateTable(
                "dbo.AbpUserOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransitPassItems", "UnitMeasureId", "dbo.RefUnitMeasures");
            DropForeignKey("dbo.TransitPassItems", "TransitPassId", "dbo.TransitPasses");
            DropForeignKey("dbo.TransitPassItems", "SpecieId", "dbo.Species");
            DropForeignKey("dbo.TransitPassItems", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.TallySheets", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.TallySheets", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles");
            DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserClaims", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Payments", "BillId", "dbo.Bills");
            DropForeignKey("dbo.AbpOrganizationUnits", "ParentId", "dbo.AbpOrganizationUnits");
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
            DropForeignKey("dbo.ExportSpecies", "SpecieId", "dbo.Species");
            DropForeignKey("dbo.Species", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.ExportSpecies", "ExportDetailId", "dbo.ExportDetails");
            DropForeignKey("dbo.ExportAttachments", "ExportDetailId", "dbo.ExportDetails");
            DropForeignKey("dbo.ExportDetails", "StationId", "dbo.Stations");
            DropForeignKey("dbo.ExportDetails", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.ExportDetails", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.ExportDetails", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.AbpFeatures", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.Districts", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.DealerActivities", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.DealerActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.CheckPointTransitPasses", "TransitPassId", "dbo.TransitPasses");
            DropForeignKey("dbo.CheckPointTransitPasses", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Candidates", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Candidates", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.BillTransitPasses", "TransitPassId", "dbo.TransitPasses");
            DropForeignKey("dbo.TransitPasses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.BillTransitPasses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Bills", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Bills", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.BillItems", "ActivityId", "dbo.Activities");
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
            DropIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "UserId", "TenantId" });
            DropIndex("dbo.TransitPassItems", new[] { "SpecieId" });
            DropIndex("dbo.TransitPassItems", new[] { "UnitMeasureId" });
            DropIndex("dbo.TransitPassItems", new[] { "ActivityId" });
            DropIndex("dbo.TransitPassItems", new[] { "TransitPassId" });
            DropIndex("dbo.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpTenants", new[] { "EditionId" });
            DropIndex("dbo.TallySheets", new[] { "SpecieCategoryId" });
            DropIndex("dbo.TallySheets", new[] { "PlotId" });
            DropIndex("dbo.AbpSettings", new[] { "UserId" });
            DropIndex("dbo.AbpUserRoles", new[] { "UserId" });
            DropIndex("dbo.AbpUserLogins", new[] { "UserId" });
            DropIndex("dbo.AbpUserClaims", new[] { "UserId" });
            DropIndex("dbo.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpPermissions", new[] { "UserId" });
            DropIndex("dbo.AbpPermissions", new[] { "RoleId" });
            DropIndex("dbo.Payments", new[] { "BillId" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "ParentId" });
            DropIndex("dbo.AbpNotificationSubscriptions", new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });
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
            DropIndex("dbo.Species", new[] { "SpecieCategoryId" });
            DropIndex("dbo.ExportSpecies", new[] { "SpecieId" });
            DropIndex("dbo.ExportSpecies", new[] { "ExportDetailId" });
            DropIndex("dbo.ExportDetails", new[] { "FinancialYearId" });
            DropIndex("dbo.ExportDetails", new[] { "StationId" });
            DropIndex("dbo.ExportDetails", new[] { "SpecieCategoryId" });
            DropIndex("dbo.ExportDetails", new[] { "ApplicantId" });
            DropIndex("dbo.ExportAttachments", new[] { "ExportDetailId" });
            DropIndex("dbo.AbpFeatures", new[] { "EditionId" });
            DropIndex("dbo.Districts", new[] { "RegionId" });
            DropIndex("dbo.DealerActivities", new[] { "ActivityId" });
            DropIndex("dbo.DealerActivities", new[] { "DealerId" });
            DropIndex("dbo.CheckPointTransitPasses", new[] { "StationId" });
            DropIndex("dbo.CheckPointTransitPasses", new[] { "TransitPassId" });
            DropIndex("dbo.Candidates", new[] { "FinancialYearId" });
            DropIndex("dbo.Candidates", new[] { "StationId" });
            DropIndex("dbo.TransitPasses", new[] { "BillId" });
            DropIndex("dbo.BillTransitPasses", new[] { "BillId" });
            DropIndex("dbo.BillTransitPasses", new[] { "TransitPassId" });
            DropIndex("dbo.Bills", new[] { "FinancialYearId" });
            DropIndex("dbo.Bills", new[] { "StationId" });
            DropIndex("dbo.Bills", new[] { "ApplicantId" });
            DropIndex("dbo.BillItems", new[] { "ActivityId" });
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
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
            DropTable("dbo.AbpUserOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserLoginAttempts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLoginAttempt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserAccounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.TransitPassItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TransitPassItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenantNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.AbpSettings",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Setting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserLogins",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLogin_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserClaims",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserClaim_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RefUnitMeasures",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefUnitMeasure_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.AbpPermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RolePermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserPermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Payments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Payment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotificationSubscriptions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotificationSubscriptionInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotifications");
            DropTable("dbo.LicenseCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LicenseCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguageTexts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.Species",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Specie_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExportSpecies",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExportSpecie_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SpecieCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExportDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExportDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExportAttachments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExportAttachment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEditions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpFeatures",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantFeatureSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.BillItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBackgroundJobs");
            DropTable("dbo.AbpAuditLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
