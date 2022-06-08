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
    public class RepairReceiptViewModel : BaseViewModel
    {
        private PHIEUSUACHUA selectedItem;
        public PHIEUSUACHUA SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    //Dostuff
                }
            }
        }

        private ObservableCollection<PHIEUSUACHUA> receptList;
        public ObservableCollection<PHIEUSUACHUA> ReceptList
        {
            get { return receptList; }
            set { receptList = value; OnPropertyChanged(nameof(ReceptList)); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ModifyCommand { get; set; }


        public RepairReceiptViewModel()
        {
            ReceptList = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Instance.DB.PHIEUSUACHUAs.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Window w = Application.Current.MainWindow;
                ShowRepairReceptListWindow k = new ShowRepairReceptListWindow(this, null);
                Application.Current.MainWindow = k;
                Application.Current.MainWindow.ShowDialog();
                Application.Current.MainWindow = w;

                ReceptList = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Instance.DB.PHIEUSUACHUAs.ToList());
            });

            ModifyCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                Window w = Application.Current.MainWindow;
                PHIEUSUACHUA psc = SelectedItem;
                
                ShowRepairReceptListWindow k = new ShowRepairReceptListWindow(this, psc);
                Application.Current.MainWindow = k;
                Application.Current.MainWindow.ShowDialog();
                Application.Current.MainWindow = w;
                ReceptList = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Instance.DB.PHIEUSUACHUAs.ToList());

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DataProvider.Instance.DB.PHIEUSUACHUAs.Remove(SelectedItem);
                DataProvider.Instance.DB.SaveChanges();
                ReceptList = new ObservableCollection<PHIEUSUACHUA>(DataProvider.Instance.DB.PHIEUSUACHUAs.ToList());
            });
        }
    }
}
