namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClosedFieldToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Closed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Closed");
        }
    }
}
