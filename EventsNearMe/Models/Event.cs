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
        public Dictionary<DateTime, int> date { get; set; }
        public string Organizer { get; set; }
        public EventCategory EventCategory { get; set; }
        public bool IsFree { get; set; }
        public double price { get; set; }
        public Location location { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }
}