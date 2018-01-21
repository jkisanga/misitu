namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bill_update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.BillItems", "RevenueResourceId", "dbo.RevenueSources");
            DropIndex("dbo.BillItems", new[] { "RevenueResourceId" });
            DropIndex("dbo.Bills", new[] { "DealerId" });
            AddColumn("dbo.BillItems", "ActivityId", c => c.Int(nullable: false));
            AddColumn("dbo.BillItems", "EquvAmont", c => c.Double(nullable: false));
            AddColumn("dbo.BillItems", "MiscAmont", c => c.Double(nullable: false));
            AddColumn("dbo.BillItems", "GfsCode", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "ApplicantId", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "EquvAmont", c => c.Double(nullable: false));
            AddColumn("dbo.Bills", "MiscAmont", c => c.Double(nullable: false));
            CreateIndex("dbo.BillItems", "ActivityId");
            CreateIndex("dbo.Bills", "ApplicantId");
            AddForeignKey("dbo.BillItems", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "ApplicantId", "dbo.Applicants", "Id", cascadeDelete: true);
            DropColumn("dbo.BillItems", "RevenueResourceId");
            DropColumn("dbo.Bills", "DealerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "DealerId", c => c.Int(nullable: false));
            AddColumn("dbo.BillItems", "RevenueResourceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bills", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.BillItems", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Bills", new[] { "ApplicantId" });
            DropIndex("dbo.BillItems", new[] { "ActivityId" });
            DropColumn("dbo.Bills", "MiscAmont");
            DropColumn("dbo.Bills", "EquvAmont");
            DropColumn("dbo.Bills", "ApplicantId");
            DropColumn("dbo.BillItems", "GfsCode");
            DropColumn("dbo.BillItems", "MiscAmont");
            DropColumn("dbo.BillItems", "EquvAmont");
            DropColumn("dbo.BillItems", "ActivityId");
            CreateIndex("dbo.Bills", "DealerId");
            CreateIndex("dbo.BillItems", "RevenueResourceId");
            AddForeignKey("dbo.BillItems", "RevenueResourceId", "dbo.RevenueSources", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "DealerId", "dbo.Dealers", "Id", cascadeDelete: true);
        }
    }
}
