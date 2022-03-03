using WkHtmlToPdfDotNet;

namespace Application.Services.PdfGenerator
{
    public class Templates
    {
        public HtmlToPdfDocument DefaultTemplate(string html)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                DPI = 96,

                Margins = new MarginSettings() {
                    Unit = Unit.Centimeters,
                    Top = 2,
                    Right = 2,
                    Bottom = 2,
                    Left = 2
                }
            },
                Objects = {
                    new ObjectSettings()
                    {
                        WebSettings = {
                            DefaultEncoding = "utf-8",
                            EnableIntelligentShrinking = true,
                        },
                        LoadSettings = {
                            ZoomFactor = 0.9
                        },
                        HtmlContent = html,
                    }
                }
            };

            return doc;
        }
    }
}