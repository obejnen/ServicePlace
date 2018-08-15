namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompletedFieldToOrderResponse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderResponse", "Completed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderResponse", "Completed");
        }
    }
}
