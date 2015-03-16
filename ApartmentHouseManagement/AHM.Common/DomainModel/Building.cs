using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Building : Entity
    {
        public string Name { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Email { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (String.IsNullOrEmpty(Name))
            {
                result.Errors.Add("Name is required");
            }
            if (String.IsNullOrEmpty(State))
            {
                result.Errors.Add("State is required");
            }
            if (String.IsNullOrEmpty(City))
            {
                result.Errors.Add("City is required");
            }
            if (String.IsNullOrEmpty(Street))
            {
                result.Errors.Add("Street is required");
            }
            if (String.IsNullOrEmpty(Number))
            {
                result.Errors.Add("Number is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}