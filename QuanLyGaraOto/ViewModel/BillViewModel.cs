using QuanLyGaraOto.Model;
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
        private Decimal? money;
        private string _BienSo;
        public string BienSo { get => _BienSo; set { _BienSo = value; OnPropertyChanged(); } }

        private DateTime? _NgayThuTien;
        public DateTime? NgayThuTien { get => _NgayThuTien; set { _NgayThuTien = value; OnPropertyChanged(); } }

        private string _SoTienThu;
        public string SoTienThu { get => _SoTienThu; set { _SoTienThu = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeSelectedItemCommand { get; set; }
        public BillViewModel()
        {
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(BienSo))
                    return false;
                if (string.IsNullOrEmpty(SoTienThu))
                    return false;
                if (NgayThuTien == null)
                    return false;
                return true;
            }, (p) =>
            {
                
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                
            });

            DeSelectedItemCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                
            });
        }
    }
}
