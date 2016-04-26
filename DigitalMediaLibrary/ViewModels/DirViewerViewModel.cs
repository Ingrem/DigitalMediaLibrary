using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;

namespace DigitalMediaLibrary.ViewModels
  {
    [Export(typeof(DirViewerViewModel))]
    public class DirViewerViewModel : PropertyChangedBase , IHandle<DirInfo>
    {
        [ImportingConstructor]
        public DirViewerViewModel(IEventAggregator events)
        {
            DirInfo rootNode = new DirInfo {Path = ""};
            CurrentDirectory = rootNode;
            events.Subscribe(this);
        }

        private IList<DirInfo> _currentItems;
        private DirInfo _currentDirectory;

        public IList<DirInfo> CurrentItems
        {
            get { return _currentItems ?? (_currentItems = new List<DirInfo>()); }
            set
            {
                _currentItems = value;
                NotifyOfPropertyChange(() => CurrentItems);
            }
        }

        private DirInfo CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;

                if (CurrentDirectory.Path != "")
                {
                    IList<FileInfo> childFiles = (from x in Directory.GetFiles(CurrentDirectory.Path)
                        select new FileInfo(x)).ToList();

                    CurrentItems = (from fobj in childFiles
                        select new DirInfo(fobj)).ToList();
                }
            }
        }

        public void Handle(DirInfo message)
        {
            CurrentDirectory = message;
        }
    }
}


