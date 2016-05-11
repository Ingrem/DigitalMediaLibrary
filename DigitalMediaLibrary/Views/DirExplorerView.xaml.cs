using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using DigitalMediaLibraryData.Models;
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
            using (LibraryContext db = new LibraryContext())
            {
                var categorys = db.Categorys.ToList();
                var tmpMediaType = db.MediaTypes.ToList();
                foreach (MediaType u in tmpMediaType)
                    DbTreeView.Items.Add(u);
            } 
            DeserializeTreeView(Properties.Resources.SaveExplorerXML);
            Dispatcher.ShutdownStarted += Window_Closing;
        }

        private readonly object _dummyNode = null;
        private string SelectedImagePath { get; set; }
        private string _currentCategoryInDb = "";
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
                _currentCategoryInDb = temp.Name;
                DirExplorerViewModel.SelectedinDbChanged(temp.Name);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #region Save Treeview
        private void Window_Closing(object sender, EventArgs e)
        {
            using (new MemoryStream())
            {
                using (XmlTextWriter wr = new XmlTextWriter(Properties.Resources.SaveExplorerXML, Encoding.UTF8))
                {
                    wr.Formatting = Formatting.Indented;
                    wr.WriteStartDocument();
                    wr.WriteStartElement("Tree");
                    // save DB tree
                    if (DbTree.IsSelected)
                    {
                        wr.WriteStartElement("Category");
                        wr.WriteAttributeString("Category", _currentCategoryInDb);
                        wr.WriteEndElement();
                    }
                    // save explorer tree
                    else
                    {
                        foreach (TreeViewItem dataNode in foldersItem.Items)
                            TreeRunnerSave(wr, dataNode);
                        wr.WriteStartElement("SelectedImagePath");
                        wr.WriteAttributeString("SelectedImagePath", SelectedImagePath);
                        wr.WriteEndElement();
                    }
                    wr.WriteEndElement();
                    wr.WriteEndDocument();
                    wr.Flush();
                }
            }
        }

        private void TreeRunnerSave(XmlTextWriter wr, TreeViewItem dataNode)
        {
            if (dataNode.Items.Count>0)
            if (dataNode.IsExpanded)
            {
                wr.WriteStartElement("Node");
                wr.WriteAttributeString("Node", dataNode.Header.ToString());
                wr.WriteEndElement();
            }
            foreach (TreeViewItem dataNo in dataNode.Items)
                if (dataNo != null)
                    TreeRunnerSave(wr, dataNo);
        }

        private void TreeRunnerLoad(string nodeForExpand, TreeViewItem dataNode)
        {
            foreach (TreeViewItem dataNo in dataNode.Items)
            {
                if (dataNo.Header.ToString() == nodeForExpand)
                {
                    dataNo.IsExpanded = true;
                }
                if (dataNo.IsExpanded)
                {
                    TreeRunnerLoad(nodeForExpand, dataNo);
                }
            }
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
                        //for load from explorer
                        if (reader.Name == "Node")
                        {
                            reader.MoveToAttribute("Node");
                            foreach (TreeViewItem dataNo in foldersItem.Items)
                            {
                                if (dataNo.Header.ToString() == reader.Value)
                                    dataNo.IsExpanded = true;

                                if (dataNo.IsExpanded)
                                    TreeRunnerLoad(reader.Value, dataNo);
                            }
                        }
                        // for load from explorer
                        if (reader.Name == "SelectedImagePath")
                        {
                            reader.MoveToAttribute("SelectedImagePath");

                            SelectedImagePath = reader.Value;
                            DirExplorerViewModel.ChangeDirEvent(SelectedImagePath);
                        }
                        // for load from DB
                        if (reader.Name == "Category")
                        {
                            DbTree.IsSelected = true;
                            reader.MoveToAttribute("Category");
                            DirExplorerViewModel.SelectedinDbChanged(reader.Value);
                            _currentCategoryInDb = reader.Value;
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
                reader.Close();
            } 
        }
        #endregion
    }
}