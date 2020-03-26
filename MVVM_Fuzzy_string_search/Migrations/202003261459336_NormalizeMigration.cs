namespace MVVM_Fuzzy_string_search.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NormalizeMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RequestResults", "SourceId", c => c.Int(nullable: false));
            CreateIndex("dbo.RequestResults", "SourceId");
            AddForeignKey("dbo.RequestResults", "SourceId", "dbo.Sources", "Id", cascadeDelete: true);
            DropColumn("dbo.RequestResults", "Source");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestResults", "Source", c => c.String());
            DropForeignKey("dbo.RequestResults", "SourceId", "dbo.Sources");
            DropIndex("dbo.RequestResults", new[] { "SourceId" });
            DropColumn("dbo.RequestResults", "SourceId");
            DropTable("dbo.Sources");
        }
    }
}
