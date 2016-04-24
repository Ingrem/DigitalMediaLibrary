using System.ComponentModel.Composition;
using System.Windows.Media;
using Caliburn.Micro;

namespace DigitalMediaLibrary.explorer
{
    [Export(typeof(ColorViewModel))]
    public class ColorViewModel
    {
        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public ColorViewModel(IEventAggregator events)
        {
            _events = events;
        }
        public void Red()
        {
            _events.PublishOnUIThread(new ColorEvent(new SolidColorBrush(Colors.Red)));
        }

        public void Green()
        {
            _events.PublishOnUIThread(new ColorEvent(new SolidColorBrush(Colors.Green)));
        }

        public void Blue()
        {
            _events.PublishOnUIThread(new ColorEvent(new SolidColorBrush(Colors.Blue)));
        }
    }
    public class ColorEvent
    {
        public SolidColorBrush Color { get; private set; }
        public ColorEvent(SolidColorBrush color)
        {
            Color = color;
        }
    }
    /* Use in Main

   private SolidColorBrush _color = new SolidColorBrush(Colors.Blue);

   public SolidColorBrush Color
   {
       get { return _color; }
       set
       {
           _color = value;
           NotifyOfPropertyChange(() => Color);
       }
   }
   public void Handle(ColorEvent message)
   {
       Color = message.Color;
   }
   */
}
