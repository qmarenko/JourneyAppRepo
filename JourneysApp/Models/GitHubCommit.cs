using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{
    public class GitHubCommit :BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string CommitComment { get; set; }
        [MaxLength(100)]
        public string AuthorName { get; set; }
        [MaxLength(100)]
        public string AuthorEmail { get; set; }
        [ForeignKey("GitHubPush")]
        public virtual int GitHubPushId { get; set; }
        public virtual GitHubPush GitHubPush {get; set; }
    }
}