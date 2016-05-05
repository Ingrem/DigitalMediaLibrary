namespace DigitalMediaLibraryData.Models
{
    public class FileInDb
    {
        public int FileInDbId { get; set; }
        public string Name { get; set; }
        public string Expansion { get; set; }
        public string DateOfCreation { get; set; }
        public string Size { get; set; }
        public byte[] FileSourse { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
