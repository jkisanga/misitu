namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class is_denied_application : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "IsDenied", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dealers", "IsDenied");
        }
    }
}
