using System.Data.Entity;

namespace DigitalMediaLibraryData.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("DbConnection")
        {
        }

        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<FileInDb> Files { get; set; }
    }
}
