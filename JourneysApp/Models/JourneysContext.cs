using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{
    public class JourneysContext : DbContext
    {
        public JourneysContext()
            : base("ProdConnection")
        {
        }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<GitHubPush> GitHubPushes { get; set; }
        public DbSet<GitHubCommit> GitHubCommits { get; set; }
        public DbSet<GitHubIssue> GitHubIssues { get; set; }
        public override int SaveChanges()
        {
            AddAuditInfo();

            return base.SaveChanges();
        }

        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            var userName = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                            ? System.Web.HttpContext.Current.User.Identity.Name
                            : "Unknown";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreatedUser = userName;
                }

                ((BaseEntity)entity.Entity).ModifiedDate = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).ModifiedUser = userName;

            }
        }
    }
}