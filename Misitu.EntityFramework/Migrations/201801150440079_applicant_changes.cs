namespace Misitu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applicant_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "IsTanzanian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Applicants", "IDtype", c => c.String());
            AddColumn("dbo.Applicants", "IDNumber", c => c.String());
            AddColumn("dbo.Applicants", "IDIssuePlace", c => c.String());
            AddColumn("dbo.Applicants", "IDExpiryDate", c => c.DateTime());
            DropColumn("dbo.Applicants", "BusinessLicenceNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applicants", "BusinessLicenceNo", c => c.String());
            DropColumn("dbo.Applicants", "IDExpiryDate");
            DropColumn("dbo.Applicants", "IDIssuePlace");
            DropColumn("dbo.Applicants", "IDNumber");
            DropColumn("dbo.Applicants", "IDtype");
            DropColumn("dbo.Applicants", "IsTanzanian");
        }
    }
}
