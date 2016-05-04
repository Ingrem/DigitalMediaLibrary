﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalMediaLibrary.Views
{
    /// <summary>
    /// Interaction logic for DirViewerView.xaml
    /// </summary>
    public partial class DirViewerView : UserControl
    {
        public DirViewerView()
        {
            InitializeComponent();
        }
        private void SaveInDb_Click(object sender, RoutedEventArgs e)
        {
            ViewModels.DirViewerViewModel.SaveInDb();
        }
    }
}
