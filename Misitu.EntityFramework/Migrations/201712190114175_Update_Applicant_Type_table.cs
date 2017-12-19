namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Applicant_Type_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
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
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefApplicantType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
          
           
        }
        
        public override void Down()
        {
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
