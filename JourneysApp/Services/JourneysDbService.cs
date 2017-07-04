using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JourneysApp.Models;
using System.Data.Entity;

namespace JourneysApp.Services
{
    public class JourneysDbService : IJourneysDbService
    {
        private JourneysContext _dbContext;

        public JourneysDbService(JourneysContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Journey> GetAllJourneys()
        {
            return _dbContext.Journeys.Where(j => j.IsDeleted == false).ToList();
        }

        public void InsertGitHubPush(GitHubPush gitHubPush)
        {
            try
            {
                _dbContext.GitHubPushes.Add(gitHubPush);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void InsertGitHubIssue(GitHubIssue gitHubIssue)
        {
            try
            {
                _dbContext.GitHubIssues.Add(gitHubIssue);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GitHubPush> GetAllGitHubPushes()
        {
            var gitHubPushes = _dbContext.GitHubPushes.Include(p => p.Commits).ToList();
            return gitHubPushes;
        }

        public List<GitHubIssue> GetAllGitHubIssues()
        {
            var gitHubIssues = _dbContext.GitHubIssues.ToList();
            return gitHubIssues;
        }

        public void AddJourney(Journey journey)
        {
            try
            {
                _dbContext.Journeys.Add(journey);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Journey> GetJourneysByUserName(string userName)
        {
            var journeys = _dbContext.Journeys.Where(j => j.UserLogin == userName && j.IsDeleted == false).ToList();

            return journeys;
        }

        public void UpdateJourney(Journey journey)
        {
            Journey journeyToEdit = GetJourneyById(journey.Id);

            journeyToEdit.Comment = journey.Comment;
            journeyToEdit.Destination = journey.Destination;
            journeyToEdit.Name = journey.Name;
            journeyToEdit.StartDate = journey.StartDate;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void DeleteJourney(int journeyId)
        {
            try
            {
                var journey = _dbContext.Journeys.Where(j => j.Id == journeyId).SingleOrDefault();
                if (journey != null)
                {
                    journey.IsDeleted = true;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public Journey GetJourneyById(int id)
        {
            Journey journey = null;

            try
            {
                journey = _dbContext.Journeys.Where(j => j.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return journey;
        }
    }
}