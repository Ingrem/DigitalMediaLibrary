using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;
using DigitalMediaLibrary.Models;

namespace DigitalMediaLibrary.ViewModels
  {
    [Export(typeof(DirViewerViewModel))]
    public class DirViewerViewModel : PropertyChangedBase , IHandle<FileInform>
    {
        [ImportingConstructor]
        public DirViewerViewModel(IEventAggregator events)
        {
            FileInform rootNode = new FileInform {Path = ""};
            CurrentDirectory = rootNode;
            _events = events;
            events.Subscribe(this);
        }

        private static IEventAggregator _events;
        private IList<FileInform> _currentItems;
        private FileInform _currentDirectory;
        private FileInform _currentCategoryFromDb;
        private FileInform _selecItem;

        public FileInform SelecItem
        {
            get { return _selecItem; }
            set
            {
                if (value != null)
                {
                    _selecItem = value;
                    if (_selecItem.Path !="")
                        _events.PublishOnUIThread(new[] { _selecItem.Path, _selecItem.ExpType});
                }
            }
        }

        public IList<FileInform> CurrentItems
        {
            get { return _currentItems ?? (_currentItems = new List<FileInform>()); }
            set
            {
                _currentItems = value;
                NotifyOfPropertyChange(() => CurrentItems);
            }
        }

        private FileInform CurrentDirectory
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
                        select new FileInform(fobj)).ToList();
                }
            }
        }

        private FileInform CurrentCategoryFromDb
        {
            get { return _currentCategoryFromDb; }
            set
            {
                _currentCategoryFromDb = value;

                var tmpCurrentItems = new List<FileInform>();
                using (LibraryContext db = new LibraryContext())
                {
                    var categorys = db.Categorys.ToList();
                    var files = db.Files.ToList();
                    tmpCurrentItems.AddRange(
                        from u in categorys
                        where u.Name == _currentCategoryFromDb.Name
                        where u.Files != null
                        from w in u.Files
                        select new FileInform(w));
                }
                CurrentItems = tmpCurrentItems;
            }
        }

        public void Handle(FileInform message)
        {
            if (message.Path!="DB")
                CurrentDirectory = message;
            else
            {
                CurrentCategoryFromDb = message;
            }
        }
    }
}


