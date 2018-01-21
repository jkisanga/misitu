namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ExportTables : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExportSpecies", "SpecieId", "dbo.Species");
            DropForeignKey("dbo.ExportSpecies", "ExportDetailId", "dbo.ExportDetails");
            DropForeignKey("dbo.ExportAttachments", "ExportDetailId", "dbo.ExportDetails");
            DropForeignKey("dbo.ExportDetails", "StationId", "dbo.Stations");
            DropForeignKey("dbo.ExportDetails", "SpecieCategoryId", "dbo.SpecieCategories");
            DropForeignKey("dbo.ExportDetails", "FinancialYearId", "dbo.FinancialYears");
            DropForeignKey("dbo.ExportDetails", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.ExportSpecies", new[] { "SpecieId" });
            DropIndex("dbo.ExportSpecies", new[] { "ExportDetailId" });
            DropIndex("dbo.ExportDetails", new[] { "FinancialYearId" });
            DropIndex("dbo.ExportDetails", new[] { "StationId" });
            DropIndex("dbo.ExportDetails", new[] { "SpecieCategoryId" });
            DropIndex("dbo.ExportDetails", new[] { "ApplicantId" });
            DropIndex("dbo.ExportAttachments", new[] { "ExportDetailId" });
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
            DropTable("dbo.ExportAttachments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExportAttachment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
