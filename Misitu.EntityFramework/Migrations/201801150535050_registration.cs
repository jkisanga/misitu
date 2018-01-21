namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "PrintedUserId", c => c.Int());
            AddColumn("dbo.Dealers", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Dealers", "ApprovedUserId", c => c.Int());
            AddColumn("dbo.Dealers", "Remark", c => c.String());
            DropColumn("dbo.Dealers", "PrintedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dealers", "PrintedBy", c => c.String());
            DropColumn("dbo.Dealers", "Remark");
            DropColumn("dbo.Dealers", "ApprovedUserId");
            DropColumn("dbo.Dealers", "IsApproved");
            DropColumn("dbo.Dealers", "PrintedUserId");
        }
    }
}
