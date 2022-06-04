using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ShowAccessoriesViewModel : BaseViewModel
    {
        private string accessoriesName;
        public string AccessoriesName
        {
            get { return accessoriesName; }
            set { accessoriesName = value; OnPropertyChanged(nameof(AccessoriesName)); }
        }

        private string price;
        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(nameof(Amount)); }
        }

        private VATTU selectedItem;
        public VATTU SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value; 
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    if (AccessoriesName == SelectedItem.TenVatTu && Price == SelectedItem.DonGiaHienTai.ToString() && Amount == SelectedItem.SoLuongTon.ToString())
                    {
                        SelectedItem = null;
                        AccessoriesName = Price = Amount = null;
                    }    
                    else
                    {
                        AccessoriesName = SelectedItem.TenVatTu;
                        Price = Convert.ToInt32(SelectedItem.DonGiaHienTai).ToString();
                        Amount = SelectedItem.SoLuongTon.ToString();
                    }    
                }    
            }
        }

        private ObservableCollection<VATTU> vattuList;
        public ObservableCollection<VATTU> VatTuList
        {
            get { return vattuList; }
            set { vattuList = value; OnPropertyChanged(nameof(VatTuList)); }
        }
        public ICommand DeSelectedItemCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }
        public ShowAccessoriesViewModel()
        {
            VatTuList = new ObservableCollection<VATTU>(DataProvider.Instance.DB.VATTUs.ToList());

            DeSelectedItemCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                SelectedItem = null;
                AccessoriesName = Price = Amount = "";
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(AccessoriesName) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Amount))
                    return false;
                if (Decimal.TryParse(Price, out _) == false || Int32.TryParse(Amount, out _) == false)
                    return false;
                if (DataProvider.Instance.DB.VATTUs.Any(x => x.TenVatTu == AccessoriesName))
                    return false;

                return true;
            }, (p) =>
            {
                var vattu = new VATTU() {TenVatTu = accessoriesName, DonGiaHienTai = Convert.ToDecimal(Price), SoLuongTon = Convert.ToInt32(Amount)};

                DataProvider.Instance.DB.VATTUs.Add(vattu);
                DataProvider.Instance.DB.SaveChanges();

                VatTuList.Add(vattu);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || Decimal.TryParse(Price, out _) == false || Int32.TryParse(Amount, out _) == false)
                    return false;
                return true;
            }, (p) =>
            {
                var vattu = DataProvider.Instance.DB.VATTUs.SingleOrDefault(x => x.MaVatTu == SelectedItem.MaVatTu);
                if (vattu == null)
                    return;

                vattu.TenVatTu = AccessoriesName;
                vattu.DonGiaHienTai = Convert.ToDecimal(Price);
                vattu.SoLuongTon = Convert.ToInt32(Amount);

                DataProvider.Instance.DB.SaveChanges();
                
                VatTuList = new ObservableCollection<VATTU>(DataProvider.Instance.DB.VATTUs.ToList());
                  
            });
        }
    }
}
