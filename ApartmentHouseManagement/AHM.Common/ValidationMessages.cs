namespace AHM.Common
{
    public static class ValidationMessages
    {
        public const string LocationInUse = "The location is in use";
        public const string PackageTypeInUse = "The package type is in use";
        public const string OccupantInUse = "The occupant is in use";
        public const string ApartmentInUse = "The apartment is in use";
        public const string BuildingNotFound = "Building is not found";
        public const string UsernameExists = "User with such username already exists";
        public const string EmptyOccupantEmail = "Apartment owner does not have emwil address";
        public const string NoApartmentOwner = "Apartment owner was not found";
        public const string LockHimself = "User can't lock himself";
    }
}