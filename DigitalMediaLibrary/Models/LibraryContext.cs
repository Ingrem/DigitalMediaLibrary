using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DigitalMediaLibrary.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("DbConnection")
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
        }

        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
