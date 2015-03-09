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
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;


        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ICollection<Bill>> GetAllBillsAsync(int buildingId, BillDateInteval dateInteval, bool? isPaid = null)
        {
            switch (dateInteval)
            {
                case BillDateInteval.Mounth:
                    return
                        await
                            _unitOfWork.GetRepository<Bill>()
                                .GetAllAsync(
                                    b =>
                                        b.Date.Month == DateTime.Now.Month && b.Date.Year == DateTime.Now.Year &&
                                        (!isPaid.HasValue || b.IsPaid == isPaid.Value));
                case BillDateInteval.Year:
                    return
                        await
                            _unitOfWork.GetRepository<Bill>()
                                .GetAllAsync(
                                    b =>
                                        b.Date.Year == DateTime.Now.Year &&
                                        (!isPaid.HasValue || b.IsPaid == isPaid.Value));
                case BillDateInteval.All:
                    return
                        await
                            _unitOfWork.GetRepository<Bill>()
                                .GetAllAsync(
                                    b => (!isPaid.HasValue || b.IsPaid == isPaid.Value));
                default:
                    throw new ArgumentOutOfRangeException("dateInteval");
            }
        }

        public async Task<Bill> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<Bill>().GetByIdAsync(id);
        }

        public async Task AddAsync(Bill bill, List<UtilitiesItem> utilitiesItems)
        {
            _unitOfWork.GetRepository<Bill>().Add(bill);
            await _unitOfWork.SaveAsync();

            var occupants = await _unitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == bill.ApartmentId);
            var occuantsCount = occupants.Count();

            foreach (var utilitiesItem in utilitiesItems)
            {
                utilitiesItem.BillId = bill.Id;
                await ProcessUtilitiesItemAsync(utilitiesItem, occuantsCount);
            }
            _unitOfWork.GetRepository<UtilitiesItem>().AddRange(utilitiesItems);

            bill.TotalAmount = utilitiesItems.Sum(i => (i.SubsidezedAmount + i.AmountByFullTariff));
            _unitOfWork.GetRepository<Bill>().Update(bill);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Bill bill, List<UtilitiesItem> utilitiesItems)
        {
            if (utilitiesItems != null)
            {
                var occupants = await _unitOfWork.GetRepository<Occupant>().GetAllAsync(o => o.ApartmentId == bill.ApartmentId);
                var occuantsCount = occupants.Count();
                foreach (var utilitiesItem in utilitiesItems)
                {
                    await ProcessUtilitiesItemAsync(utilitiesItem, occuantsCount);

                    if (utilitiesItem.Id > 0)
                    {
                        _unitOfWork.GetRepository<UtilitiesItem>().Update(utilitiesItem);
                    }
                    else
                    {
                        utilitiesItem.BillId = bill.Id;
                        _unitOfWork.GetRepository<UtilitiesItem>().Add(utilitiesItem);
                    }
                }

                bill.TotalAmount = utilitiesItems.Sum(i => (i.SubsidezedAmount + i.AmountByFullTariff));
            }

            _unitOfWork.GetRepository<Bill>().Update(bill);
            await _unitOfWork.SaveAsync();
        }

        private async Task ProcessUtilitiesItemAsync(UtilitiesItem utilitiesItem, int occupantsCount)
        {
            var utilitiesClause = await 
                _unitOfWork.GetRepository<UtilitiesClause>().GetByIdAsync(utilitiesItem.UtilitiesClauseId);

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
    }
}