namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Activity_Bill_BillItem_Add_Payment : DbMigration
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
                "dbo.BillItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
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
                        EquvAmont = c.Double(nullable: false),
                        MiscAmont = c.Double(nullable: false),
                        GfsCode = c.Int(nullable: false),
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
                        ExpiredDate = c.DateTime(nullable: false),
                        ControlNumber = c.String(),
                        BillAmount = c.Double(nullable: false),
                        PaidAmount = c.Double(nullable: false),
                        EquvAmont = c.Double(nullable: false),
                        MiscAmont = c.Double(nullable: false),
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
            
                   }
        
        public override void Down()
        {
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
            DropForeignKey("dbo.TransitPasses", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.BillTransitPasses", "BillId", "dbo.Bills");
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "StationId", "dbo.Stations");
            DropForeignKey("dbo.Bills", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Bills", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.BillItems", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Applicants", "FinancialYearId", "dbo.FinancialYears");
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
            DropForeignKey("dbo.Activities", "RevenueSourceId", "dbo.RevenueSources");
            DropForeignKey("dbo.RevenueSources", "MainRevenueSourceId", "dbo.MainRevenueSources");
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
            DropIndex("dbo.TransitPasses", new[] { "ApplicantId" });
            DropIndex("dbo.BillTransitPasses", new[] { "BillId" });
            DropIndex("dbo.BillTransitPasses", new[] { "TransitPassId" });
            DropIndex("dbo.Bills", new[] { "FinancialYearId" });
            DropIndex("dbo.Bills", new[] { "StationId" });
            DropIndex("dbo.Bills", new[] { "ApplicantId" });
            DropIndex("dbo.BillItems", new[] { "ActivityId" });
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropIndex("dbo.Applicants", new[] { "FinancialYearId" });
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
            DropIndex("dbo.RevenueSources", new[] { "MainRevenueSourceId" });
            DropIndex("dbo.Activities", new[] { "RevenueSourceId" });
            DropColumn("dbo.AbpUsers", "ApplicantId");
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
            DropTable("dbo.RefSubRevenueSources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefSubRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RefServiceCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefServiceCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropTable("dbo.BillItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Applicants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Applicant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
