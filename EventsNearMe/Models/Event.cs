using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{

    public enum EventCategory
    {
        SPORT,
        FASHION,
        MUSIC,
        FOOD
    }

    public struct Location
    {
        string Address;
        string City;
        int PostCode;
        Dictionary<string, double> Coordinates;
    }

    public class Event
    {
        public string Name { get; set; }
        public Dictionary<DateTime, int> dates { get; set; }
        public string Organizer { get; set; }
        public EventCategory EventCategory { get; set; }
        public bool IsFree { get; set; }
        public double Price { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public Event()
        {
        }

        public Event(string name, Dictionary<DateTime, int> date, string organizer, EventCategory eventCategory, bool isFree, double price, Location location, string description, List<string> tags)
        {
            Name = name;
            dates = date;
            Organizer = organizer;
            EventCategory = eventCategory;
            IsFree = isFree;
            Price = price;
            Location = location;
            Description = description;
            Tags = tags;
        }
    }
}