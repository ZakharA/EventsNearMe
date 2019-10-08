﻿using EventsNearMe.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventsNearMe.Models
{
    public enum EventType {
        Food,
        Fashion,
        Sport,
        Entertainment
    }

    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        public string Name { get; set; }
        public string CoverImage { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }
        [Range(1,365, ErrorMessage = "Event length must be at least one day")]
        public int eventLength { get; set; }
        public virtual EventOrganizer Organizer { get; set; }
        [EnumDataType(typeof(EventType))]
        public EventType EventCategory { get; set; }
        public bool IsFree { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public virtual EventLocation Location { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Event()
        {
            this.Location = new EventLocation();
        }

        public Event(int eventID, string name, DateTime startingDate, int eventLength, EventOrganizer organizer, EventType eventCategory, bool isFree, double price, EventLocation location, string description)
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