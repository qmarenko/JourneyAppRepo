using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{
    public class GitHubIssue : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string IssueUrl { get; set; }
        [MaxLength(100)]
        public string UserAdded { get; set; }
        public string Body { get; set; }
        public DateTime CreateTime { get; set; }
    }
}