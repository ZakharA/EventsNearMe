using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using EventsNearMe.Models;
using System.IO;

namespace EventsNearMe.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            List<Booking> result;

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                result = db.Bookings.Include(b => b.Event).ToList();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                result = db.Bookings.Where(e => e.User.Id == userId).Include(b => b.Event).Include(b => b.Rating).ToList();
            }
            
            return View(result);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Include(b => b.Event.Location).FirstOrDefault(b => b.BookingID == id);
           // var result = new InnovationController().SendBookingConfirmation(bookingHtml, booking); 
            if (booking == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("SendBookingConfirmation", "Innovation", new { @bookingID = booking.BookingID });
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "EventID", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID")] int eventId)
        {
            if (ModelState.IsValid)
            {
                Booking newBooking = new Booking();
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                newBooking.EventId = eventId;
                newBooking.User = user;
                Booking savedBooking = db.Bookings.Add(newBooking);
                db.SaveChanges();
                return RedirectToAction("Details", new { @id = savedBooking.BookingID });
            }

            return RedirectToAction("Index");
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventID", "Name", booking.EventId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,EventId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventID", "Name", booking.EventId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Rate(int bookingId, int rating)
        {
            if(db.Ratings.Where(r => r.Booking.BookingID == bookingId).Count() == 0)
            {
                Rating newRating = new Rating();
                newRating.BookingId = bookingId;
                newRating.number = rating;
                db.Ratings.Add(newRating);
                db.SaveChanges();
                return Json(new { success = true });
            } else
            {
                return Json(new { fail = true });
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
