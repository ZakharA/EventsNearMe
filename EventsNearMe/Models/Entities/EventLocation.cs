using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models.Entities
{
    public class EventLocation
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "{0} address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "{0} City is required")]
        public string City { get; set; }
        [DataType(DataType.PostalCode)]
        public int PostCode { get; set; }
        [Range(0,90, ErrorMessage = "Latitude range from 0 to 90")]
        public double Latitude { get; set; }
        [Range(0, 180, ErrorMessage = "Longtitude range from 0 to 180")]
        public double Longitudes { get; set; }

        public EventLocation()
        {
        }

        public EventLocation(int iD, string address, string city, int postCode, double latitude, double longitudes)
        {
            ID = iD;
            Address = address;
            City = city;
            PostCode = postCode;
            this.Latitude = latitude;
            this.Longitudes = longitudes;
        }
    }
}