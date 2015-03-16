using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Bill : Entity
    {
        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public bool IsEmailSent { get; set; }

        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (ApartmentId <= 0)
            {
                result.Errors.Add("Apartment is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}