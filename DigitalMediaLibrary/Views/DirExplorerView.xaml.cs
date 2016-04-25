using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DigitalMediaLibrary.Views
{
    /// <summary>
    ///     Interaction logic for DirExplorerView.xaml
    /// </summary>
    public partial class DirExplorerView
    {
        private readonly object _dummyNode = null;

        public DirExplorerView()
        {
            InitializeComponent();
            foreach (var s in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem {Header = s, Tag = s, FontWeight = FontWeights.Normal};
                item.Items.Add(_dummyNode);
                item.Expanded += folder_Expanded;
                foldersItem.Items.Add(item);
            }
        }

        private string SelectedImagePath { get; set; }

        private void folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem) sender;
            if (item.Items.Count == 1 && item.Items[0] == _dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (var s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        var subitem = new TreeViewItem
                        {
                            Header = s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal) + 1),
                            Tag = s,
                            FontWeight = FontWeights.Normal
                        };
                        subitem.Items.Add(_dummyNode);
                        subitem.Expanded += folder_Expanded;
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = (TreeView) sender;
            var temp = ((TreeViewItem) tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = "";
            var temp2 = "";
            while (true)
            {
                var temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType() == typeof (TreeView))
                {
                    break;
                }
                temp = ((TreeViewItem) temp.Parent);
                temp2 = @"\";
            }
            //selected path // MessageBox.Show(SelectedImagePath);
        }
    }
}