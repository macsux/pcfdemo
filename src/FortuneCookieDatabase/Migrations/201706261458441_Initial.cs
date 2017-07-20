namespace FortuneCookieDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FortuneCookies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cookie = c.String(unicode: false),
                        Language = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FortuneCookies");
        }
    }
}
