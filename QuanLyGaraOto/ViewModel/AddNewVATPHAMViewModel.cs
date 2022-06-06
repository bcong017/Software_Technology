using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

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
                if (SelectedItem != null)
                {
                    //DoStuff
                }
            }
        }

        private VATTU selectedVatTu;
        public VATTU SelectedItem
        {
            get { return selectedVatTu; }
            set
            {
                selectedVatTu = value;
                OnPropertyChanged();
                if (SelectedItem != null)
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

        public AddNewVATPHAMViewModel(RepairReceiptViewModel repairReceiptViewModel)
        {
            k = repairReceiptViewModel;
            TienCongList = new ObservableCollection<TIENCONG>(DataProvider.Instance.DB.TIENCONGs.ToList());
            VatTuList = new ObservableCollection<VATTU>(DataProvider.Instance.DB.VATTUs.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                if ((noiDung == null) || (soLuong == null) /*|| (selectedTienCong == null) || (selectedVatTu == null)*/)
                    return false;
                return true;
            }, (p) =>
            {
                k.Receive();
                Close();
            });

            CancelCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Close();
            });
        }

        void Close()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
