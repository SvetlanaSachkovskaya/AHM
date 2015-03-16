using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using AHM.Common.DomainModel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AHM.BusinessLayer
{
    //todo: refactoring
    public class BillPdfGenerator : IBillPdfGenerator
    {
        private readonly IBillService _billService;
        private readonly IUtilitiesItemService _utilitiesItemService;
        private readonly IBuildingService _buildingService;
        private readonly IOccupantService _occupantService;

        private readonly BaseColor _customerEvenRowColor = new BaseColor(238, 238, 238);
        private readonly BaseColor _customerHeaderColor = new BaseColor(204, 204, 204);
        private readonly Font _middleTextBoldStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD);
        private readonly Font _smallTextBoldStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11, Font.BOLD);
        private readonly Font _smallTextStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11, Font.NORMAL);


        public BillPdfGenerator(
            IBillService billService,
            IUtilitiesItemService utilitiesItemService,
            IBuildingService buildingService,
            IOccupantService occupantService)
        {
            _billService = billService;
            _utilitiesItemService = utilitiesItemService;
            _buildingService = buildingService;
            _occupantService = occupantService;
        }


        public async Task<string> GenerateAsync(int billId, string directoryPath)
        {
            var bill = await _billService.GetByIdAsync(billId);
            var utilitiesItems = await _utilitiesItemService.GetByBillIdAsync(billId);
            var building = await _buildingService.GetBuildingByIdAsync(bill.Apartment.BuildingId);
            var owner = await _occupantService.GetApartmentOwnerAsync(bill.ApartmentId);
            var occupants = await _occupantService.GetOccupantsByApartmentIdAsync(bill.ApartmentId);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = GetBillFileName(bill);
            var filePath = Path.Combine(directoryPath, fileName);

            var fileStream = new FileStream(filePath, FileMode.Create);
            var pdfDocument = new Document(PageSize.A4, -50, -50, 20, 100);
            var writer = PdfWriter.GetInstance(pdfDocument, fileStream);

            pdfDocument.Open();

            var headerTable = new PdfPTable(2);
            var mainTable = new PdfPTable(8);

            headerTable.SetWidths(new[] { 1f, 3f });

            //header
            var monthCell = new PdfPCell(new Phrase(bill.Date.ToString("MMMM yyyy"))) { Rowspan = 5 };
            headerTable.AddCell(monthCell);
            headerTable.AddCell(String.Format("Address: {0} {1} {2} {3}", building.State, building.City, building.Street, building.Number));
            headerTable.AddCell(String.Format("Payer: {0}", owner != null ? owner.Name : String.Empty));
            headerTable.AddCell(String.Format("Count of occupants: {0}", occupants.Count()));
            headerTable.AddCell(String.Format("Living space: {0}", bill.Apartment.Square));
            headerTable.AddCell(String.Format("Personal account: {0}", bill.Apartment.PersonalAccount));

            //main content
            mainTable.SetWidths(new[] { 3f, 2f, 2f, 2f, 2f, 2f, 3f, 3f });

            mainTable.AddCell(new PdfPCell(new Phrase("Name", _middleTextBoldStyle)) {BackgroundColor = _customerHeaderColor});
            mainTable.AddCell(new PdfPCell(new Phrase("Measure", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });
            mainTable.AddCell(new PdfPCell(new Phrase("Quantity", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });
            mainTable.AddCell(new PdfPCell(new Phrase("Tariff*", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });
            mainTable.AddCell(new PdfPCell(new Phrase("Tariff**", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });
            mainTable.AddCell(new PdfPCell(new Phrase("Amount*", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });
            mainTable.AddCell(new PdfPCell(new Phrase("Amount**", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });
            mainTable.AddCell(new PdfPCell(new Phrase("Total", _middleTextBoldStyle)) { BackgroundColor = _customerHeaderColor });

            foreach (var utilitiesItem in utilitiesItems)
            {
                mainTable.AddCell(utilitiesItem.UtilitiesClause.Name);
                mainTable.AddCell(utilitiesItem.UtilitiesClause.Measure);
                mainTable.AddCell(new PdfPCell(new Phrase(utilitiesItem.Quantity.ToString("F"), _smallTextStyle)));
                mainTable.AddCell(new PdfPCell(new Phrase(utilitiesItem.UtilitiesClause.SubsidizedTariff.ToString("N"), _smallTextStyle)));
                mainTable.AddCell(new PdfPCell(new Phrase(utilitiesItem.UtilitiesClause.FullTariff.ToString("N"), _smallTextStyle)));
                mainTable.AddCell(new PdfPCell(new Phrase(utilitiesItem.SubsidezedAmount.ToString("N"), _smallTextStyle)));
                mainTable.AddCell(new PdfPCell(new Phrase(utilitiesItem.AmountByFullTariff.ToString("N"), _smallTextStyle)));
                mainTable.AddCell(new PdfPCell(new Phrase((utilitiesItem.AmountByFullTariff + utilitiesItem.SubsidezedAmount).ToString("N"), _smallTextStyle)));
            }

            //totals
            var subsidizedTotal = utilitiesItems.Sum(i => i.SubsidezedAmount);
            var totalByFullTariff = utilitiesItems.Sum(i => i.AmountByFullTariff);

            var totals = new PdfPCell(new Phrase("Totals:", _middleTextBoldStyle)) { Colspan = 5 };
            mainTable.AddCell(totals);
            mainTable.AddCell(new PdfPCell(new Phrase(subsidizedTotal.ToString("N"), _smallTextBoldStyle)));
            mainTable.AddCell(new PdfPCell(new Phrase(totalByFullTariff.ToString("N"), _smallTextBoldStyle)));
            mainTable.AddCell(new PdfPCell(new Phrase((subsidizedTotal + totalByFullTariff).ToString("N"), _smallTextBoldStyle)));

            pdfDocument.Add(headerTable);
            pdfDocument.Add(mainTable);

            writer.Flush();
            pdfDocument.Close();
            fileStream.Close();

            return filePath;
        }


        private string GetBillFileName(Bill bill)
        {
            return String.Format("{0}_{1}({2}-{3}).pdf", bill.ApartmentId, bill.Apartment.Name,
                bill.Date.ToString("MMMM"), bill.Date.Year);
        }
    }
}