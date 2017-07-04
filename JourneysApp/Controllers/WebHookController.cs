using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JourneysApp.Models;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Configuration;
using JourneysApp.Services;
using Newtonsoft.Json;

namespace JourneysApp.Controllers
{
    public class WebHookController : Controller
    {
        private string Sha1Prefix = "sha1=";
        private IJourneysDbService _dbService;

        public WebHookController(IJourneysDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public ActionResult Index(GitHubPushEventData gitHubCommitData)
        {
            string eventName = string.Empty;
            string signature = string.Empty;
            string delivery = string.Empty;
            string test = string.Empty;

            eventName = Request.Headers["X-GitHub-Event"];
            signature = Request.Headers["X-Hub-Signature"]; ;
            delivery = Request.Headers["X-GitHub-Delivery"]; ;


            Stream requestStream = Request.InputStream;
            requestStream.Seek(0, System.IO.SeekOrigin.Begin);

            using (var reader = new StreamReader(requestStream))
            {
                var txt = reader.ReadToEnd();
                if (IsGithubPushAllowed(txt, eventName, signature))
                {
                    if (gitHubCommitData != null && eventName == "push")
                    {
                        try
                        {
                            SaveGitHubPushData(gitHubCommitData);
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                    }
                    else if (eventName == "issues")
                    {
                        GitHubIssueEventData gitHubIssueEventData = JsonConvert.DeserializeObject<GitHubIssueEventData>(txt);
                        try
                        {
                            SaveGitHubIssueData(gitHubIssueEventData);
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                        
                    }

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 

        }

        [HttpGet]
        public ActionResult GitHubPush()
        {
            var model = _dbService.GetAllGitHubPushes();

            return View(model);
        }

        [HttpGet]
        public ActionResult GitHubIssues()
        {
            var model = _dbService.GetAllGitHubIssues();
            return View(model);
        }
        private bool SaveGitHubPushData(GitHubPushEventData gitHubPushEventData)
        {
            bool result = false;
            GitHubPush gitHubPush = new GitHubPush();
            gitHubPush.After = gitHubPushEventData.after;
            gitHubPush.Before = gitHubPushEventData.before;
            gitHubPush.Ref = gitHubPushEventData.@ref;
            gitHubPush.PusherEmail = gitHubPushEventData.pusher?.email;
            gitHubPush.PusherName = gitHubPushEventData.pusher?.name;
            gitHubPush.Commits = new List<GitHubCommit>();

            GitHubCommit commit = null;

            foreach (var commitEventData in gitHubPushEventData.commits)
            {
                commit = new GitHubCommit();
                commit.AuthorEmail = commitEventData.author?.email;
                commit.AuthorName = commitEventData.author?.name;
                commit.CommitComment = commitEventData.message;

                gitHubPush.Commits.Add(commit);
            }

            try
            {
                _dbService.InsertGitHubPush(gitHubPush);
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        private bool SaveGitHubIssueData(GitHubIssueEventData gitHubIssueData)
        {
            bool result = false;
            DateTime createIssueDateTime = new DateTime();

            GitHubIssue gitHubIssue = new GitHubIssue();
            gitHubIssue.IssueUrl = gitHubIssueData.issue?.html_url;
            gitHubIssue.Title = gitHubIssueData.issue?.title;
            gitHubIssue.UserAdded = gitHubIssueData.issue?.user?.login;
            var isDateFormatValid = DateTime.TryParse(gitHubIssueData.issue?.created_at, out createIssueDateTime);

            if (isDateFormatValid)
                gitHubIssue.CreateTime = createIssueDateTime;

            gitHubIssue.Body = gitHubIssueData.issue?.body;

            try
            {
                _dbService.InsertGitHubIssue(gitHubIssue);
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
        private bool IsGithubPushAllowed(string payload, string eventName, string signatureWithPrefix)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                throw new ArgumentNullException(nameof(payload));
            }
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }
            if (string.IsNullOrWhiteSpace(signatureWithPrefix))
            {
                throw new ArgumentNullException(nameof(signatureWithPrefix));
            }

            if (signatureWithPrefix.StartsWith(Sha1Prefix, StringComparison.OrdinalIgnoreCase))
            {
                var signature = signatureWithPrefix.Substring(Sha1Prefix.Length);

                var secretString = ConfigurationManager.AppSettings["GitHubWebHookSecretKey"];
                if (string.IsNullOrWhiteSpace(secretString))
                    return false;

                var secret = Encoding.ASCII.GetBytes(secretString);
                var payloadBytes = Encoding.ASCII.GetBytes(payload);

                using (var hmSha1 = new HMACSHA1(secret))
                {
                    var hash = hmSha1.ComputeHash(payloadBytes);

                    var hashString = ToHexString(hash);

                    if (hashString.Equals(signature))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string ToHexString(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }

    }
}