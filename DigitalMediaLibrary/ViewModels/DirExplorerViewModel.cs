using System.ComponentModel.Composition;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;

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

        public static readonly HeaderToImageConverter Instance = new HeaderToImageConverter();
        private static IEventAggregator _events;

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
