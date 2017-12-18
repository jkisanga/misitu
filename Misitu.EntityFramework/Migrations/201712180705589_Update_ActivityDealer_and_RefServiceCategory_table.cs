namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ActivityDealer_and_RefServiceCategory_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DealerActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DealerId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
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
                    { "DynamicFilter_DealerActivity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Dealers", t => t.DealerId, cascadeDelete: true)
                .Index(t => t.DealerId)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DealerActivities", "DealerId", "dbo.Dealers");
            DropForeignKey("dbo.DealerActivities", "ActivityId", "dbo.Activities");
            DropIndex("dbo.DealerActivities", new[] { "ActivityId" });
            DropIndex("dbo.DealerActivities", new[] { "DealerId" });
            DropTable("dbo.DealerActivities",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DealerActivity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
