using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Event : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsRemoved { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (BuildingId <= 0)
            {
                result.Errors.Add("Building is required");
            }

            if (String.IsNullOrEmpty(Content))
            {
                result.Errors.Add("Content is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}