namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryToProvider : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Provider", "ProviderCategory_Id", "dbo.ProviderCategory");
            DropIndex("dbo.Provider", new[] { "ProviderCategory_Id" });
            RenameColumn(table: "dbo.Provider", name: "ProviderCategory_Id", newName: "Category_Id");
            AlterColumn("dbo.Provider", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Provider", "Category_Id");
            AddForeignKey("dbo.Provider", "Category_Id", "dbo.ProviderCategory", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provider", "Category_Id", "dbo.ProviderCategory");
            DropIndex("dbo.Provider", new[] { "Category_Id" });
            AlterColumn("dbo.Provider", "Category_Id", c => c.Int());
            RenameColumn(table: "dbo.Provider", name: "Category_Id", newName: "ProviderCategory_Id");
            CreateIndex("dbo.Provider", "ProviderCategory_Id");
            AddForeignKey("dbo.Provider", "ProviderCategory_Id", "dbo.ProviderCategory", "Id");
        }
    }
}
