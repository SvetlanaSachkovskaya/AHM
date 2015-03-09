using System;
using System.ComponentModel.DataAnnotations;

namespace AHM.Common.DomainModel
{
    public class Occupant : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public bool IsSubTenant { get; set; }

        public bool IsOwner { get; set; }

        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }
    }
}