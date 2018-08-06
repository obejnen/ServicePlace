namespace ServicePlace.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExecutorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Executor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        Creator_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id, cascadeDelete: true)
                .Index(t => t.Creator_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Executor", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Executor", new[] { "Creator_Id" });
            DropTable("dbo.Executor");
        }
    }
}
