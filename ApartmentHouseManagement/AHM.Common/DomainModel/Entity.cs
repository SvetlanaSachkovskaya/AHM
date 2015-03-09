using System.ComponentModel.DataAnnotations;

namespace AHM.Common.DomainModel
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}