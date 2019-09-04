using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public enum EventDate
    {
        
    }

    public enum EventType
    {
    }

    public class Event
    {
        public string Name { get; set; }
        public Dictionary<EventDate, DateTime> dates { get; set; }
        public string Organizer { get; set; }
        public EventType TypeOfEvent { get; set; }
        public double price { get; set; }
        public Location location { get; set; }
        public string description { get; set; }
        public List<string> tags { get; set; }
    }
}

namespace EventsNearMe
{
    public struct Location
    {
        string address;
        string city;
        int postCode;
        Dictionary<string, double> coordinates;
    }
}