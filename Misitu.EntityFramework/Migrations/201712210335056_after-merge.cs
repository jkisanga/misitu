namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class aftermerge : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.RefApplicantTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_RefApplicantType_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.RefApplicantTypes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.RefApplicantTypes", "DeleterUserId", c => c.Long());
            AddColumn("dbo.RefApplicantTypes", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.RefApplicantTypes", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.RefApplicantTypes", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.RefApplicantTypes", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RefApplicantTypes", "CreatorUserId", c => c.Long());
            AlterColumn("dbo.RefServiceCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.RefServiceCategories", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.RefIdentityTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefIdentityTypes", "Name", c => c.String());
            AlterColumn("dbo.RefServiceCategories", "Description", c => c.String());
            AlterColumn("dbo.RefServiceCategories", "Name", c => c.String());
            DropColumn("dbo.RefApplicantTypes", "CreatorUserId");
            DropColumn("dbo.RefApplicantTypes", "CreationTime");
            DropColumn("dbo.RefApplicantTypes", "LastModifierUserId");
            DropColumn("dbo.RefApplicantTypes", "LastModificationTime");
            DropColumn("dbo.RefApplicantTypes", "DeletionTime");
            DropColumn("dbo.RefApplicantTypes", "DeleterUserId");
            DropColumn("dbo.RefApplicantTypes", "IsDeleted");
            AlterTableAnnotations(
                "dbo.RefApplicantTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_RefApplicantType_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
