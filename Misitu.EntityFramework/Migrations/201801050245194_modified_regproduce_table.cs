namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified_regproduce_table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForestProduceRegistrations", "RegNumber", c => c.String());
            AlterColumn("dbo.ForestProduceRegistrations", "SawmillInstalledLocation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ForestProduceRegistrations", "SawmillInstalledLocation", c => c.Int(nullable: false));
            AlterColumn("dbo.ForestProduceRegistrations", "RegNumber", c => c.Int(nullable: false));
        }
    }
}
