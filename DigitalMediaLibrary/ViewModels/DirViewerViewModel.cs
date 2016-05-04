using System;
using System.Windows;
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
        private static FileInform _selecItem;

        // Select item and play it
        public FileInform SelecItem
        {
            get { return _selecItem; }
            set
            {
                if (value != null)
                {
                    _selecItem = value;
                    if (_selecItem.Path !="DB")
                        _events.PublishOnUIThread(new[] { _selecItem.Path, _selecItem.ExpType});
                }
            }
        }
        //List for GUI
        public IList<FileInform> CurrentItems
        {
            get { return _currentItems ?? (_currentItems = new List<FileInform>()); }
            set
            {
                _currentItems = value;
                NotifyOfPropertyChange(() => CurrentItems);
            }
        }
        //Take files from dir in dirTreeview and add it to CurrentItems list
        private FileInform CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;

                try
                {
                    if (CurrentDirectory.Path != "DB" & CurrentDirectory.Path != "")
                    {
                        IList<FileInfo> childFiles = (from x in Directory.GetFiles(CurrentDirectory.Path)
                                                      select new FileInfo(x)).ToList();

                        CurrentItems = (from fobj in childFiles
                                        select new FileInform(fobj)).ToList();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Доступ запрещён");
                }
            }
        }
        //Take files from db in selected category and add it to CurrentItems list
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
        // Save in db
        public static void SaveInDb()
        {
            if (_selecItem.Path != "DB")
                using (LibraryContext db = new LibraryContext())
                {
                    var category = db.Categorys.FirstOrDefault(s => s.Name == "Ню");
                    if (category != null)
                    {
                        category.Files = new List<FileInDb>
                        {
                            new FileInDb
                            {
                                Name = _selecItem.Name,
                                CategoryId = category.CategoryId,
                                DateOfCreation = _selecItem.DateOfCreation,
                                Expansion = _selecItem.Expansion,
                                Size = _selecItem.Size
                            }
                        };
                    }
                    db.SaveChanges();
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


