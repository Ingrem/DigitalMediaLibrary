using System.Windows.Controls;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof (DirExplorerViewModel))]
    public class DirExplorerViewModel : UserControl
    {
        private readonly IEventAggregator _events;

        [ImportingConstructor]
        public DirExplorerViewModel(IEventAggregator events)
        {
            _events = events;
        }
    }
}
