using System;

namespace AHM.Common.DomainModel
{
    public class Event : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public bool IsRemoved { get; set; }
    }
}