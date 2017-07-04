namespace JourneysApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseEntityToJourneysAndIsDeletedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journeys", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Journeys", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Journeys", "CreatedUser", c => c.String(maxLength: 70));
            AddColumn("dbo.Journeys", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Journeys", "ModifiedUser", c => c.String(maxLength: 70));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journeys", "ModifiedUser");
            DropColumn("dbo.Journeys", "ModifiedDate");
            DropColumn("dbo.Journeys", "CreatedUser");
            DropColumn("dbo.Journeys", "CreatedDate");
            DropColumn("dbo.Journeys", "IsDeleted");
        }
    }
}
