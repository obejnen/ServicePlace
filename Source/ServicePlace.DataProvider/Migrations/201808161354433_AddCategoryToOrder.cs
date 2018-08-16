namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryToOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "OrderCategory_Id", "dbo.OrderCategory");
            DropIndex("dbo.Order", new[] { "OrderCategory_Id" });
            RenameColumn(table: "dbo.Order", name: "OrderCategory_Id", newName: "Category_Id");
            AlterColumn("dbo.Order", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "Category_Id");
            AddForeignKey("dbo.Order", "Category_Id", "dbo.OrderCategory", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Category_Id", "dbo.OrderCategory");
            DropIndex("dbo.Order", new[] { "Category_Id" });
            AlterColumn("dbo.Order", "Category_Id", c => c.Int());
            RenameColumn(table: "dbo.Order", name: "Category_Id", newName: "OrderCategory_Id");
            CreateIndex("dbo.Order", "OrderCategory_Id");
            AddForeignKey("dbo.Order", "OrderCategory_Id", "dbo.OrderCategory", "Id");
        }
    }
}
