using System.ComponentModel;

namespace DigitalMediaLibrary.ViewModels
{
    internal class CategoryInDbSelectorModel : INotifyPropertyChanged
    {
        private int _stateId;
        private string _stateName;
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                FirePropertyChanged("IsSelected");
            }
        }

        public int StateId
        {
            get { return _stateId; }
            set
            {
                _stateId = value;
                FirePropertyChanged("StateId");
            }
        }

        public string StateName
        {
            get { return _stateName; }
            set
            {
                _stateName = value;
                FirePropertyChanged("StateName");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
