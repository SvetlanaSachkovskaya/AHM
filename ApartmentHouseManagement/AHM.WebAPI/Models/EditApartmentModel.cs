using AHM.Common.DomainModel;

namespace AHM.WebAPI.Models
{
    public class EditApartmentModel : Apartment
    {
        public int OwnerId { get; set; }


        public Apartment GetApartment()
        {
            return new Apartment
            {
                Id = Id,
                BuildingId = BuildingId,
                Floor = Floor,
                Number = Number,
                Square = Square,
                PersonalAccount = PersonalAccount
            };
        }
    }
}