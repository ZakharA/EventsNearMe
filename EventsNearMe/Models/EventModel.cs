using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class EventModel
    {
        public Event Event { get; set; }
        public HttpPostedFileBase coverImage { get; set; }
    }
}