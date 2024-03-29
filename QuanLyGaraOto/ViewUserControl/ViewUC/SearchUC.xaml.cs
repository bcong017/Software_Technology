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
using QuanLyGaraOto.ViewModel;

namespace QuanLyGaraOto.ViewUserControl.ViewUC
{
    /// <summary>
    /// Interaction logic for SearchUC.xaml
    /// </summary>
    public partial class SearchUC : UserControl
    {
        public SearchUC()
        {
            InitializeComponent();
            this.DataContext = new SearchViewModel();
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
    
}
