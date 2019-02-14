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
            return View(model);
        }

        // GET: ThemePark/Edit/{id}

        // POST: ThemePark/Edit/{id}

        // GET: ThemePark/Delete/{id}

        // POST: ThemePark/Delete/{id}
    }
}