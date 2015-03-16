using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Occupant : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public bool IsSubTenant { get; set; }

        public bool IsOwner { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (ApartmentId <= 0)
            {
                result.Errors.Add("Apartment is required");
            }
            if (String.IsNullOrEmpty(Name))
            {
                result.Errors.Add("Name is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}