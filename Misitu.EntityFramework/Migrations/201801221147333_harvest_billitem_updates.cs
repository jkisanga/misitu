namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class harvest_billitem_updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillItems", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.ForestProduceRegistrations", "CertificatePrinted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ForestProduceRegistrations", "IsSubmitted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ForestProduceRegistrations", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.ForestProduceRegistrations", "IsRejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.ForestProduceRegistrations", "ApprovedUserId", c => c.Int());
            AddColumn("dbo.ForestProduceRegistrations", "Remark", c => c.String());
            AlterColumn("dbo.ForestProduceRegistrations", "Status", c => c.String());
            DropColumn("dbo.ForestProduceRegistrations", "CertificatePrented");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForestProduceRegistrations", "CertificatePrented", c => c.Int(nullable: false));
            AlterColumn("dbo.ForestProduceRegistrations", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.ForestProduceRegistrations", "Remark");
            DropColumn("dbo.ForestProduceRegistrations", "ApprovedUserId");
            DropColumn("dbo.ForestProduceRegistrations", "IsRejected");
            DropColumn("dbo.ForestProduceRegistrations", "IsApproved");
            DropColumn("dbo.ForestProduceRegistrations", "IsSubmitted");
            DropColumn("dbo.ForestProduceRegistrations", "CertificatePrinted");
            DropColumn("dbo.BillItems", "Quantity");
        }
    }
}
