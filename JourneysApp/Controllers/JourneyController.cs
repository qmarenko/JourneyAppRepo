using JourneysApp.Models;
using JourneysApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace JourneysApp.Controllers
{
    [Authorize]
    public class JourneyController : Controller
    {
        private IJourneysDbService _dbService;

        public JourneyController(IJourneysDbService dbService)
        {
            _dbService = dbService;
        }
        // GET: Journey
        public ActionResult Index()
        {
            List<Journey> journeys = null;
            /*
            var identity = (ClaimsIdentity)User.Identity;
            var test = identity.HasClaim(ClaimTypes.Role, "Administrator");
            */
            // TODO: problem with AD roles - always false

            var user = User?.Identity?.Name;

            if (User.IsInRole("Administrator"))
                journeys = _dbService.GetAllJourneys();
            else
                journeys = _dbService.GetJourneysByUserName(user);

            return View(journeys);
        }

        // GET: Journey/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Journey/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Journey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Journey journey)
        {

            var user = User?.Identity?.Name;
            journey.UserLogin = user;
            try
            {
                try
                {
                    _dbService.AddJourney(journey);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Journey/Edit/5
        public ActionResult Edit(int id)
        {
            Journey journey = null;

            try
            {
               journey = _dbService.GetJourneyById(id);

                if (journey != null)
                    return View(journey);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);


        }

        // POST: Journey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Journey journey)
        {
            try
            {
                if (journey != null)
                {
                    try
                    {
                        _dbService.UpdateJourney(journey);
                    }
                    catch (Exception)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                    
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: Journey/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var journey = _dbService.GetJourneyById(id);

            if (journey != null)
            {
                return View(journey);
            }

            return RedirectToAction("Index");
        }

        // POST: Journey/Delete/5
        [HttpPost]
        public ActionResult Delete(Journey journey)
        {
            if (journey != null)
            {
                try
                {
                    _dbService.DeleteJourney(journey.Id);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            return View();
        }
    }
}
