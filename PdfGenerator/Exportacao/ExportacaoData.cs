#nullable disable

using System.Collections.Generic;

namespace Application.Services.PdfGenerator.Exportacao
{
    public class ExportacaoData
    {
        public int NrPedido { get; set; }
        public decimal VlPedido { get; set; }
        public short? CondicaoVenda { get; set; }
        public string CondPag { get; set; }
        public string Cliente { get; set; }
        public string Nit { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public string CidadeEmbarque { get; set; }
        public string EstadoEmbarque { get; set; }
        public int InvoiceCounter { get; set; }
        public int PalletsNumber { get; set; }
        public string Incoterm { get; set; }
        public IReadOnlyList<Product> Products { get; set; }
    }
}