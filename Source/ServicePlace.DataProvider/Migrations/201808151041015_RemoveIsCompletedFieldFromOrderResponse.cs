namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsCompletedFieldFromOrderResponse : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderResponse", "IsCompleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderResponse", "IsCompleted", c => c.Boolean(nullable: false));
        }
    }
}
