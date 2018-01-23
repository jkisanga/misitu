namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tp_expire_days : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransitPasses", "ExpireDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransitPasses", "ExpireDays");
        }
    }
}
