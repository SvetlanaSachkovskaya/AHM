using System;

namespace AHM.Common.DomainModel
{
    public class Package : Entity
    {
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
    }
}