using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Application.ZipGenerator
{
    public class ZipHandler : IZipHandler
    {
        public List<File> Files { get; set; }

        public ZipHandler()
        {
            this.Files = new List<File>();
        }

        public void AddFile(byte[] fileArray, string fileName)
        {
            Files.Add(new File(fileName, fileArray));
        }

        public byte[] GenerateZipFile()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in Files)
                    {
                        using (var zipEntryStream = archive.CreateEntry(file.Name).Open())
                        {
                            zipEntryStream.Write(file.ByteArray);
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public string GenerateInvoiceZipName(string invoiceNumber)
        {
            var zipName = "Invoice" + "_" + invoiceNumber + ".zip";

            return zipName;
        }
    }
}