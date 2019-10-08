using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public class EventCategory
    {
        public EventCategory()
        {
        }

        public EventCategory(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public int ID { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }

}