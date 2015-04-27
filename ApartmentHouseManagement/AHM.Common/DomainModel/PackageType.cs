using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class PackageType : IEntity
    {
        public int Id { get; set; }

        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }


        public ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (BuildingId <= 0)
            {
                result.Errors.Add("Building is required");
            }
            if (String.IsNullOrEmpty(ShortDescription))
            {
                result.Errors.Add("Short description is required");
            }

            if (String.IsNullOrEmpty(LongDescription))
            {
                result.Errors.Add("Long description is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}