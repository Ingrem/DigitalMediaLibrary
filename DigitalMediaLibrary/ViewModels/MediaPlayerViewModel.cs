using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof(MediaPlayerViewModel))]
    public class MediaPlayerViewModel : IHandle<string>
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

        private Uri _currentMediaUri;
        public List<MediaElement> Media { get; set; }
        private Uri CurrentMediaUri
        {
            get { return _currentMediaUri; }
            set
            {
                _currentMediaUri = value;
                Media[0].Source = _currentMediaUri;
            }
            
        }

        public void Start()
        {
            if (Media[0] != null)
            {
                Media[0].Play();
                Media[0].Visibility = Visibility.Visible;
            }
        }

        public void Stop()
        {
            if (Media[0] != null)
            {
                Media[0].Stop();
                Media[0].Visibility = Visibility.Hidden;
            }
        }

        public void Pause()
        {
            Media[0]?.Pause();
        }

        public void Handle(string message)
        {
            CurrentMediaUri = new Uri(message);
            Start();
        }
    }
}
