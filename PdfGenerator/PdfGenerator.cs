using WkHtmlToPdfDotNet.Contracts;

namespace Application.Services.PdfGenerator
{
    //Strategy Implementation
    public class PdfGenerator : Templates, IPdfGenerator
    {
        private readonly IConverter _converter;
        private HtmlBuilder _pdfType;
        public bool Success { get; private set; } = true;

        public PdfGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public void SetPdfType(HtmlBuilder pdfType)
        {
            this._pdfType = pdfType;
        }

        public byte[] GeneratePdf()
        {
            var pdf = _converter.Convert(DefaultTemplate(_pdfType.Html));

            if (Success != false)
            {
                Success = _pdfType.Success;
            }

            return pdf;
        }
    }
}