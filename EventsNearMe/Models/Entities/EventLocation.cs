using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models.Entities
{
    public class EventLocation
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public EventLocation()
        {
        }

        public EventLocation(int iD, string address, string city, int postCode, double latitude, double longtitude)
        {
            ID = iD;
            Address = address;
            City = city;
            PostCode = postCode;
            this.Latitude = latitude;
            this.Longtitude = longtitude;
        }
    }
}