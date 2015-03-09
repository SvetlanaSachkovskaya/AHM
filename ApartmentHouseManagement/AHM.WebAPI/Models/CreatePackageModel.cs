using System;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Models
{
    public class CreatePackageModel
    {
        public string OpenComment { get; set; }

        public int ApartmentId { get; set; }

        public bool ShouldNotifyAll { get; set; }

        public int? OccupantId { get; set; }

        public int? LocationId { get; set; }

        public int PackageTypeId { get; set; }


        public Package GetPackage()
        {
            return new Package
            {
                OpenDate = DateTime.Now,
                OpenComment = OpenComment,
                ApartmentId = ApartmentId,
                NotificationOptions = new NotificationOptions
                {
                    ShouldNotifyAllOccupants = ShouldNotifyAll,
                    OccupantId = OccupantId,
                },
                LocationId = LocationId,
                PackageTypeId = PackageTypeId,
                IsClosed = false,
                LastChangeDate = DateTime.Now
            };
        }
    }
}