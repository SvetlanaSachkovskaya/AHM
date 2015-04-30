namespace AHM.Common.DomainModel
{
    public class UserModel
    {
        public int Id { get; set; }

        public int? BuildingId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsLocked { get; set; }
    }
}
