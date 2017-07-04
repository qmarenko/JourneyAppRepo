namespace JourneysApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseEntityToGitHubEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GitHubCommits", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GitHubCommits", "CreatedUser", c => c.String(maxLength: 70));
            AddColumn("dbo.GitHubCommits", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GitHubCommits", "ModifiedUser", c => c.String(maxLength: 70));
            AddColumn("dbo.GitHubPushes", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GitHubPushes", "CreatedUser", c => c.String(maxLength: 70));
            AddColumn("dbo.GitHubPushes", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GitHubPushes", "ModifiedUser", c => c.String(maxLength: 70));
            AddColumn("dbo.GitHubIssues", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GitHubIssues", "CreatedUser", c => c.String(maxLength: 70));
            AddColumn("dbo.GitHubIssues", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GitHubIssues", "ModifiedUser", c => c.String(maxLength: 70));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GitHubIssues", "ModifiedUser");
            DropColumn("dbo.GitHubIssues", "ModifiedDate");
            DropColumn("dbo.GitHubIssues", "CreatedUser");
            DropColumn("dbo.GitHubIssues", "CreatedDate");
            DropColumn("dbo.GitHubPushes", "ModifiedUser");
            DropColumn("dbo.GitHubPushes", "ModifiedDate");
            DropColumn("dbo.GitHubPushes", "CreatedUser");
            DropColumn("dbo.GitHubPushes", "CreatedDate");
            DropColumn("dbo.GitHubCommits", "ModifiedUser");
            DropColumn("dbo.GitHubCommits", "ModifiedDate");
            DropColumn("dbo.GitHubCommits", "CreatedUser");
            DropColumn("dbo.GitHubCommits", "CreatedDate");
        }
    }
}
