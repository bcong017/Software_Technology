﻿using QuanLyGaraOto.ViewModel;
using System;
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

namespace QuanLyGaraOto.ViewUserControl.ViewUC
{
    /// <summary>
    /// Interaction logic for ShowWageAndMaterialUC.xaml
    /// </summary>
    public partial class ShowWageAndMaterialUC : UserControl
    {
        public ShowWageAndMaterialUC()
        {
            InitializeComponent();
            this.DataContext = new ShowWageAndMaterialViewModel();
        }

   
    }
}
