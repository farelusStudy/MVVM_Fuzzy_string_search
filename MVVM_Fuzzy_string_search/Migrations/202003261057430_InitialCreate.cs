namespace MVVM_Fuzzy_string_search.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                        Url = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RequestResults");
        }
    }
}
