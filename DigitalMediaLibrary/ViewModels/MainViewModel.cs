using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using DigitalMediaLibrary.explorer;
using DigitalMediaLibrary.Views;

namespace DigitalMediaLibrary.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : PropertyChangedBase , IHandle<object>
    {
        [ImportingConstructor]
        public MainViewModel(DirExplorerViewModel dExpModel, IEventAggregator events)
        {
            DExpModel = dExpModel;
            events.Subscribe(this);
        }
        public DirExplorerViewModel DExpModel { get; private set; }

        public void Handle(Object message)
        {
            
        }
    }
}
