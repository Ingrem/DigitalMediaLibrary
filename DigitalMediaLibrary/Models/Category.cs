

using System.Collections.Generic;

namespace DigitalMediaLibrary.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public int MediaTypeId { get; set; }

        public MediaType MediaType { get; set; }

        public List<File> Files { get; set; }
    }
}
