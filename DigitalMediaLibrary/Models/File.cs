﻿namespace DigitalMediaLibrary.Models
{
    public class File
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public string Expansion { get; set; }
        public string DateOfCreation { get; set; }
        public string Size { get; set; }
        public object Content { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}