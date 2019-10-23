using EventsNearMe.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EventsNearMe.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
           
            return View(db.Events.Where(e => System.Data.Entity.DbFunctions.AddDays(e.StartingDate, e.eventLength) >= DateTime.Now).ToArray());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}