namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "Order_Id", c => c.Int());
            CreateIndex("dbo.Image", "Order_Id");
            AddForeignKey("dbo.Image", "Order_Id", "dbo.Order", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Image", "Order_Id", "dbo.Order");
            DropIndex("dbo.Image", new[] { "Order_Id" });
            DropColumn("dbo.Image", "Order_Id");
        }
    }
}
