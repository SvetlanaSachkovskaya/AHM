using System.Linq;

namespace AHM.Common.DomainModel
{
    public class UtilitiesItem : Entity
    {
        public UtilitiesClause UtilitiesClause { get; set; }

        public int UtilitiesClauseId { get; set; }

        public Bill Bill { get; set; }

        public int BillId { get; set; }

        public double Quantity { get; set; }

        public decimal SubsidezedAmount { get; set; }

        public decimal AmountByFullTariff { get; set; }


        public override ValidationResult Validate()
        {
            var result = new ValidationResult();
            if (UtilitiesClauseId <= 0)
            {
                result.Errors.Add("Utilities clause is required");
            }
            if (BillId <= 0)
            {
                result.Errors.Add("Bill is required");
            }

            result.IsValid = !result.Errors.Any();

            return result;
        }
    }
}