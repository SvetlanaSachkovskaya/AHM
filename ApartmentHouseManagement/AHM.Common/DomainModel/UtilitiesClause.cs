using System;
using System.Linq;

namespace AHM.Common.DomainModel
{
    public class UtilitiesClause : Entity
    {
        public UtilitiesClauseType UtilitiesClauseType { get; set; }

        public string Measure { get; set; }

        public CalculationType CalculationType { get; set; }

        public decimal SubsidizedTariff { get; set; }

        public decimal FullTariff { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsLimited { get; set; }

        public double LimitForPerson { get; set; }

        public Building Building { get; set; }

        public int BuildingId { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (BuildingId <= 0)
            {
                result.Errors.Add("Building is required");
            }
            if (String.IsNullOrEmpty(Name))
            {
                result.Errors.Add("Name is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}