using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGaraOto.Model;

namespace QuanLyGaraOto.ViewModel
{
    public class ShowWageAndMaterialViewModel : BaseViewModel
    {
        private ObservableCollection<VATTU> vattuList;
        public ObservableCollection<VATTU> VatTuList
        {
            get { return vattuList; }
            set { vattuList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TIENCONG> tiencongList;
        public ObservableCollection<TIENCONG> TienCongList
        {
            get { return tiencongList; }
            set { tiencongList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<HIEUXE> hieuxeList;
        public ObservableCollection<HIEUXE> HieuXeList
        {
            get { return hieuxeList; }
            set { hieuxeList = value; OnPropertyChanged(); }
        }

        public ShowWageAndMaterialViewModel()
        {
            VatTuList = new ObservableCollection<VATTU>(DataProvider.Instance.DB.VATTUs.ToList());
            TienCongList = new ObservableCollection<TIENCONG>(DataProvider.Instance.DB.TIENCONGs.ToList());
            HieuXeList = new ObservableCollection<HIEUXE>(DataProvider.Instance.DB.HIEUXEs.ToList());


        }
    }
}
