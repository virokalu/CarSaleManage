using CarSaleManage.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CarSaleManage.Services
{
    public class PDFService
    {
        public byte[] GenerateReceiptPdf(AppUser user, Vehicle vehicle)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content()
                        .Column(col =>
                        {
                            col.Item().Text("Purchase Receipt").FontSize(20).Bold();
                            col.Item().Text($"Buyer: {user.Firstname} {user.Lastname}");
                            col.Item().Text($"Email: {user.Email}");
                            col.Item().Text($"Vehicle: {vehicle.Make} {vehicle.ModelNo}");
                            col.Item().Text($"Purchase Date: {vehicle.PurchaseDate?.ToShortDateString()}");
                            // Add more details as needed
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
