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
    }
}