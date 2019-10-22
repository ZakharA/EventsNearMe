﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventsNearMe.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("eventsNearMe", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EventsNearMe.Models.Event> Events { get; set; }
        public System.Data.Entity.DbSet<EventsNearMe.Models.EventLocation> EventLocations { get; set; }
        public System.Data.Entity.DbSet<EventsNearMe.Models.Booking> Bookings { get; set; }
        public System.Data.Entity.DbSet<EventsNearMe.Models.Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}