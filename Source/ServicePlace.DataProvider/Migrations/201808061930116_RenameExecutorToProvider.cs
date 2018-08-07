namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameExecutorToProvider : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Executor", newName: "Provider");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Provider", newName: "Executor");
        }
    }
}
