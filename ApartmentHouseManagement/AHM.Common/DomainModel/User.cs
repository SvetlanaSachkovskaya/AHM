using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AHM.Common.DomainModel
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public Building Building { get; set; }

        public int? BuildingId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
    }
}