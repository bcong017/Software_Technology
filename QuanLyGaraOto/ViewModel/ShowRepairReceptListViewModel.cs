using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using QuanLyGaraOto;

namespace QuanLyGaraOto.ViewModel
{
    public class ShowRepairReceptListViewModel : BaseViewModel
    {
        PHIEUSUACHUA phieuSuaChua;
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand DoneCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private CT_PSC selectedItem;
        public CT_PSC SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    //DoStuff
                }
            }
        }

        private Nullable<DateTime> selectedDate;
        public Nullable<DateTime> SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
                if (SelectedDate != null)
                {
                    //DoStuff
                }
            }
        }

        private Nullable<decimal> totalMoney;
        public Nullable<decimal> TotalMoney
        {
            get { return totalMoney; }
            set
            {
                totalMoney = value;
                OnPropertyChanged(nameof(TotalMoney));
            }
        }

        private ObservableCollection<CT_PSC> detailList;
        public ObservableCollection<CT_PSC> DetailList
        {
            get { return detailList; }
            set { detailList = value; OnPropertyChanged(nameof(DetailList));}
        }

        private KeyValuePair<string, XE> selectedCar;
        public KeyValuePair<string, XE> SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<string, XE> carList;
        public Dictionary<string, XE> CarList
        {
            get { return carList; }
            set { carList = value; OnPropertyChanged(nameof(CarList)); }
        }

        public ShowRepairReceptListViewModel(RepairReceiptViewModel rrv,PHIEUSUACHUA psc)
        {
            CarList = new Dictionary<string, XE>();
            ObservableCollection<XE> cl = new ObservableCollection<XE>(DataProvider.Instance.DB.XEs.ToList());

            foreach (XE xe in cl)
            {
                CarList.Add(xe.BienSo, xe);
            }

            if (psc != null)
            {
                phieuSuaChua = psc;
                SelectedDate = psc.NgaySuaChua;
                TotalMoney = psc.TongTien;
                DetailList = new ObservableCollection<CT_PSC>(psc.CT_PSC.ToList());
                SelectedCar = CarList.Where(x => x.Value == psc.XE).FirstOrDefault();
            }
            else
            {
                phieuSuaChua = new PHIEUSUACHUA();
                DataProvider.Instance.DB.PHIEUSUACHUAs.Add(phieuSuaChua);
                DetailList = new ObservableCollection<CT_PSC>();
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Window w = Application.Current.MainWindow;
                AddNewVATPHAM k = new AddNewVATPHAM(this);
                Application.Current.MainWindow = k;
                Application.Current.MainWindow.ShowDialog();
                Application.Current.MainWindow = w;
            });

            RemoveCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DetailList.Remove(SelectedItem);
                TotalMoney = TongSoTien();
            });

            DoneCommand = new RelayCommand<object>((p) =>
            {
                if ((TotalMoney == null) || (SelectedDate == null) || (SelectedCar.Key == null))
                    return false;
                return true;
            }, (p) =>
            {
                if (psc != null)
                {
                    psc.NgaySuaChua = SelectedDate;
                    psc.XE = SelectedCar.Value;
                    psc.BienSo = phieuSuaChua.XE.BienSo;
                    psc.TongTien = TongSoTien();
                    psc.CT_PSC = DetailList;
                    DataProvider.Instance.DB.SaveChanges();
                } else
                {
                    phieuSuaChua.XE = SelectedCar.Value;
                    phieuSuaChua.BienSo = SelectedCar.Value.BienSo;
                    phieuSuaChua.NgaySuaChua = SelectedDate;
                    phieuSuaChua.TongTien = TongSoTien();
                    phieuSuaChua.CT_PSC = DetailList;
                    DataProvider.Instance.DB.SaveChanges();
                }

                Close();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Close();
            });
        }

        public void Receive(string nd, ObservableCollection<CT_SUDUNGVATTU> ct, TIENCONG tc)
        {
            CT_PSC ctpsc = new CT_PSC();
            ctpsc.NoiDung = nd;
            ctpsc.SoLan = ct.Count();
            ctpsc.MaTienCong = tc.MaTienCong;
            ctpsc.TIENCONG = tc;
            ctpsc.CT_SUDUNGVATTU = ct;
            ctpsc.ThanhTien = 0;
            foreach (CT_SUDUNGVATTU ctsdvt in ct)
            {
                ctpsc.ThanhTien += ctsdvt.ThanhTien;
            }
            ctpsc.ThanhTien += tc.GiaTienCong;

            DetailList.Add(ctpsc);
            TotalMoney = TongSoTien();
        }

        private Nullable<decimal> TongSoTien()
        {
            Nullable<decimal> result = 0;
            foreach (CT_PSC ct in DetailList)
            {
                result += ct.ThanhTien;
            }
            return result;
        }

        void Close()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
