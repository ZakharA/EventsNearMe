using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class EventOrganizer : ApplicationUser
    {
        public virtual ICollection<Event> createdEvents { get; set; }

        public EventOrganizer()
        {
            this.createdEvents = new List<Event>();
        }

        public EventOrganizer(ICollection<Event> createdEvents)
        {
            this.createdEvents = createdEvents;
        }
    }
}