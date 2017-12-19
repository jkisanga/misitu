namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class HarvestingLogs_table : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HarvestingLogs", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.HarvestingLogs", "LicenseId", "dbo.Licenses");
            DropForeignKey("dbo.HarvestingLogs", "DealerId", "dbo.Dealers");
            DropIndex("dbo.HarvestingLogs", new[] { "LicenseId" });
            DropIndex("dbo.HarvestingLogs", new[] { "DealerId" });
            DropIndex("dbo.HarvestingLogs", new[] { "PlotId" });
            DropTable("dbo.HarvestingLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HarvestingLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
