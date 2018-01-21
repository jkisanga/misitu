namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class major_updates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "RefServiceCategoryId", "dbo.RefServiceCategories");
            DropIndex("dbo.Activities", new[] { "RefServiceCategoryId" });
            AddColumn("dbo.Dealers", "ApplicantId", c => c.Int(nullable: false));
            AddColumn("dbo.Applicants", "TIN", c => c.String());
            AddColumn("dbo.Applicants", "BusinessLicenceNo", c => c.String());
            CreateIndex("dbo.Dealers", "ApplicantId");
            AddForeignKey("dbo.Dealers", "ApplicantId", "dbo.Applicants", "Id");
            DropColumn("dbo.Dealers", "Name");
            DropColumn("dbo.Dealers", "Address");
            DropColumn("dbo.Dealers", "Email");
            DropColumn("dbo.Dealers", "Phone");
            DropColumn("dbo.Dealers", "RegisteredDate");
            DropColumn("dbo.Dealers", "TIN");
            DropColumn("dbo.Dealers", "BusinessLicense");
            DropColumn("dbo.Dealers", "PaymentReferenceNumber");
            DropColumn("dbo.Dealers", "AllocatedCubicMetres");
            DropColumn("dbo.Applicants", "IsTanzanian");
            DropColumn("dbo.Applicants", "IDtype");
            DropColumn("dbo.Applicants", "IDNumber");
            DropColumn("dbo.Applicants", "IDIssuePlace");
            DropColumn("dbo.Applicants", "IDExpiryDate");
            DropTable("dbo.RefServiceCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefServiceCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RefServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefServiceCategory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Applicants", "IDExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Applicants", "IDIssuePlace", c => c.String());
            AddColumn("dbo.Applicants", "IDNumber", c => c.String(nullable: false));
            AddColumn("dbo.Applicants", "IDtype", c => c.String(nullable: false));
            AddColumn("dbo.Applicants", "IsTanzanian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Dealers", "AllocatedCubicMetres", c => c.Double(nullable: false));
            AddColumn("dbo.Dealers", "PaymentReferenceNumber", c => c.String());
            AddColumn("dbo.Dealers", "BusinessLicense", c => c.String());
            AddColumn("dbo.Dealers", "TIN", c => c.String());
            AddColumn("dbo.Dealers", "RegisteredDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dealers", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Dealers", "Email", c => c.String());
            AddColumn("dbo.Dealers", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Dealers", "Name", c => c.String(nullable: false));
            DropForeignKey("dbo.Dealers", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Dealers", new[] { "ApplicantId" });
            DropColumn("dbo.Applicants", "BusinessLicenceNo");
            DropColumn("dbo.Applicants", "TIN");
            DropColumn("dbo.Dealers", "ApplicantId");
            CreateIndex("dbo.Activities", "RefServiceCategoryId");
            AddForeignKey("dbo.Activities", "RefServiceCategoryId", "dbo.RefServiceCategories", "Id", cascadeDelete: true);
        }
    }
}
