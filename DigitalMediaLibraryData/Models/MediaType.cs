using System.Collections.Generic;

namespace DigitalMediaLibraryData.Models
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string Name { get; set; }

        public List<Category> Categorys { get; set; }
    }
}
