namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_online_applicant_tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DealerActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.DealerActivities", "DealerId", "dbo.Dealers");
            DropIndex("dbo.DealerActivities", new[] { "DealerId" });
            DropIndex("dbo.DealerActivities", new[] { "ActivityId" });
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
                        IsTanzanian = c.Boolean(nullable: false),
                        IDtype = c.String(nullable: false),
                        IDNumber = c.String(nullable: false),
                        IDIssuePlace = c.String(),
                        IDExpiryDate = c.DateTime(nullable: false),
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
                    { "DynamicFilter_Applicant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FinancialYears", t => t.FinancialYearId, cascadeDelete: true)
                .Index(t => t.FinancialYearId);
            
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
                        RegNumber = c.Int(nullable: false),
                        HasSawmill = c.Boolean(nullable: false),
                        IsSawmillInstalled = c.Boolean(nullable: false),
                        SawmillInstalledLocation = c.Int(nullable: false),
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
                "dbo.RefApplicantTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefIdentityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            
          
           
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Fee = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ForestProduceAppliedSpecieCategories", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.ForestProduceAppliedSpecieCategories", "ForestProduceRegistrationId", "dbo.ForestProduceRegistrations");
            DropForeignKey("dbo.ForestProduceRegistrations", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.ForestProduceRegistrations", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.ForestProduceRegistrations", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.ForestProduceAppliedSpecieCategories", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.Districts", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Applicants", "FinancialYearId", "dbo.FinancialYears");
            DropIndex("dbo.ForestProduceRegistrations", new[] { "DistrictId" });
            DropIndex("dbo.ForestProduceRegistrations", new[] { "FinancialYearId" });
            DropIndex("dbo.ForestProduceRegistrations", new[] { "ApplicantId" });
            DropIndex("dbo.ForestProduceAppliedSpecieCategories", new[] { "FinancialYearId" });
            DropIndex("dbo.ForestProduceAppliedSpecieCategories", new[] { "SpecieCategoryId" });
            DropIndex("dbo.ForestProduceAppliedSpecieCategories", new[] { "ForestProduceRegistrationId" });
            DropIndex("dbo.Districts", new[] { "RegionId" });
            DropIndex("dbo.Applicants", new[] { "FinancialYearId" });
            DropTable("dbo.RefIdentityTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefIdentityType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.RefApplicantTypes");
            DropTable("dbo.ForestProduceRegistrations",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ForestProduceRegistration_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ForestProduceAppliedSpecieCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ForestProduceAppliedSpecieCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Districts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_District_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Applicants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Applicant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("dbo.DealerActivities", "ActivityId");
            CreateIndex("dbo.DealerActivities", "DealerId");
            AddForeignKey("dbo.DealerActivities", "DealerId", "dbo.Dealers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DealerActivities", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
    }
}
