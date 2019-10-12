using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public Booking()
        {
        }

        public Booking(int bookingID, string userId, ApplicationUser user, int eventId, Event @event)
        {
            BookingID = bookingID;
            User = user;
            EventId = eventId;
            Event = @event;
        }
    }
}