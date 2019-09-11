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
        public double latitude { get; set; }
        public double longtitude { get; set; }

        public EventLocation()
        {
        }

        public EventLocation(int iD, string address, string city, int postCode, double latitude, double longtitude)
        {
            ID = iD;
            Address = address;
            City = city;
            PostCode = postCode;
            this.latitude = latitude;
            this.longtitude = longtitude;
        }
    }
}