using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DigitalMediaLibrary.ViewModels
{
    class CategoryInDbSelectorViewModel : INotifyPropertyChanged
    {
        private List<CategoryInDbSelectorModel> _states;

        public List<CategoryInDbSelectorModel> States
        {
            get { return _states; }
            set { _states = value; FirePropertyChanged("States"); }
        }

        public CategoryInDbSelectorViewModel()
        {
            int i = 0;
            States = new List<CategoryInDbSelectorModel>();
            foreach (var u in DirViewerViewModel.CategoriesInDb)
            {
                States.Add(
                    new CategoryInDbSelectorModel
                    {
                        StateName = u,
                        StateId = i
                    });
                i++;
            }
        }

        public void Save()
        {
            foreach (var u in States.Where(u => u.IsSelected))
                DirViewerViewModel.JustSaveInDb(u.StateName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
