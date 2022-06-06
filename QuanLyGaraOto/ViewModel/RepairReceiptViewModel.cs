using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyGaraOto.ViewModel
{
    public class VATPHAM
    {
    }

    public class RepairReceiptViewModel : BaseViewModel
    {
        private string ngaySuaChua;
        public string NgaySuaChua
        {
            get { return ngaySuaChua; }
            set { ngaySuaChua = value; OnPropertyChanged(nameof(NgaySuaChua)); }
        }

        private string bienSoXe;
        public string BienSoXe
        {
            get { return bienSoXe; }
            set { bienSoXe = value; OnPropertyChanged(nameof(BienSoXe)); }
        }

        private string tongSoTien;
        public string TongSoTien
        {
            get { return tongSoTien; }
            set { tongSoTien = value; OnPropertyChanged(nameof(TongSoTien)); }
        }

        private VATPHAM selectedItem;
        public VATPHAM SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    //Do Stuff
                }
            }
        }

        private ObservableCollection<VATPHAM> vatPhamList;
        public ObservableCollection<VATPHAM> VatPhamList
        {
            get { return vatPhamList; }
            set { vatPhamList = value; OnPropertyChanged(nameof(VatPhamList)); }
        }

        public ICommand HoanTat { get; set; }
        public ICommand InPhieu { get; set; }
        public ICommand PhieuMoi { get; set; }
        public ICommand ThemVatPham { get; set; }
        public ICommand XoaVatPham { get; set; }

        public RepairReceiptViewModel()
        {
            ThemVatPham = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddNewVATPHAM addNewVATPHAM = new AddNewVATPHAM(this);
                Application.Current.MainWindow = addNewVATPHAM;
                Application.Current.MainWindow.Show();
            });
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        public void Received()
        {
            NotifyWindow i = new NotifyWindow("đã nhận đc");
            i.Show();
        }
    }
}
