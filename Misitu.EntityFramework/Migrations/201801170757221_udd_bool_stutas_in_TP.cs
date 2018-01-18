namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class udd_bool_stutas_in_TP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransitPasses", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransitPasses", "Status");
        }
    }
}
