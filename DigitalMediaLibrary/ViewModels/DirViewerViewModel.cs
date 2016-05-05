using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;
using DigitalMediaLibraryData.Models;
using DigitalMediaLibrary.Views;

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
        public static List<string> CategoriesInDb;
        private static CategoryInDbSelector _choiseCategory;

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
                    else
                    {
                        _events.PublishOnUIThread(_selecItem.FileSourse);
                    }
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
        // Save in db show window
        public static void FormingCategoryChoiser()
        {
            if (_selecItem.Path != "DB")
                using (LibraryContext db = new LibraryContext())
                {
                    int mediaType;
                    switch (_selecItem.ExpType)
                    {
                        case "audio":
                            mediaType = 1;
                            break;
                        case "video":
                            mediaType = 2;
                            break;
                        case "img":
                            mediaType = 3;
                            break;
                        default:
                            mediaType = 4;
                            break;
                    }
                    if (mediaType != 4)
                    {
                        CategoriesInDb = new List<string>();
                        var categorys = db.Categorys.ToList();
                        foreach (var u in categorys.Where(u => u.MediaTypeId == mediaType))
                        {
                            CategoriesInDb.Add(u.Name);
                        }
                        _choiseCategory = new CategoryInDbSelector();
                        _choiseCategory.ShowDialog();
                    }
                }
        }
        // Save in db
        public static void JustSaveInDb(string categoryName)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var category = db.Categorys.FirstOrDefault(s => s.Name == categoryName);
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
                            Size = _selecItem.Size,
                            FileSourse = _selecItem.FileSourse
                        }
                    };
                }
                db.SaveChanges();
            }
            _choiseCategory.Close();
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


