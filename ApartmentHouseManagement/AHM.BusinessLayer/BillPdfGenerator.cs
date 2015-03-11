using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AHM.BusinessLayer.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AHM.WebAPI.Helper
{
    //todo: refactoring
    public class BillPdfGenerator
    {
        private readonly IBillService _billService;
        private readonly IUtilitiesItemService _utilitiesItemService;
        private readonly IBuildingService _buildingService;
        private readonly IOccupantService _occupantService;

        private readonly BaseColor _customerEvenRowColor = new BaseColor(238, 238, 238);
        private readonly BaseColor _customerHeaderColor = new BaseColor(204, 204, 204);
        private readonly Font _middleTextBoldStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.BOLD);
        private readonly Font _smallTextBoldStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, Font.BOLD);
        private readonly Font _smallTextStyle = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, Font.NORMAL);


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


        public async Task<string> Generate(int billId)
        {
            var bill = await _billService.GetByIdAsync(billId);
            var utilitiesItems = await _utilitiesItemService.GetByBillIdAsync(billId);
            var building = await _buildingService.GetBuildingByIdAsync(bill.Apartment.BuildingId);
            var owner = await _occupantService.GetApartmentOwnerAsync(bill.ApartmentId);
            var occupants = await _occupantService.GetOccupantsByApartmentIdAsync(bill.ApartmentId);

            var documentsDirectory = ConfigurationManager.AppSettings["BillsDirectory"];
            if (!Directory.Exists(documentsDirectory))
            {
                Directory.CreateDirectory(documentsDirectory);
            }

            var fileName = String.Format("{0}-{1}({2}, {3})", bill.Apartment.BuildingId, bill.ApartmentId,
                bill.Date.Month, bill.Date.Year);
            var filePath = Path.Combine(documentsDirectory, fileName);

            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            var pdfDocument = new Document(PageSize.A4, -50, -50, 20, 100);

            pdfDocument.Open();

            var headerTable = new PdfPTable(2);
            var mainTable = new PdfPTable(8);

            headerTable.SetWidths(new[] { 1f, 2f });

            //header
            var monthCell = new PdfPCell(new Phrase(bill.Date.ToString("MMMM yyyy"))) { Rowspan = 5 };
            headerTable.AddCell(monthCell);
            headerTable.AddCell(String.Format("Address: {0} {1} {2} {3}", building.State, building.City, building.Street, building.Number));
            headerTable.AddCell(String.Format("Payer: {0}", owner != null ? owner.Name : String.Empty));
            headerTable.AddCell(String.Format("Count of occupants: {0}", occupants.Count()));
            headerTable.AddCell(String.Format("Living space: {0}", bill.Apartment.Square));
            headerTable.AddCell(String.Format("Зersonal account: {0}", 123456));

            //main content
            mainTable.SetWidths(new[] { 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f });

            mainTable.AddCell("Name");
            mainTable.AddCell("Measure");
            mainTable.AddCell("Quantity");
            mainTable.AddCell("Tariff*");
            mainTable.AddCell("Tariff**");
            mainTable.AddCell("Amount*");
            mainTable.AddCell("Amount**");
            mainTable.AddCell("Total");

            foreach (var utilitiesItem in utilitiesItems)
            {
                mainTable.AddCell(utilitiesItem.UtilitiesClause.Name);
                mainTable.AddCell(utilitiesItem.UtilitiesClause.Measure);
                mainTable.AddCell(utilitiesItem.Quantity.ToString("F"));
                mainTable.AddCell(utilitiesItem.UtilitiesClause.SubsidizedTariff.ToString("N"));
                mainTable.AddCell(utilitiesItem.UtilitiesClause.FullTariff.ToString("N"));
                mainTable.AddCell(utilitiesItem.SubsidezedAmount.ToString("N"));
                mainTable.AddCell(utilitiesItem.AmountByFullTariff.ToString("N"));
                mainTable.AddCell((utilitiesItem.AmountByFullTariff + utilitiesItem.SubsidezedAmount).ToString("N"));
            }


            //totals
            var subsidizedTotal = utilitiesItems.Sum(i => i.SubsidezedAmount);
            var totalByFullTariff = utilitiesItems.Sum(i => i.AmountByFullTariff);

            var totals = new PdfPCell(new Phrase("Totals:")) { Colspan = 5 };
            mainTable.AddCell(totals);
            mainTable.AddCell(subsidizedTotal.ToString("N"));
            mainTable.AddCell(totalByFullTariff.ToString("N"));
            mainTable.AddCell((subsidizedTotal + totalByFullTariff).ToString("N"));

            pdfDocument.Add(headerTable);
            pdfDocument.Add(mainTable);

            pdfDocument.Close();

            return filePath;
        }
    }
}