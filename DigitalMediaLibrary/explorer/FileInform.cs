using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DigitalMediaLibrary.explorer
{
    public class FileInform : DependencyObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ExpType { get; set; }

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

        readonly List<string> _audioExt = new List<string>() { ".PCM", ".FLAC", ".WMA-Lossless", ".MP3",
            ".WMA", ".AAC", ".AC3", ".MIDI", ".OGG", ".midi", ".ogg", ".mp3"};
        readonly List<string> _videoExt = new List<string>() { ".avi", ".divx", ".flv", ".h264",
            ".mkv", ".mov", ".mp4", ".mpeg", ".mts", ".mlmp", ".wmv", ".m2t", ".MP4"};
        readonly List<string> _imgExt = new List<string>() { ".crw", ".djvu", ".ico", ".gif",
            ".jp2", ".jpeg", ".jpg", ".png", ".sfw", ".tiff", ".tif"};
    }
}