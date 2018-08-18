namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatarToProvider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avatar_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Avatar_Id");
            AddForeignKey("dbo.AspNetUsers", "Avatar_Id", "dbo.Image", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Avatar_Id", "dbo.Image");
            DropIndex("dbo.AspNetUsers", new[] { "Avatar_Id" });
            DropColumn("dbo.AspNetUsers", "Avatar_Id");
        }
    }
}
