using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace feedbackFlowAPI.Services.Implementations { 
    public class PdfService: IDocument
    {
    public string Title { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new List<string>();

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12).FontFamily(Fonts.Verdana));

            // 1. FORSIDE
            page.Content().Column(column =>
            {
                column.Item().PaddingTop(10, Unit.Centimetre).AlignCenter().Text(Title).FontSize(30).Bold();
                column.Item().AlignCenter().Text("Opgavesæt genereret via FeedbackFlow").FontSize(16).Italic();

                // Spring til næste side efter forsiden
                column.Item().PageBreak();

                // 2. OPGAVER (Billeder)
                foreach (var url in ImageUrls)
                {
                    column.Item().PaddingBottom(1, Unit.Centimetre).Column(opgaveCol =>
                    {
                        opgaveCol.Spacing(10);
                        opgaveCol.Item().Text($"Opgave:").Bold().FontSize(14);

                        // QuestPDF kan hente billedet direkte fra din MinIO URL
                        opgaveCol.Item().Image(url);

                        opgaveCol.Item().PageBreak(); // Ny side for hver opgave
                    });
                }
            });

            page.Footer().AlignCenter().Text(x =>
            {
                x.Span("Side ");
                x.CurrentPageNumber();
            });
        });
    }
}
}