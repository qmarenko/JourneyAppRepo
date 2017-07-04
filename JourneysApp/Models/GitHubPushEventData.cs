using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JourneysApp.Models
{

    public class GitHubPushEventData
    {
        public string @ref { get; set; }
        public string before { get; set; }
        public string after { get; set; }
        public bool created { get; set; }
        public bool deleted { get; set; }
        public bool forced { get; set; }
        public object base_ref { get; set; }
        public string compare { get; set; }
        public List<Commit> commits { get; set; }
        public HeadCommit head_commit { get; set; }
        public Repository repository { get; set; }
        public Pusher pusher { get; set; }
        public Sender sender { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class Committer
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class Commit
    {
        public string id { get; set; }
        public string tree_id { get; set; }
        public bool distinct { get; set; }
        public string message { get; set; }
        public string timestamp { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
        public Committer committer { get; set; }
        public List<object> added { get; set; }
        public List<object> removed { get; set; }
        public List<string> modified { get; set; }
    }

    public class Author2
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class Committer2
    {
        public string name { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    public class HeadCommit
    {
        public string id { get; set; }
        public string tree_id { get; set; }
        public bool distinct { get; set; }
        public string message { get; set; }
        public string timestamp { get; set; }
        public string url { get; set; }
        public Author2 author { get; set; }
        public Committer2 committer { get; set; }
        public List<object> added { get; set; }
        public List<object> removed { get; set; }
        public List<string> modified { get; set; }
    }


    public class Pusher
    {
        public string name { get; set; }
        public string email { get; set; }
    }



}