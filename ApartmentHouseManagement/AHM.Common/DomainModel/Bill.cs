using System;

namespace AHM.Common.DomainModel
{
    public class Bill : Entity
    {
        public Apartment Apartment { get; set; }

        public int ApartmentId { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public bool IsEmailSent { get; set; }
    }
}