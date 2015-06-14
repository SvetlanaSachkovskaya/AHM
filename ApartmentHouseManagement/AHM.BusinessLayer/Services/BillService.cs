using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class BillService : BaseService, IBillService
    {
        private readonly IEmailSender _emailSender;

        public BillService(IUnitOfWork unitOfWork, IEmailSender emailSender) : base(unitOfWork)
        {
            _emailSender = emailSender;
        }


        public async Task<ICollection<Bill>> GetAllBillsAsync(int buildingId, bool onlyOpen = false)
        {
            var bills =
                await
                    UnitOfWork.GetRepository<Bill>()
                        .GetAllAsync(
                            b =>
                                b.Apartment.BuildingId == buildingId &&
                                (!onlyOpen || !b.IsClosed));

            return bills;
        }

        public async Task<ICollection<Bill>> GetBillsByDateAsync(int buildingId, DateTime date, bool onlyOpen = false)
        {
            var bills =
                await
                    UnitOfWork.GetRepository<Bill>()
                        .GetAllAsync(
                            b =>
                                b.Apartment.BuildingId == buildingId &&
                                (!onlyOpen || !b.IsClosed));

            return bills.Where(b => b.Date.Month == date.Month && date.Date.Year == date.Year).ToList();
        }

        public async Task<Bill> GetByIdAsync(int id)
        {
            return await UnitOfWork.GetRepository<Bill>().GetByIdAsync(id);
        }

        public async Task<ModifyDbStateResult> AddAsync(Bill bill, List<UtilitiesItem> utilitiesItems)
        {
            var creationResult = await AddEntityAsync(bill, "Failed to create bill", async () =>
            {
                await SaveNewBill(bill);

                var occupants =
                    await UnitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == bill.ApartmentId);
                var occupantsCount = occupants.Count();

                foreach (var utilitiesItem in utilitiesItems)
                {
                    utilitiesItem.BillId = bill.Id;
                    await ProcessUtilitiesItemAsync(utilitiesItem, occupantsCount);
                }
                UnitOfWork.GetRepository<UtilitiesItem>().AddRange(utilitiesItems);

                bill.CalculatedAmount = utilitiesItems.Sum(i => (i.SubsidezedAmount + i.AmountByFullTariff));
                UnitOfWork.GetRepository<Bill>().Update(bill);

                await UnitOfWork.SaveAsync();
            });

            return creationResult;
        }

        public async Task<ModifyDbStateResult> UpdateAsync(Bill bill, List<UtilitiesItem> utilitiesItems)
        {
            var updatingResult = await UpdateEntityAsync(bill, "Failed to update Bill", async () =>
            {
                await UpdateUtilitiesItems(utilitiesItems, bill);

                var billEntity = await UnitOfWork.GetRepository<Bill>().GetByIdAsync(bill.Id);
                billEntity.CalculatedAmount = utilitiesItems.Sum(i => (i.SubsidezedAmount + i.AmountByFullTariff));
                UnitOfWork.GetRepository<Bill>().Update(billEntity);

                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> PayBillAsync(Bill bill)
        {
            var updatingResult = await UpdateEntityAsync(bill, "Failed to pay Bill", async () =>
            {
                var fullAmount = bill.CalculatedAmount + bill.CarryOver + bill.Fine;

                if (bill.PaidAmount >= fullAmount)
                {
                    var overpay = bill.PaidAmount - fullAmount;
                    if (overpay > 0)
                    {
                        var apartment = await UnitOfWork.GetRepository<Apartment>().GetByIdAsync(bill.ApartmentId);
                        apartment.EscrowBalance += overpay;
                        UnitOfWork.GetRepository<Apartment>().Update(apartment);
                    }
                }
                else
                {
                    var apartment =
                        await UnitOfWork.GetRepository<Apartment>().GetByIdAsync(bill.ApartmentId);

                    if (apartment.EscrowBalance > 0)
                    {
                        var shortPay = fullAmount - bill.PaidAmount;
                        var escrowDeductionAmount = apartment.EscrowBalance >= shortPay ? shortPay : apartment.EscrowBalance;

                        bill.PaidAmount += escrowDeductionAmount;
                        apartment.EscrowBalance -= escrowDeductionAmount;

                        UnitOfWork.GetRepository<Apartment>().Update(apartment);
                    }
                }

                bill.IsClosed = true;
                bill.PaidDate = DateTime.Now;

                UnitOfWork.GetRepository<Bill>().Update(bill);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> SendEmailAsync(Bill bill, string filePath)
        {
            var owner = await UnitOfWork.GetRepository<Occupant>().GetEntityAsync(o => o.ApartmentId == bill.ApartmentId && o.IsOwner);
            if (owner == null)
            {
                return new ModifyDbStateResult
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ValidationMessages.NoApartmentOwner }
                };
            }

            if (String.IsNullOrEmpty(owner.Email))
            {
                return new ModifyDbStateResult
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ValidationMessages.EmptyOccupantEmail }
                };
            }

            var updatingResult = await UpdateEntityAsync(bill, "Failed to send email", async () =>
            {
                _emailSender.Send(owner.Email, Constants.BillEmailSubject, String.Empty, filePath);

                var sentBill = await UnitOfWork.GetRepository<Bill>().GetByIdAsync(bill.Id);
                sentBill.IsEmailSent = true;
                UnitOfWork.GetRepository<Bill>().Update(sentBill);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        private async Task ProcessUtilitiesItemAsync(UtilitiesItem utilitiesItem, int occupantsCount)
        {
            var utilitiesClause = await
                UnitOfWork.GetRepository<UtilitiesClause>().GetByIdAsync(utilitiesItem.UtilitiesClauseId);

            double overshoot = utilitiesItem.Quantity;
            if (utilitiesClause.IsLimited)
            {
                var difference = utilitiesItem.Quantity - utilitiesClause.LimitForPerson*occupantsCount;
                overshoot = difference > 0 ? difference : 0;
            }

            switch (utilitiesClause.CalculationType)
            {
                case CalculationType.Coefficient:
                    utilitiesItem.SubsidezedAmount = (decimal)(utilitiesItem.Quantity - overshoot)*
                                                     utilitiesClause.SubsidizedTariff;
                    utilitiesItem.AmountByFullTariff = (decimal) overshoot*utilitiesClause.FullTariff;
                    break;
                case CalculationType.Rate:
                    utilitiesItem.SubsidezedAmount = utilitiesClause.SubsidizedTariff;
                    utilitiesItem.AmountByFullTariff = overshoot > 0 ? utilitiesClause.FullTariff : 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task SaveNewBill(Bill bill)
        {
            var lastBill = await UnitOfWork.BillRepository.GetLastBillAsync(bill.ApartmentId);
            if (lastBill != null)
            {
                var apartment = await UnitOfWork.GetRepository<Apartment>().GetByIdAsync(bill.ApartmentId);
                var deadline = new DateTime(lastBill.Date.Year, lastBill.Date.Month + 1, apartment.Building.LastPayUtilitiesDay);

                bill.CarryOver = lastBill.CalculatedAmount + lastBill.CarryOver + lastBill.Fine - lastBill.PaidAmount;

                var daysOverdraft = (DateTime.Now - deadline).Days;
                if (daysOverdraft > 0)
                {
                    bill.Fine = daysOverdraft * bill.CarryOver * (decimal)apartment.Building.FinePercent / 100 ;
                }

                lastBill.IsClosed = true;
                UnitOfWork.BillRepository.Update(lastBill);
            }

            UnitOfWork.BillRepository.Add(bill);
            await UnitOfWork.SaveAsync();
        }

        private async Task UpdateUtilitiesItems(List<UtilitiesItem> utilitiesItems, Bill bill)
        {
            UnitOfWork.BillRepository.DeleteOldUtilitiesItems(utilitiesItems, bill.Id);
            await UnitOfWork.SaveAsync();
            
            var occupants = await UnitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == bill.ApartmentId);
            var occupantsCount = occupants.Count();
            foreach (var utilitiesItem in utilitiesItems)
            {
                await ProcessUtilitiesItemAsync(utilitiesItem, occupantsCount);

                if (utilitiesItem.Id > 0)
                {
                    UnitOfWork.GetRepository<UtilitiesItem>().Update(utilitiesItem);
                }
                else
                {
                    utilitiesItem.BillId = bill.Id;
                    UnitOfWork.GetRepository<UtilitiesItem>().Add(utilitiesItem);
                }
            }
            //await UnitOfWork.SaveAsync();
        }
    }
}