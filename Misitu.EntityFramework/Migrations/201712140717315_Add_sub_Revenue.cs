namespace Misitu.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_sub_Revenue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefSubRevenueSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RevenueResourceId = c.Int(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Royalty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaFF = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CESS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TREE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LMDA = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                    { "DynamicFilter_RefSubRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefSubRevenueSources",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RefSubRevenueSource_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
