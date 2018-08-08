namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProviderResponse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProviderResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
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
            DropForeignKey("dbo.ProviderResponse", "Provider_Id", "dbo.Provider");
            DropForeignKey("dbo.ProviderResponse", "Order_Id", "dbo.Order");
            DropIndex("dbo.ProviderResponse", new[] { "Provider_Id" });
            DropIndex("dbo.ProviderResponse", new[] { "Order_Id" });
            DropTable("dbo.ProviderResponse");
        }
    }
}
