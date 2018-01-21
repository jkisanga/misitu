namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class is_reg_submitted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "IsSubmitted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dealers", "IsSubmitted");
        }
    }
}
