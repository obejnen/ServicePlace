namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatorToOrderAndProviderResponses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderResponse", "Creator_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ProviderResponse", "Creator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderResponse", "Creator_Id");
            CreateIndex("dbo.ProviderResponse", "Creator_Id");
            AddForeignKey("dbo.OrderResponse", "Creator_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ProviderResponse", "Creator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProviderResponse", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderResponse", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ProviderResponse", new[] { "Creator_Id" });
            DropIndex("dbo.OrderResponse", new[] { "Creator_Id" });
            DropColumn("dbo.ProviderResponse", "Creator_Id");
            DropColumn("dbo.OrderResponse", "Creator_Id");
        }
    }
}
