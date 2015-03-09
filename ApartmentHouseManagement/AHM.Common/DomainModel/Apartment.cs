namespace AHM.Common.DomainModel
{
    public class Apartment : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public int Floor { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public double Square { get; set; }
    }
}