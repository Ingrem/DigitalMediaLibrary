using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : PropertyChangedBase , IHandle<object>
    {
        [ImportingConstructor]
        public MainViewModel(MediaPlayerViewModel mPlayerModel, DirViewerViewModel dViewerModel, DirExplorerViewModel dExpModel,
            IEventAggregator events)
        {
            DExpModel = dExpModel;
            DViewerModel = dViewerModel;
            MPlayerModel = mPlayerModel;
            events.Subscribe(this);
        }
        public DirExplorerViewModel DExpModel { get; private set; }
        public DirViewerViewModel DViewerModel { get; private set; }
        public MediaPlayerViewModel MPlayerModel { get; private set; }

        public void Handle(object message)
        {
        }
    }
}
