#nullable disable

namespace Application.Services.PdfGenerator.Exportacao
{
    public class Product
    {
        public string CdItem { get; set; }
        public string Descricao { get; set; }
        public string Ncm { get; set; }
        public decimal QtPedida { get; set; }
        public decimal NetWeight { get; set; }
        public decimal? NetWeightTotal { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal? GrossWeightTotal { get; set; }
        public decimal? VlUnitario { get; set; }
        public decimal VlTotal { get; set; }
    }
}