using System;
using System.Globalization;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Models
{
    public class EventModel
    {
        public string Content { get; set; }

        public string DateTimeString { get; set; }


        public Event GetEvent()
        {
            return new Event
            {
                Content = Content,
                DateTime = DateTime.ParseExact(DateTimeString, "d/M/yyyy H:m", CultureInfo.InvariantCulture)
            };
        }
    }
}