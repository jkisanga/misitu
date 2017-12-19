namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_expiredate_LicenceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Licenses", "ExpiredDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Licenses", "ExpiredDate");
        }
    }
}
