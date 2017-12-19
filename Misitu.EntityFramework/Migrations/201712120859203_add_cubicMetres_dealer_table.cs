namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_cubicMetres_dealer_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dealers", "AllocatedCubicMetres", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dealers", "AllocatedCubicMetres");
        }
    }
}
