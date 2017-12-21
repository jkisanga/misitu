namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Updates_From_Victor : DbMigration
    {
        public override void Up()
        {


        }
        
        public override void Down()
        {
            AddColumn("dbo.Dealers", "ReceiptNumber", c => c.String());
            DropForeignKey("dbo.HarvestingLogs", "PlotId", "dbo.Plots");
            DropForeignKey("dbo.HarvestingLogs", "LicenseId", "dbo.Licenses");
            DropForeignKey("dbo.HarvestingLogs", "DealerId", "dbo.Dealers");
            DropIndex("dbo.HarvestingLogs", new[] { "LicenseId" });
            DropIndex("dbo.HarvestingLogs", new[] { "DealerId" });
            DropIndex("dbo.HarvestingLogs", new[] { "PlotId" });
            DropColumn("dbo.AbpUsers", "ApplicantId");
            DropColumn("dbo.Licenses", "ExpiredDate");
            DropColumn("dbo.Dealers", "AllocatedCubicMetres");
            DropColumn("dbo.Dealers", "BillControlNumber");
            DropTable("dbo.HarvestingLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HarvestingLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
