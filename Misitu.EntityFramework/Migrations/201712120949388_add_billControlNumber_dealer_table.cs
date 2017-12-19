namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_billControlNumber_dealer_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "BillControlNumber", c => c.String());
            DropColumn("dbo.Dealers", "ReceiptNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dealers", "ReceiptNumber", c => c.String());
            DropColumn("dbo.Dealers", "BillControlNumber");
        }
    }
}
