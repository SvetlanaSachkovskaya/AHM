using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Models
{
    public class EditBuildingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public Building GetBuilding()
        {
            return new Building
            {
                City = City,
                FinePercent = 0,
                Id = Id,
                LastPayUtilitiesDay = 29,
                Name = Name,
                Number = Number,
                State = State,
                Street = Street
            };
        }
    }
}