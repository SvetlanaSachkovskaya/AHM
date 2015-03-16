using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class Instruction : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int EmploeeId { get; set; }

        public virtual User Emploee { get; set; }

        public DateTime ExecutionDate { get; set; }

        public bool IsClosed { get; set; }

        public Priority Priority { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (BuildingId <= 0)
            {
                result.Errors.Add("Building is required");
            }
            if (EmploeeId <= 0)
            {
                result.Errors.Add("Emploee is required");
            }
            if (String.IsNullOrEmpty(Title))
            {
                result.Errors.Add("Title is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}