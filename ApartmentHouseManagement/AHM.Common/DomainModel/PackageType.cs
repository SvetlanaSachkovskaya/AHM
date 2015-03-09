namespace AHM.Common.DomainModel
{
    public class PackageType : Entity
    {
        public Building Building { get; set; }

        public int BuildingId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }
    }
}