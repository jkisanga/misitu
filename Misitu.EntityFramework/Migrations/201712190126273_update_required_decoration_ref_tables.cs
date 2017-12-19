namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_required_decoration_ref_tables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefServiceCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.RefServiceCategories", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.RefIdentityTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefIdentityTypes", "Name", c => c.String());
            AlterColumn("dbo.RefServiceCategories", "Description", c => c.String());
            AlterColumn("dbo.RefServiceCategories", "Name", c => c.String());
        }
    }
}
