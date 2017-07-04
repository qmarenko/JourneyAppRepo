namespace JourneysApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Destination = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 500),
                        UserLogin = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Journeys");
        }
    }
}
