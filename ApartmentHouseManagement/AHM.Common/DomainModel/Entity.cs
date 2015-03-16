using System.ComponentModel.DataAnnotations;

namespace AHM.Common.DomainModel
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }


        public abstract ValidationResult Validate();
    }
}