namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesToProvider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "Provider_Id", c => c.Int());
            CreateIndex("dbo.Image", "Provider_Id");
            AddForeignKey("dbo.Image", "Provider_Id", "dbo.Provider", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Image", "Provider_Id", "dbo.Provider");
            DropIndex("dbo.Image", new[] { "Provider_Id" });
            DropColumn("dbo.Image", "Provider_Id");
        }
    }
}
