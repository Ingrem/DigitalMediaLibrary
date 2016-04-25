using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;

namespace DigitalMediaLibrary.ViewModels
  {
    [Export(typeof(DirViewerViewModel))]
    public class DirViewerViewModel : UserControl
      {
        private readonly IEventAggregator _events;

        [ImportingConstructor]
        public DirViewerViewModel(IEventAggregator events)
        {
            DirInfo rootNode = new DirInfo {Path = "D:/Games/Art"};
            CurrentDirectory = rootNode;
            _events = events;
        }
        private IList<DirInfo> _currentItems;
        private DirInfo _currentDirectory;
        public IList<DirInfo> CurrentItems
        {
            get { return _currentItems ?? (_currentItems = new List<DirInfo>()); }
            set
            {
                _currentItems = value;
             //   OnPropertyChanged("CurrentItems");
            }
        }

        private DirInfo CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;

                IList<FileInfo> childFiles = (from x in Directory.GetFiles(CurrentDirectory.Path)
                                                 select new FileInfo(x)).ToList();

                CurrentItems = (from fobj in childFiles
                                select new DirInfo(fobj)).ToList();

                //  OnPropertyChanged("CurrentDirectory");
            }
        }
    }

    /// <summary>
    /// Class for containing the information about a Directory/File
    /// </summary>
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


