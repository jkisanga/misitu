namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_aplicant_id_UsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "ApplicantId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "ApplicantId");
        }
    }
}
