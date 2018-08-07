namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderResponseModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Comment = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Order_Id = c.Int(nullable: false),
                        Provider_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.Provider", t => t.Provider_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Provider_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderResponse", "Provider_Id", "dbo.Provider");
            DropForeignKey("dbo.OrderResponse", "Order_Id", "dbo.Order");
            DropIndex("dbo.OrderResponse", new[] { "Provider_Id" });
            DropIndex("dbo.OrderResponse", new[] { "Order_Id" });
            DropTable("dbo.OrderResponse");
        }
    }
}
