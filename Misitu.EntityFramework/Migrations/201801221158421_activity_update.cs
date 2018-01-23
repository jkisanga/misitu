namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activity_update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Name", c => c.String());
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String());
            AlterColumn("dbo.Activities", "Name", c => c.String(nullable: false));
        }
    }
}
