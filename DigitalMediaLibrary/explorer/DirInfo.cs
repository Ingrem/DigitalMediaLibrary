using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DigitalMediaLibrary.explorer
{
    public class DirInfo : DependencyObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Size { get; set; }
        public string Ext { get; set; }
        public int ExpType { get; set; }

        public DirInfo() { }

        public DirInfo(FileInfo fileobj)
        {
            Name = fileobj.Name;
            Path = fileobj.FullName;
            Size = (fileobj.Length / 1024) + " KB";
            Ext = fileobj.Extension + " File";
            ExpType = 3;
            if (_audioExt.Contains(fileobj.Extension))
                ExpType = 2;
            if (_videoExt.Contains(fileobj.Extension))
                ExpType = 1;
            if (_imgExt.Contains(fileobj.Extension))
                ExpType = 0;
        }

        readonly List<string> _audioExt = new List<string>() { ".PCM", ".FLAC", ".WMA-Lossless", ".MP3",
            ".WMA", ".AAC", ".AC3", ".MIDI", ".OGG", ".midi", ".ogg"};
        readonly List<string> _videoExt = new List<string>() { ".avi", ".divx", ".flv", ".h264",
            ".mkv", ".mov", ".mp4", ".mpeg", ".mts", ".mlmp", ".wmv"};
        readonly List<string> _imgExt = new List<string>() { ".crw", ".djvu", ".ico", ".gif",
            ".jp2", ".jpeg", ".jpg", ".png", ".sfw", ".tiff", ".tif"};
    }
}