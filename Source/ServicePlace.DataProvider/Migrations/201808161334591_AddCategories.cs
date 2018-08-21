namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProviderCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "OrderCategory_Id", c => c.Int());
            AddColumn("dbo.Provider", "ProviderCategory_Id", c => c.Int());
            CreateIndex("dbo.Order", "OrderCategory_Id");
            CreateIndex("dbo.Provider", "ProviderCategory_Id");
            AddForeignKey("dbo.Order", "OrderCategory_Id", "dbo.OrderCategory", "Id");
            AddForeignKey("dbo.Provider", "ProviderCategory_Id", "dbo.ProviderCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provider", "ProviderCategory_Id", "dbo.ProviderCategory");
            DropForeignKey("dbo.Order", "OrderCategory_Id", "dbo.OrderCategory");
            DropIndex("dbo.Provider", new[] { "ProviderCategory_Id" });
            DropIndex("dbo.Order", new[] { "OrderCategory_Id" });
            DropColumn("dbo.Provider", "ProviderCategory_Id");
            DropColumn("dbo.Order", "OrderCategory_Id");
            DropTable("dbo.ProviderCategory");
            DropTable("dbo.OrderCategory");
        }
    }
}
