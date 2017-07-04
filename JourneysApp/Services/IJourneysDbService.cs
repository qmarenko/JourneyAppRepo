using JourneysApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneysApp.Services
{
    public interface IJourneysDbService
    {
        Journey GetJourneyById(int id);
        List<Journey> GetAllJourneys();
        List<Journey> GetJourneysByUserName(string userName);
        void AddJourney(Journey journey);
        void UpdateJourney(Journey journey);
        void DeleteJourney(int journeyId);
        void InsertGitHubPush(GitHubPush gitHubPush);
        List<GitHubPush> GetAllGitHubPushes();
        void InsertGitHubIssue(GitHubIssue gitHubIssue);
        List<GitHubIssue> GetAllGitHubIssues();
    }
}
