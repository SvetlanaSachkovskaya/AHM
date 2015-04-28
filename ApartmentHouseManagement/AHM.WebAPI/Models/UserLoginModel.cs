using System.ComponentModel.DataAnnotations;

namespace AHM.WebAPI.Models
{
    public class RegisterUserModel
    {
        [Required]
        [Display(Name = "User name")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        public int RoleId { get; set; }

        public int BuildingId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}