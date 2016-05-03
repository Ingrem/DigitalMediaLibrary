using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;
using DigitalMediaLibrary.Models;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof (DirExplorerViewModel))]
    public class DirExplorerViewModel : PropertyChangedBase
    {
        [ImportingConstructor]
        public DirExplorerViewModel(IEventAggregator events)
        {
            _events = events;
        }

        private static IEventAggregator _events;
        public static readonly HeaderToImageConverter Instance = new HeaderToImageConverter();

        private List<MediaType> _treeViewSource = new List<MediaType>();

        public List<MediaType> TreeViewSource
        {
            get
            {
                using (LibraryContext db = new LibraryContext())
                {
                    var categorys = db.Categorys.ToList();
                    var tmpMediaType = db.MediaTypes.ToList();
                    foreach (MediaType u in tmpMediaType)
                        _treeViewSource.Add(u);
                }
                return _treeViewSource;
            }
            set
            {
                if (_treeViewSource != value)
                {
                    _treeViewSource = value;
                    NotifyOfPropertyChange(() => TreeViewSource);
                }
            }
        }

        public static void SelectedinDbChanged(string categoryName)
        {
            _events.PublishOnUIThread(new FileInform {Name = categoryName, Path = "DB"});
        }

        public static void ChangeDirEvent(string selectedImagePath)
        {
            _events.PublishOnUIThread(new FileInform { Path = selectedImagePath });
        }
    }
}
