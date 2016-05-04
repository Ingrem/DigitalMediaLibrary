using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using DigitalMediaLibrary.Models;
using DigitalMediaLibrary.ViewModels;

namespace DigitalMediaLibrary.Views
{
    /// <summary>
    ///     Interaction logic for DirExplorerView.xaml
    /// </summary>
    public partial class DirExplorerView
    {
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
            DeserializeTreeView(Properties.Resources.SaveExplorerXML);
            Dispatcher.ShutdownStarted += Window_Closing;
        }

        private readonly object _dummyNode = null;
        private string SelectedImagePath { get; set; }
        //Create tree nodes from dir treeview
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
        // Select from dir TreeView
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
            DirExplorerViewModel.ChangeDirEvent(SelectedImagePath);
        }
        // Select from DB
        private void SelectedChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = (TreeView)sender;
            try
            {
                var temp = ((Category)tree.SelectedItem);
                DirExplorerViewModel.SelectedinDbChanged(temp.Name);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        #region Save Dir Treeview
        private void Window_Closing(object sender, EventArgs e)
        {
            using (new MemoryStream())
            {
                using (XmlTextWriter wr = new XmlTextWriter(Properties.Resources.SaveExplorerXML, Encoding.UTF8))
                {
                    wr.Formatting = Formatting.Indented;
                    wr.WriteStartDocument();
                    wr.WriteStartElement("Tree");
                    foreach (TreeViewItem dataNode in foldersItem.Items)
                            TreeRunner(wr, dataNode);
                    wr.WriteStartElement("SelectedImagePath");
                    wr.WriteAttributeString("SelectedImagePath", SelectedImagePath);
                    wr.WriteEndElement();
                    wr.WriteEndElement();
                    wr.WriteEndDocument();
                    wr.Flush();
                }
            }
        }

        private void TreeRunner(XmlTextWriter wr, TreeViewItem dataNode)
        {
            if (dataNode.Items.Count>0)
            if (dataNode.Items[0] != null)
            {
                wr.WriteStartElement("Node");
                wr.WriteAttributeString("Node", dataNode.Header.ToString());
                wr.WriteEndElement();
            }
            foreach (TreeViewItem dataNo in dataNode.Items)
                if (dataNo != null)
                    TreeRunner(wr, dataNo);
        }

        private void DeserializeTreeView(string fileName)
        {
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(fileName);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Node")
                        {
                            reader.MoveToAttribute("Node");
                           // foldersItem.FindName("reader.Value");
                           //  folder_Expanded(foldersItem.Items[1],new RoutedEventArgs());
                        }
                        if (reader.Name == "SelectedImagePath")
                        {
                            reader.MoveToAttribute("SelectedImagePath");

                            SelectedImagePath = reader.Value;
                            DirExplorerViewModel.ChangeDirEvent(SelectedImagePath);
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        //Ignore Xml Declaration                    
                    }
                    else if (reader.NodeType == XmlNodeType.None)
                    {
                        return;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                reader?.Close();
            }
        }
        #endregion
    }
}