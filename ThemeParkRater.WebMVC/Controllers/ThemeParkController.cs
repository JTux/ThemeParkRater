using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThemeParkRater.Models.ThemeParkModels;
using ThemeParkRater.Services;

namespace ThemeParkRater.WebMVC.Controllers
{
    public class ThemeParkController : Controller
    {
        // GET: ThemePark
        public ActionResult Index()
        {
            var service = new ThemeParkService();
            var model = service.GetThemeParks();
            return View(model);
        }

        // GET: ThemePark/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThemePark/Create
        [HttpPost]
        public ActionResult Create(ThemeParkCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = new ThemeParkService();
            if (service.CreateThemePark(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Theme park could not be added");
            return View(model);
        }

        // GET: ThemePark/Detail/{id}
        public ActionResult Details(int id)
        {
            var service = new ThemeParkService();
            var model = service.GetParkByID(id);

            var ratingService = new ThemeParkRatingService(Guid.Parse(User.Identity.GetUserId()));
            var ratings = ratingService.GetRatingsByParkID(id);

            ViewBag.Ratings = ratings;

            return View(model);
        }

        // GET: ThemePark/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = new ThemeParkService();

            var detail = service.GetParkByID(id);
            var model = new ThemeParkEdit
            {
                ThemeParkID = detail.ThemeParkID,
                ThemeParkName = detail.ThemeParkName,
                ThemeParkCity = detail.ThemeParkCity,
                ThemeParkState = detail.ThemeParkState
            };

            return View(model);
        }

        // POST: ThemePark/Edit/{id}
        [HttpPost]
        public ActionResult Edit(ThemeParkEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = new ThemeParkService();

            if (service.EditThemePark(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Theme park could not be edited.");
            return View(model);
        }

        // GET: ThemePark/Delete/{id}
        public ActionResult Delete(int id)
        {
            var service = new ThemeParkService();

            var model = service.GetParkByID(id);

            return View(model);
        }

        // POST: ThemePark/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePark(int id)
        {
            var service = new ThemeParkService();

            if (service.DeleteThemePark(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }
    }
}