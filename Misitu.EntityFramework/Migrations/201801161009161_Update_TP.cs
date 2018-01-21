namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransitPasses", "BillId", c => c.Int(nullable: false));
            AlterColumn("dbo.TransitPasses", "LisenceNo", c => c.String());
            CreateIndex("dbo.TransitPasses", "BillId");
            AddForeignKey("dbo.TransitPasses", "BillId", "dbo.Bills", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransitPasses", "BillId", "dbo.Bills");
            DropIndex("dbo.TransitPasses", new[] { "BillId" });
            AlterColumn("dbo.TransitPasses", "LisenceNo", c => c.Int(nullable: false));
            DropColumn("dbo.TransitPasses", "BillId");
        }
    }
}
