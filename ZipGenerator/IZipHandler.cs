namespace Application.ZipGenerator
{
    public interface IZipHandler
    {
        public void AddFile(byte[] fileArray, string fileName);

        public byte[] GenerateZipFile();

        public string GenerateInvoiceZipName(string invoiceNumber);
    }
}