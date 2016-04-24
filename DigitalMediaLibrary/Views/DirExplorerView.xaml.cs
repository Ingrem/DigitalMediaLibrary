using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DigitalMediaLibrary.Views
{
    /// <summary>
    ///     Interaction logic for DirExplorerView.xaml
    /// </summary>
    public partial class DirExplorerView : UserControl
    {
        private readonly object dummyNode = null;

        public DirExplorerView()
        {
            InitializeComponent();
            foreach (var s in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += folder_Expanded;
                foldersItem.Items.Add(item);
            }
        }

        public string SelectedImagePath { get; set; }

        private void folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem) sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (var s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        var subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += folder_Expanded;
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception)
                {
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
            var temp1 = "";
            var temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof (TreeView)))
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