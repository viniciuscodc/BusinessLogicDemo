namespace Application.Services.PdfGenerator
{
    public interface IPdfGenerator
    {
        public byte[] GeneratePdf();

        public void SetPdfType(HtmlBuilder pdfType);

        public bool Success { get; }
    }
}