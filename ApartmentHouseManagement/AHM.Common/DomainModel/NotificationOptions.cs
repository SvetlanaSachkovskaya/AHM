namespace AHM.Common.DomainModel
{
    public class NotificationOptions : IEntity
    {
        public int Id { get; set; }

        public bool IsOpenNotificationSent { get; set; }

        public bool ShouldNotifyAllOccupants { get; set; }

        public Occupant Occupant { get; set; }

        public int? OccupantId { get; set; }


        public ValidationResult Validate()
        {
            return new ValidationResult { IsValid = true };
        }
    }
}