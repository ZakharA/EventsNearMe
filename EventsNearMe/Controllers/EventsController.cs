﻿using EntityFramework.Extensions;
using EventsNearMe.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EventsNearMe.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            List<Event> result;

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                result = db.Events.ToList();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                result = db.Events.Where(e => e.Organizer.Id == userId).ToList();
            }

            return View(result);
        }

        [AllowAnonymous]
        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(e => e.Location).FirstOrDefault(e => e.EventID == id);
            ViewBag.avaliableTickets = avaliableTickets(id);

            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.coverImage != null)
                {
                    saveImage(model);
                }
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                model.Event.Organizer = user;
                model.Event.CoverImage = model.coverImage.FileName;
                db.Events.Add(model.Event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private void saveImage(EventModel model)
        {
            string path = Server.MapPath("~/Content/Images/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            model.coverImage.SaveAs(path + Path.GetFileName(model.coverImage.FileName));
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(e => e.Location).FirstOrDefault(e => e.EventID == id);
            EventModel emodel = new EventModel();
            emodel.Event = @event;
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(emodel);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.Event.Location).State = EntityState.Modified;
                db.Entry(model.Event).State = EntityState.Modified;
                if (model.coverImage != null)
                {
                    model.Event.CoverImage = model.coverImage.FileName;
                    saveImage(model);
                }

                db.SaveChanges();
                return RedirectToAction("sendUpdateOnEvent", "Innovation", new { @eventId = model.Event.EventID });
            }
            return View(model);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bookings = db.Bookings.Where(b => b.EventId == id).Select(b => b.BookingID).ToList();
            db.Ratings.Where(r => bookings.Contains((int)r.BookingId)).Delete();
            db.Bookings.Where(b => b.EventId == id).Include(b => b.Rating).Delete(); 
            Event @event = db.Events.Include(e => e.Location).FirstOrDefault(e => e.EventID == id); 
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int avaliableTickets(int? eventId)
        {
            int numberOfTickets = db.Events.Find(eventId).numberOfTickets;
            int numberOfBookings = db.Bookings.Where(b => b.EventId == eventId).Count();
            return numberOfTickets - numberOfBookings;
        }
    }
}
