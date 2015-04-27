using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Package : IEntity
    {
        public int Id { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public string OpenComment { get; set; }

        public string CloseComment { get; set; }

        public bool IsClosed { get; set; }

        public DateTime LastChangeDate { get; set; }

        public byte[] OpenPhoto { get; set; }

        public byte[] ClosePhoto { get; set; }

        public User OpenedByEmployee { get; set; }

        public int OpenedByEmployeeId { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public NotificationOptions NotificationOptions { get; set; }

        public int NotificationOptionsId { get; set; }

        public Location Location { get; set; }

        public int? LocationId { get; set; }

        public PackageType PackageType { get; set; }

        public int PackageTypeId { get; set; }


        public ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (OpenedByEmployeeId <= 0)
            {
                result.Errors.Add("Employee is required");
            }
            if (ApartmentId <= 0)
            {
                result.Errors.Add("Apartment is required");
            }
            if (NotificationOptions == null)
            {
                result.Errors.Add("Notification options is required");
            }
            if (LocationId <= 0)
            {
                result.Errors.Add("Location is required");
            }
            if (PackageTypeId <= 0)
            {
                result.Errors.Add("Package type is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}