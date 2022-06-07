using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private string _HoTen;
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }

        private string _BienSo;
        public string BienSo { get => _BienSo; set { _BienSo = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private DateTime? _NgayThuTien;
        public DateTime? NgayThuTien { get => _NgayThuTien; set { _NgayThuTien = value; OnPropertyChanged(); } }

        private string _SoDienThoai;
        public string SoDienThoai { get => _SoDienThoai; set { _SoDienThoai = value; OnPropertyChanged(); } }

        private Decimal _SoTienThu;
        public Decimal SoTienThu { get => _SoTienThu; set { _SoTienThu = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeSelectedItemCommand { get; set; }
        public BillViewModel()
        {
            NgayTiepNhan = DateTime.Now;
            ListHieuXe = DataProvider.Instance.DB.HIEUXEs.ToList();
            ListTiepNhanXeSua = new ObservableCollection<ReceiveNumbericalOrder>();
            var Cars = DataProvider.Instance.DB.XEs.ToList();
            for (int i = 0; i < Cars.Count; i++)
            {
                ReceiveNumbericalOrder receiveNumbericalOrder = new ReceiveNumbericalOrder();
                receiveNumbericalOrder.Car = Cars[i];
                receiveNumbericalOrder.Number = i + 1;
                if (receiveNumbericalOrder.Car.DaXoa == true)
                    continue;
                ListTiepNhanXeSua.Add(receiveNumbericalOrder);
            }
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(HoTen))
                    return false;
                if (string.IsNullOrEmpty(BienSo))
                    return false;
                if (HieuXe == null)
                    return false;
                if (string.IsNullOrEmpty(DiaChi))
                    return false;
                if (NgayTiepNhan == null)
                    return false;
                if (string.IsNullOrEmpty(SoDienThoai))
                    return false;
                return true;
            }, (p) =>
            {
                
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                
            });

            DeSelectedItemCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                
            });
        }
    }
}
