using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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


        public ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (BuildingId <= 0)
            {
                result.Errors.Add("Building is required");
            }
            if (String.IsNullOrEmpty(UserName))
            {
                result.Errors.Add("UserName is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}