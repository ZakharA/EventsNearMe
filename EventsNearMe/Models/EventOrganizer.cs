using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class EventOrganizer : IdentityUser
    {
        public virtual ICollection<Event> createdEvents { get; set; }
    }
}