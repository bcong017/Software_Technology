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
        ShowRepairReceptListViewModel k;
        ObservableCollection<TIENCONG> TienCong;

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

        private KeyValuePair<string, TIENCONG> selectedTienCong;
        public KeyValuePair<string, TIENCONG> SelectedTienCong
        {
            get { return selectedTienCong; }
            set
            {
                selectedTienCong = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<VATTU> vattuList;
        public ObservableCollection<VATTU> VatTuList
        {
            get { return vattuList; }
            set { vattuList = value; OnPropertyChanged(nameof(VatTuList)); }
        }

        private ObservableCollection<CT_SUDUNGVATTU> final;
        public ObservableCollection<CT_SUDUNGVATTU> Final
        {
            get { return final; }
            set { final = value; OnPropertyChanged(nameof(Final)); }
        }

        private Dictionary<string, TIENCONG> tienCongList;
        public Dictionary<string, TIENCONG> TienCongList
        {
            get { return tienCongList; }
            set { tienCongList = value; OnPropertyChanged(nameof(TienCongList)); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DoneCommand { get; set; }

        public AddNewVATPHAMViewModel(ShowRepairReceptListViewModel showRepairReceptListViewModel)
        {
            k = showRepairReceptListViewModel;

            TienCongList = new Dictionary<string, TIENCONG>();
            Final = new ObservableCollection<CT_SUDUNGVATTU>();
            VatTuList = new ObservableCollection<VATTU>(DataProvider.Instance.DB.VATTUs.ToList());
            TienCong = new ObservableCollection<TIENCONG>(DataProvider.Instance.DB.TIENCONGs.ToList());

            foreach (TIENCONG tc in TienCong)
            {
                TienCongList.Add(tc.TenTienCong + ": " + tc.GiaTienCong.ToString(), tc);
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                int sl = Convert.ToInt32(SoLuong);
                if ((sl == 0) || (SelectedVatTu == null))
                    return false;
                return true;
            }, (p) =>
            {
                CT_SUDUNGVATTU ct = new CT_SUDUNGVATTU();
                ct.VATTU = SelectedVatTu;
                ct.MaVatTu = SelectedVatTu.MaVatTu;
                ct.SoLuong = Convert.ToInt32(SoLuong);
                ct.DonGia = SelectedVatTu.DonGiaHienTai;
                ct.ThanhTien = ct.DonGia * ct.SoLuong;
                Final.Add(ct);
            });

            DoneCommand = new RelayCommand<Window>((p) =>
            {
                if ((noiDung == null) || (SoLuong == null) || (!Final.Any()) || (SelectedTienCong.Key == null))
                    return false;
                return true;
            }, (p) =>
            {
                TIENCONG tc = SelectedTienCong.Value;
                k.Receive(NoiDung, Final, tc);
                Close();
            });

            CancelCommand = new RelayCommand<Window>((p) =>
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
