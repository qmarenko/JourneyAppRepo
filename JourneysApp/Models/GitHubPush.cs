using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{
    public class GitHubPush :BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string @Ref { get; set; }
        [MaxLength(100)]
        public string Before { get; set; }
        [MaxLength(100)]
        public string After { get; set; }
        [MaxLength(70)]
        public string PusherName { get; set; }
        [MaxLength(150)]
        public string PusherEmail { get; set; }
        public virtual ICollection<GitHubCommit> Commits { get; set; }
    }
}