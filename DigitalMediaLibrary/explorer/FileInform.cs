using System.Collections.Generic;
using System.IO;
using System.Windows;
using File = DigitalMediaLibrary.Models.File;

namespace DigitalMediaLibrary.explorer
{
    public class FileInform : DependencyObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ExpType { get; set; }

        public object Content { get; set; }

        public FileInform() { }

        public FileInform(FileInfo fileobj)
        {
            Name = fileobj.Name;
            Path = fileobj.FullName;
            ExpType = "somefile";
            if (_audioExt.Contains(fileobj.Extension))
                ExpType = "audio";
            if (_videoExt.Contains(fileobj.Extension))
                ExpType = "video";
            if (_imgExt.Contains(fileobj.Extension))
                ExpType = "img";
        }

        public FileInform(File fileobj)
        {
            Name = fileobj.Name;
            Content = fileobj.Content;
            ExpType = "somefile";
            if (_audioExt.Contains(fileobj.Expansion))
                ExpType = "audio";
            if (_videoExt.Contains(fileobj.Expansion))
                ExpType = "video";
            if (_imgExt.Contains(fileobj.Expansion))
                ExpType = "img";
        }

        readonly List<string> _audioExt = new List<string>() { ".PCM", ".FLAC", ".WMA-Lossless", ".MP3",
            ".WMA", ".AAC", ".AC3", ".MIDI", ".OGG", ".midi", ".ogg", ".mp3"};
        readonly List<string> _videoExt = new List<string>() { ".avi", ".divx", ".flv", ".h264",
            ".mkv", ".mov", ".mp4", ".mpeg", ".mts", ".mlmp", ".wmv", ".m2t", ".MP4"};
        readonly List<string> _imgExt = new List<string>() { ".crw", ".djvu", ".ico", ".gif",
            ".jp2", ".jpeg", ".jpg", ".png", ".sfw", ".tiff", ".tif", ".pdf"};
    }
}