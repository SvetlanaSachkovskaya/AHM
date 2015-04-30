using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Bill : IEntity
    {
        public int Id { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public DateTime Date { get; set; }

        public decimal CalculatedAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime? PaidDate { get; set; }

        public decimal Fine { get; set; }

        public decimal CarryOver { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailSent { get; set; }

        public ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (ApartmentId <= 0)
            {
                result.Errors.Add("Apartment is required");
            }

            if (PaidDate.HasValue && PaidDate.Value < Date)
            {
                result.Errors.Add("Paid date must be greater than create date");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}