using System.ComponentModel.Composition;
using System.Windows;
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

        #region Visibility

        private Visibility _expVisibility = Visibility.Visible;
        private Visibility _viewerVisibility = Visibility.Visible;
        private Visibility _playerVisibility = Visibility.Visible;

        public Visibility ExpVisibility
        {
            get { return _expVisibility; }
            set
            {
                _expVisibility = value;
                NotifyOfPropertyChange(() => ExpVisibility);
            }
        }
        public Visibility ViewerVisibility
        {
            get { return _viewerVisibility; }
            set
            {
                _viewerVisibility = value;
                NotifyOfPropertyChange(() => ViewerVisibility);
            }
        }
        public Visibility PlayerVisibility
        {
            get { return _playerVisibility; }
            set
            {
                _playerVisibility = value;
                NotifyOfPropertyChange(() => PlayerVisibility);
            }
        }

        #endregion

        #region Width
        private GridLength _viewerWidth = new GridLength(SystemParameters.WorkArea.Width / 2, GridUnitType.Star);
        private GridLength _playerWidth = new GridLength(SystemParameters.WorkArea.Width / 2, GridUnitType.Star);

        public GridLength ViewerWidth
        {
            get { return _viewerWidth; }
            set
            {
                _viewerWidth = value;
                NotifyOfPropertyChange(() => ViewerWidth);
            }
        }
        public GridLength PlayerWidth
        {
            get { return _playerWidth; }
            set
            {
                _playerWidth = value;
                NotifyOfPropertyChange(() => PlayerWidth);
            }
        }
        #endregion

        #region minimize

        public void ExpMinimize()
        {
            ExpVisibility = _expVisibility == Visibility.Visible 
                ? Visibility.Collapsed 
                : Visibility.Visible;
        }
        public void ViewerMinimize()
        {
            ViewerVisibility = _viewerVisibility == Visibility.Visible 
                ? Visibility.Collapsed 
                : Visibility.Visible;
            ViewerWidth = _viewerWidth.GridUnitType == GridUnitType.Auto
                ? new GridLength(550, GridUnitType.Star)
                : new GridLength(550, GridUnitType.Auto);
        }
        public void PlayerMinimize()
        {
            PlayerVisibility = _playerVisibility == Visibility.Visible 
                ? Visibility.Collapsed 
                : Visibility.Visible;
            PlayerWidth = _playerWidth.GridUnitType == GridUnitType.Auto
                ? new GridLength(500, GridUnitType.Star)
                : new GridLength(500, GridUnitType.Auto);
        }

        #endregion

        public void Handle(object message)
        {
        }
    }
}
