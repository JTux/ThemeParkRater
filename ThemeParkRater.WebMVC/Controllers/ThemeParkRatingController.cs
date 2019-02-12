using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThemeParkRater.Models.ThemeParkRatingModels;
using ThemeParkRater.Services;

namespace ThemeParkRater.WebMVC.Controllers
{
    public class ThemeParkRatingController : Controller
    {
        // GET: ThemeParkRating
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var parkService = new ThemeParkService();
            var parkList = parkService.GetThemeParks();

            ViewBag.ThemeParkID = new SelectList(parkList, "ThemeParkID", "ThemeParkName");

            return View();
        }

        [HttpPost]
        public ActionResult Create(ThemeParkRatingCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetRatingService();

            if (service.CreateRating(model))
            {
                return RedirectToAction("Index","ThemePark");
            }

            var parkService = new ThemeParkService();
            var parkList = parkService.GetThemeParks();

            ViewBag.ThemeParkID = new SelectList(parkList, "ThemeParkID", "ThemeParkName");

            return View(model);
        }

        private ThemeParkRatingService GetRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ThemeParkRatingService(userId);
            return service;
        }
    }
}