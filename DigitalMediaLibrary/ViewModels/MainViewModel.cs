using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : PropertyChangedBase , IHandle<object>
    {
        [ImportingConstructor]
        public MainViewModel(DirViewerViewModel dViewerModel, DirExplorerViewModel dExpModel, IEventAggregator events)
        {
            DExpModel = dExpModel;
            DViewerModel = dViewerModel;
            events.Subscribe(this);
        }
        public DirExplorerViewModel DExpModel { get; private set; }
        public DirViewerViewModel DViewerModel { get; private set; }

        public void Handle(object message)
        {
        }
    }
}
