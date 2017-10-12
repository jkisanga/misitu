namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Activity_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "RegistrationFee", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "RegistrationFee");
        }
    }
}
