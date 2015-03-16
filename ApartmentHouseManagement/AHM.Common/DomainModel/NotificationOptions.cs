namespace AHM.Common.DomainModel
{
    public class NotificationOptions : Entity
    {
        public bool IsOpenNotificationSent { get; set; }

        public bool ShouldNotifyAllOccupants { get; set; }

        public Occupant Occupant { get; set; }

        public int? OccupantId { get; set; }


        public override ValidationResult Validate()
        {
            return new ValidationResult { IsValid = true };
        }
    }
}