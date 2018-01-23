namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class BillItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        EquvAmont = c.Double(nullable: false),
                        MiscAmont = c.Double(nullable: false),
                        GfsCode = c.Int(nullable: false),
                        Loyality = c.Double(nullable: false),
                        TFF = c.Double(nullable: false),
                        LMDA = c.Double(nullable: false),
                        VAT = c.Double(nullable: false),
                        CESS = c.Double(nullable: false),
                        TP = c.Double(nullable: false),
                        DataSheet = c.Double(nullable: false),
                        Others = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
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
                    { "DynamicFilter_BillItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.BillItems", "ActivityId", "dbo.Activities");
            DropIndex("dbo.BillItems", new[] { "ActivityId" });
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropTable("dbo.BillItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BillItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
