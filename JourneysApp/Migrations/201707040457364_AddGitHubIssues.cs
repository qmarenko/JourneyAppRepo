namespace JourneysApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGitHubIssues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GitHubIssues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        IssueUrl = c.String(maxLength: 255),
                        UserAdded = c.String(maxLength: 100),
                        Body = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GitHubIssues");
        }
    }
}
