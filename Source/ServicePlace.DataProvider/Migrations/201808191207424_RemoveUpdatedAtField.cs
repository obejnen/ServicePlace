namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUpdatedAtField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "UpdatedAt");
            DropColumn("dbo.Provider", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Provider", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.Order", "UpdatedAt", c => c.DateTime());
        }
    }
}
