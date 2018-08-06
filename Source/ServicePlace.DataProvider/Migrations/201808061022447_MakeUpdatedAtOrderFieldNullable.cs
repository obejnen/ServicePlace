namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUpdatedAtOrderFieldNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
