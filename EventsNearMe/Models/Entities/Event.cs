using EventsNearMe.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public string CoverImage { get; set; }
        public DateTime StartingDate { get; set; }
        public int eventLength { get; set; }
        public virtual EventOrganizer Organizer { get; set; }
        public virtual EventCategory EventCategory { get; set; }
        public bool IsFree { get; set; }
        public double Price { get; set; }
        public virtual EventLocation Location { get; set; }
        public string Description { get; set; }

        public Event()
        {
        }

        public Event(int eventID, string name, DateTime startingDate, int eventLength, EventOrganizer organizer, EventCategory eventCategory, bool isFree, double price, EventLocation location, string description)
        {
            EventID = eventID;
            Name = name;
            StartingDate = startingDate;
            this.eventLength = eventLength;
            Organizer = organizer;
            EventCategory = eventCategory;
            IsFree = isFree;
            Price = price;
            Location = location;
            Description = description;
        }
    }
}