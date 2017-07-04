namespace JourneysApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGitHubPushAndCommitTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GitHubCommits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommitComment = c.String(maxLength: 255),
                        AuthorName = c.String(maxLength: 100),
                        AuthorEmail = c.String(maxLength: 100),
                        GitHubPushId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GitHubPushes", t => t.GitHubPushId, cascadeDelete: false)
                .Index(t => t.GitHubPushId);
            
            CreateTable(
                "dbo.GitHubPushes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ref = c.String(maxLength: 100),
                        Before = c.String(maxLength: 100),
                        After = c.String(maxLength: 100),
                        PusherName = c.String(maxLength: 70),
                        PusherEmail = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GitHubCommits", "GitHubPushId", "dbo.GitHubPushes");
            DropIndex("dbo.GitHubCommits", new[] { "GitHubPushId" });
            DropTable("dbo.GitHubPushes");
            DropTable("dbo.GitHubCommits");
        }
    }
}
