using System;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Models
{
    public class ShortUserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string Username { get; set; }

        public bool IsLocked { get; set; }

        public int? BuildingId { get; set; }


        public ShortUserModel(UserModel user)
        {
            Id = user.Id;
            Name = String.Format("{0} {1}", user.FirstName, user.LastName);
            Username = user.Username;
            IsLocked = user.IsLocked;
            Role = user.RoleName;
            BuildingId = user.BuildingId;
        }
    }
}