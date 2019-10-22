using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingID { get; set; }
        [Key, ForeignKey("Booking")]
        public int? BookingId { get; set; }
        public virtual  Booking Booking { get; set; }
        public int number { get; set; }
        public Rating()
        {
        }
    }
}