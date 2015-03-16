using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Apartment : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public int Floor { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public double Square { get; set; }

        public string PersonalAccount { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (BuildingId <= 0)
            {
                result.Errors.Add("Building is required");
            }
            if (String.IsNullOrEmpty(Number))
            {
                result.Errors.Add("Number is required");
            }

            if (String.IsNullOrEmpty(PersonalAccount))
            {
                result.Errors.Add("Personal account is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}