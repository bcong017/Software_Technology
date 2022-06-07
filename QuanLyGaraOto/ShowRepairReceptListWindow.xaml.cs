using QuanLyGaraOto.ViewModel;
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
using QuanLyGaraOto.Model;

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for ShowRepairReceptListWindow.xaml
    /// </summary>
    public partial class ShowRepairReceptListWindow : Window
    {
        public ShowRepairReceptListWindow(RepairReceiptViewModel rrv, PHIEUSUACHUA psc)
        {
            InitializeComponent();
            this.DataContext = new ShowRepairReceptListViewModel(rrv, psc);
        }
    }
}
