using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
using System;

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
   
    }
}