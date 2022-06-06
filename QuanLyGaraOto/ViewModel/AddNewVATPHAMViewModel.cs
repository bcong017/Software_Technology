using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AddNewVATPHAMViewModel : BaseViewModel
    {
        RepairReceiptViewModel k;

        private string noiDung;
        public string NoiDung
        {
            get { return noiDung; }
            set { noiDung = value; OnPropertyChanged(nameof(NoiDung)); }
        }

        private string soLuong;
        public string SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; OnPropertyChanged(nameof(SoLuong)); }
        }

        private TIENCONG selectedTienCong;
        public TIENCONG SelectedTienCong
        {
            get { return selectedTienCong; }
            set
            {
                selectedTienCong = value;
                OnPropertyChanged();
                if (selectedTienCong != null)
                {
                    //DoStuff
                }
            }
        }

        private VATTU selectedVatTu;
        public VATTU SelectedVatTu
        {
            get { return selectedVatTu; }
            set
            {
                selectedVatTu = value;
                OnPropertyChanged();
                if (SelectedVatTu != null)
                {
                    //DoStuff
                }
            }
        }

        private ObservableCollection<VATTU> vattuList;
        public ObservableCollection<VATTU> VatTuList
        {
            get { return vattuList; }
            set { vattuList = value; OnPropertyChanged(nameof(VatTuList)); }
        }

        private ObservableCollection<TIENCONG> tiencongList;
        public ObservableCollection<TIENCONG> TienCongList
        {
            get { return tiencongList; }
            set { tiencongList = value; OnPropertyChanged(nameof(TienCongList)); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddNewVATPHAMViewModel(RepairReceiptViewModel former)
        {
            k = former;

            VatTuList = new ObservableCollection<VATTU>(DataProvider.Instance.DB.VATTUs.ToList());
            TienCongList = new ObservableCollection<TIENCONG>(DataProvider.Instance.DB.TIENCONGs.ToList());

            AddCommand = new RelayCommand<Window>((p) =>
            {
                int sl = Convert.ToInt32(SoLuong);
                if ((SelectedVatTu == null) || (SelectedTienCong == null) || (NoiDung == null) || (sl == null))
                    return false;
                return true;
            }, (p) =>
            {
                k.Received();
                //Do stuff
                CloseWindow(p);
            });

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                CloseWindow(p);
            });
        }

        void CloseWindow(Window p)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
