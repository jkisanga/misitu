namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tp_and_tpitems_fields_change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransitPasses", "RegistrationNo", c => c.String());
            AddColumn("dbo.TransitPasses", "StationId", c => c.Int(nullable: false));
            AddColumn("dbo.TransitPasses", "DistrictId", c => c.Int(nullable: false));
            AddColumn("dbo.TransitPassItems", "Size", c => c.String());
            DropColumn("dbo.TransitPasses", "SourceForest");
            DropColumn("dbo.TransitPasses", "DestinationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransitPasses", "DestinationId", c => c.Int(nullable: false));
            AddColumn("dbo.TransitPasses", "SourceForest", c => c.Int(nullable: false));
            DropColumn("dbo.TransitPassItems", "Size");
            DropColumn("dbo.TransitPasses", "DistrictId");
            DropColumn("dbo.TransitPasses", "StationId");
            DropColumn("dbo.TransitPasses", "RegistrationNo");
        }
    }
}
