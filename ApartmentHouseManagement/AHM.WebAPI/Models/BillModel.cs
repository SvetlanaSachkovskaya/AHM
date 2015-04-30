using System.Collections.Generic;
using AHM.Common.DomainModel;

namespace AHM.WebAPI.Models
{
    public class BillModel : Bill
    {
        public List<UtilitiesItem> UtilitiesItems { get; set; }


        public BillModel()
        {
            
        }

        public BillModel(Bill bill, List<UtilitiesItem> items)
        {
            Id = bill.Id;
            ApartmentId = bill.ApartmentId;
            Apartment = bill.Apartment;
            Date = bill.Date;
            CalculatedAmount = bill.CalculatedAmount;
            IsActive = bill.IsActive;
            PaidAmount = bill.PaidAmount;
            PaidDate = PaidDate;
            Fine = Fine;
            IsEmailSent = bill.IsEmailSent;
            UtilitiesItems = items;
        }


        public Bill GetBill()
        {
            return new Bill
            {
                Id = Id,
                ApartmentId = ApartmentId,
                Date = Date,
                IsEmailSent = IsEmailSent,
                IsActive = IsActive,
                CalculatedAmount = CalculatedAmount
            };
        }
    }
}