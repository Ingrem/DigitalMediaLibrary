using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using DigitalMediaLibrary.ViewModels;
using DigitalMediaLibrary.explorer;

namespace DigitalMediaLibrary.Views
{
    /// <summary>
    /// Interaction logic for DirViewerView.xaml
    /// </summary>
    public partial class DirViewerView
    {
        public DirViewerView()
        {
            InitializeComponent();
        }
        private void SaveInDb_Click(object sender, RoutedEventArgs e)
        {
            ViewModels.DirViewerViewModel.FormingCategoryChoiser();
        }

        private void CurrentItems_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileInform selecItem = (FileInform) CurrentItems.SelectedItem;
            if (selecItem.Path != "DB")
                DirViewerViewModel._events.PublishOnUIThread(new[] { selecItem.Path, selecItem.ExpType, selecItem.Expansion });
            else
            {
                DirViewerViewModel._events.PublishOnUIThread(new[] { selecItem.Path, selecItem.ExpType, selecItem.Expansion });
                DirViewerViewModel._events.PublishOnUIThread(selecItem.FileSourse);
            }
        }
    }
}
