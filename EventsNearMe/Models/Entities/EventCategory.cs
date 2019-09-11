using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
    }

}