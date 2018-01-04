namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class add_ForestProduceAppliedSpecieCategory_table : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForestProduceAppliedForests", "StationId", "dbo.Stations");
            DropForeignKey("dbo.ForestProduceAppliedForests", "ForestProduceRegistrationId", "dbo.ForestProduceRegistrations");
            DropForeignKey("dbo.ForestProduceAppliedForests", "FinancialYearId", "dbo.FinancialYears");
            DropIndex("dbo.ForestProduceAppliedForests", new[] { "FinancialYearId" });
            DropIndex("dbo.ForestProduceAppliedForests", new[] { "StationId" });
            DropIndex("dbo.ForestProduceAppliedForests", new[] { "ForestProduceRegistrationId" });
            DropTable("dbo.ForestProduceAppliedForests",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ForestProduceAppliedForest_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
