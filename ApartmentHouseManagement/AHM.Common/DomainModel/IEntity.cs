namespace AHM.Common.DomainModel
{
    public interface IEntity
    {
        int Id { get; set; }

        ValidationResult Validate();
    }
}