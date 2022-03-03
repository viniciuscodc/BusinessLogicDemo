namespace Application.ZipGenerator
{
    public class File
    {
        public string Name { get; set; }
        public byte[] ByteArray { get; set; }

        public File(string fileName, byte[] byteArray)
        {
            Name = fileName;
            ByteArray = byteArray;
        }
    }
}