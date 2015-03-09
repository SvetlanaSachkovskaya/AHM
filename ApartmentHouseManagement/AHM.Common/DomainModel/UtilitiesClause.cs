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
    }
}