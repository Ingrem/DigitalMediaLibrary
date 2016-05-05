using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using DigitalMediaLibraryData.Models;

namespace DigitalMediaLibrary.explorer
{
    public class FileInform : DependencyObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ExpType { get; set; }
        public string DateOfCreation { get; set; }
        public string Expansion { get; set; }
        public string Size { get; set; }
        public int CategoryId { get; set; }
        public byte[] FileSourse { get; set; }

        public FileInform() { }

        //for DirExplorer
        public FileInform(FileInfo fileobj)
        {
            Name = fileobj.Name;
            Path = fileobj.FullName;
            DateOfCreation = fileobj.CreationTime.ToString(CultureInfo.InvariantCulture);
            Expansion = fileobj.Extension;
            Size = fileobj.Length.ToString();
            ExpType = "somefile";
            if (_audioExt.Contains(fileobj.Extension))
                ExpType = "audio";
            if (_videoExt.Contains(fileobj.Extension))
                ExpType = "video";
            if (_imgExt.Contains(fileobj.Extension))
                ExpType = "img";
            FileSourse = File.ReadAllBytes(fileobj.FullName);
        }
        //for DB Explorer
        public FileInform(FileInDb fileobj)
        {
            Name = fileobj.Name;
            Path = "DB";
            //Content = fileobj.Content;
            DateOfCreation = fileobj.DateOfCreation;
            Expansion = fileobj.Expansion;
            Size = fileobj.Size;
            CategoryId = fileobj.CategoryId;
            ExpType = "somefile";
            if (_audioExt.Contains(fileobj.Expansion))
                ExpType = "audio";
            if (_videoExt.Contains(fileobj.Expansion))
                ExpType = "video";
            if (_imgExt.Contains(fileobj.Expansion))
                ExpType = "img";
            FileSourse = fileobj.FileSourse;
        }

        readonly List<string> _audioExt = new List<string> { ".PCM", ".FLAC", ".WMA-Lossless", ".MP3",
            ".WMA", ".AAC", ".AC3", ".MIDI", ".OGG", ".midi", ".ogg", ".mp3"};
        readonly List<string> _videoExt = new List<string> { ".avi", ".divx", ".flv", ".h264",
            ".mkv", ".mov", ".mp4", ".mpeg", ".mts", ".mlmp", ".wmv", ".m2t", ".MP4"};
        readonly List<string> _imgExt = new List<string> { ".crw", ".djvu", ".ico", ".gif",
            ".jp2", ".jpeg", ".jpg", ".png", ".sfw", ".tiff", ".tif", ".pdf"};
    }
}