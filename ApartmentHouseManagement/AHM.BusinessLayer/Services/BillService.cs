using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common;
using AHM.Common.DomainModel;
using AHM.DataLayer.Interfaces;

namespace AHM.BusinessLayer.Services
{
    public class BillService : BaseService, IBillService
    {
        public BillService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }


        public async Task<ICollection<Bill>> GetAllBillsAsync(int buildingId, BillDateInteval dateInteval, bool? isClosed = null)
        {
            switch (dateInteval)
            {
                case BillDateInteval.Mounth:
                    return
                        await
                            UnitOfWork.GetRepository<Bill>()
                                .GetAllAsync(
                                    b =>
                                        b.Date.Month == DateTime.Now.Month && b.Date.Year == DateTime.Now.Year &&
                                        (!isClosed.HasValue || b.IsClosed == isClosed.Value));
                case BillDateInteval.Year:
                    return
                        await
                            UnitOfWork.GetRepository<Bill>()
                                .GetAllAsync(
                                    b =>
                                        b.Date.Year == DateTime.Now.Year &&
                                        (!isClosed.HasValue || b.IsClosed == isClosed.Value));
                case BillDateInteval.All:
                    return
                        await
                            UnitOfWork.GetRepository<Bill>()
                                .GetAllAsync(
                                    b => (!isClosed.HasValue || b.IsClosed == isClosed.Value));
                default:
                    throw new ArgumentOutOfRangeException("dateInteval");
            }
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

                bill.CalculatedAmount = utilitiesItems.Sum(i => (i.SubsidezedAmount + i.AmountByFullTariff));

                UnitOfWork.GetRepository<Bill>().Update(bill);
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
                    bill.IsClosed = true;
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

                bill.PaidDate = DateTime.Now;

                UnitOfWork.GetRepository<Bill>().Update(bill);
                await UnitOfWork.SaveAsync();
            });

            return updatingResult;
        }

        public async Task<ModifyDbStateResult> SendEmailAsync(Bill bill, string email, string username, string password, string filePath)
        {
            var updatingResult = await UpdateEntityAsync(bill, "Failed to send email", async () =>
            {
                var owner = await UnitOfWork.GetRepository<Occupant>().GetEntityAsync(o => o.ApartmentId == bill.ApartmentId && o.IsOwner);
                if (owner == null)
                {
                    return;
                }

                var mail = new MailMessage(email, owner.Email);
                var client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 10000,
                    Credentials = new NetworkCredential(username, password)
                };

                mail.Subject = "Utilities bill";
                mail.Attachments.Add(new Attachment(filePath));

                client.Send(mail);

                mail.Dispose();

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

            double overshoot = 0;
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
                var deadline = new DateTime(lastBill.Date.Year, lastBill.Date.Month, apartment.Building.LastPayUtilitiesDay);

                bill.CarryOver = lastBill.CalculatedAmount + lastBill.CarryOver + lastBill.Fine - lastBill.PaidAmount;
                bill.Fine = bill.CarryOver * (decimal)apartment.Building.FinePercent * (DateTime.Now - deadline).Days;

                lastBill.IsClosed = true;
                UnitOfWork.BillRepository.Update(lastBill);
            }

            UnitOfWork.BillRepository.Add(bill);
            await UnitOfWork.SaveAsync();
        }

        private async Task UpdateUtilitiesItems(List<UtilitiesItem> utilitiesItems, Bill bill)
        {
            var utilitiesItemsBeforeUpdate =
                        await UnitOfWork.GetRepository<UtilitiesItem>().GetAllAsync(i => i.BillId == bill.Id);

            var removedUtilitiesItems =
                utilitiesItemsBeforeUpdate.Where(i => utilitiesItems.Find(it => it.Id == i.Id) == null);
            UnitOfWork.GetRepository<UtilitiesItem>().DeleteRange(removedUtilitiesItems.Select(i => i.Id));

            var occupants = await UnitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == bill.ApartmentId);
            var occupantsCount = occupants.Count();
            foreach (var utilitiesItem in utilitiesItems)
            {
                await ProcessUtilitiesItemAsync(utilitiesItem, occupantsCount);

                if (utilitiesItem.Id > 0)
                {
                    utilitiesItem.UtilitiesClause = null;
                    UnitOfWork.GetRepository<UtilitiesItem>().Update(utilitiesItem);
                }
                else
                {
                    utilitiesItem.BillId = bill.Id;
                    UnitOfWork.GetRepository<UtilitiesItem>().Add(utilitiesItem);
                }
            }
        }
    }
}