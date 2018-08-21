namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApprovedFieldToOrdersAndProviders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Provider", "Approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Provider", "Approved");
            DropColumn("dbo.Order", "Approved");
        }
    }
}
