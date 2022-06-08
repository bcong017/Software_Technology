using QuanLyGaraOto.ViewModel;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class ContentNumbericalOrder : BaseViewModel
    {
        public static int orderNumber = 1;
        private int number;
        public int Number { get { return number; } set { number = value; OnPropertyChanged(); } }

        private string noiDung;
        public string NoiDung { get { return noiDung; } set { noiDung = value; OnPropertyChanged(); } }

        private int soLan;
        public int SoLan { get { return soLan; } set { soLan = value; OnPropertyChanged(); } }

        private string tenTienCong;
        public string TenTienCong { get { return tenTienCong; } set { tenTienCong = value; OnPropertyChanged(); } }

        private Decimal thanhTien;
        public Decimal ThanhTien { get { return thanhTien; } set { thanhTien = value; OnPropertyChanged(); } }

        public int MaTienCong { get; set; }


    }
}
