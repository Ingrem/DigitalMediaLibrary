using System.ComponentModel.Composition;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof (DirExplorerViewModel))]
    public class DirExplorerViewModel 
    {
        [ImportingConstructor]
        public DirExplorerViewModel(IEventAggregator events)
        {
            _events = events;
        }

        private static IEventAggregator _events;
        public static readonly HeaderToImageConverter Instance = new HeaderToImageConverter();

        public static void ChangeDirEvent(string selectedImagePath)
        {
            _events.PublishOnUIThread(new FileInform { Path = selectedImagePath });
        }
    }
}
