using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof(MediaPlayerViewModel))]
    public class MediaPlayerViewModel : PropertyChangedBase, IHandle<string[]>
    {
        [ImportingConstructor]
        public MediaPlayerViewModel(IEventAggregator events)
        {
            events.Subscribe(this);

            Media = new List<MediaElement>
            {
                new MediaElement
                {
                    LoadedBehavior = MediaState.Manual
                }
            };
        }

        private string _currentMediaType;
        public List<MediaElement> Media { get; set; }

        #region Visibility

        private Visibility _allPlayerVisibility = Visibility.Collapsed;
        private Visibility _buttonsVisibility = Visibility.Collapsed;

        public Visibility AllPlayerVisibility
        {
            get { return _allPlayerVisibility; }
            set
            {
                _allPlayerVisibility = value;
                NotifyOfPropertyChange(() => AllPlayerVisibility);
            }
        }

        public Visibility ButtonsVisibility
        {
            get { return _buttonsVisibility; }
            set
            {
                _buttonsVisibility = value;
                NotifyOfPropertyChange(() => ButtonsVisibility);
            }
        }

        #endregion

        #region Buttons: Start, Stop, Pause

        public void Start()
        {
            if (Media[0] != null)
            {
                Media[0].Play();
                AllPlayerVisibility = Visibility.Visible;
                if (_currentMediaType == "video" || _currentMediaType == "audio")
                    ButtonsVisibility = Visibility.Visible;
                else
                    ButtonsVisibility = Visibility.Collapsed;
            }
        }

        public void Stop()
        {
            if (Media[0] != null)
            {
                Media[0].Stop();
                AllPlayerVisibility = Visibility.Collapsed;
            }
        }

        public void Pause()
        {
            Media[0]?.Pause();
        }

        #endregion

        public void Handle(string[] message)
        {
            Media[0].Source = new Uri(message[0]);
            _currentMediaType = message[1];
            Start();
        }
    }
}
