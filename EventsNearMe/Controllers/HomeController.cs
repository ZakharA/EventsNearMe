using EventsNearMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventsNearMe.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Event myEvent1 = new Event();
            Event myEvent2 = new Event();
            Event myEvent3 = new Event();
            Event myEvent4 = new Event();

            myEvent1.Name = "event 1";
            myEvent2.Name = "event 2";
            myEvent3.Name = "event 3";
            myEvent4.Name = "event 4";

            Event[] events = { myEvent1, myEvent2, myEvent3, myEvent4 };

            return View(events);
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