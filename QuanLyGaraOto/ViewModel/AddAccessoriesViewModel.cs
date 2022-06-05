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
    public class AddAccessoriesViewModel : BaseViewModel
    {
        private int number = 1;
        private string accessoriesName;
        public string AccessoriesName
        {
            get { return accessoriesName; }
            set { accessoriesName = value; OnPropertyChanged(nameof(AccessoriesName)); }
        }

        private string priceInput;
        public string PriceInput
        {
            get { return priceInput; }
            set { priceInput = value; OnPropertyChanged(nameof(PriceInput)); }
        }

        private string priceOutputRecommended;
        public string PriceOutputRecommended
        {
            get { return priceOutputRecommended; }
            set { priceOutputRecommended = value; OnPropertyChanged(nameof(PriceOutputRecommended)); }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(nameof(Amount)); }
        }

        private decimal totalMoney;
        public decimal TotalMoney
        {
            get { return totalMoney; }
            set { totalMoney = value; OnPropertyChanged(); }
        }

        private DateTime? inputDate;
        public DateTime? InputDate
        {
            get { return inputDate; }
            set { inputDate = value; OnPropertyChanged(); }
        }

        private AccessoriesNumbericalOrder selectedItem;
        public AccessoriesNumbericalOrder SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    AccessoriesName = SelectedItem.TenVatTu;
                    PriceInput = Convert.ToInt32(SelectedItem.DonGiaNhap).ToString();
                    PriceOutputRecommended = Convert.ToInt32(SelectedItem.DonGiaBanDeNghi).ToString();
                    Amount = SelectedItem.SoLuong.ToString();
                }    
            }
        }

        private ObservableCollection<AccessoriesNumbericalOrder> inputList;
        public ObservableCollection<AccessoriesNumbericalOrder> InputList
        {
            get { return inputList; }
            set { inputList = value; OnPropertyChanged(); }
        }

        public ICommand ShowInputRecordCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeSelectedItemCommand { get; set; }
        public ICommand AddAccessoriesList { get; set; }
        public AddAccessoriesViewModel()
        {
            InputDate = DateTime.Now;
            InputList = new ObservableCollection<AccessoriesNumbericalOrder>();
            ShowInputRecordCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ShowInputRecordWindow showInputRecordWindow = new ShowInputRecordWindow(); showInputRecordWindow.Show(); });
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(AccessoriesName) || string.IsNullOrEmpty(PriceInput) || string.IsNullOrEmpty(Amount))
                    return false;
                if (Decimal.TryParse(PriceInput, out _) == false || Int32.TryParse(Amount, out _) == false)
                    return false;
                if (CheckInputName() == false)
                    return false;
                return true;

            }, (p) =>
            {
                AccessoriesNumbericalOrder accessoriesNumbericalOrder = new AccessoriesNumbericalOrder();
                accessoriesNumbericalOrder.Number = number;
                accessoriesNumbericalOrder.TenVatTu = AccessoriesName;                
                accessoriesNumbericalOrder.DonGiaNhap = Convert.ToDecimal(PriceInput);
                accessoriesNumbericalOrder.DonGiaBanDeNghi = accessoriesNumbericalOrder.DonGiaNhap * (decimal)1.2;
                accessoriesNumbericalOrder.SoLuong = Convert.ToInt32(Amount);
                accessoriesNumbericalOrder.ThanhTien = accessoriesNumbericalOrder.DonGiaNhap * accessoriesNumbericalOrder.SoLuong;
                InputList.Add(accessoriesNumbericalOrder);
                AccessoriesName = PriceInput = PriceOutputRecommended = Amount = "";
                number++;
                UpdateTotalMoney();
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                if (Decimal.TryParse(PriceInput, out _) == false || Int32.TryParse(Amount, out _) == false)
                    return false;
                return true;
            }, (p) =>
            {
                foreach (var item in InputList)
                {
                    if (item == SelectedItem)
                    {
                        item.TenVatTu = AccessoriesName;
                        item.DonGiaNhap = Convert.ToDecimal(PriceInput);
                        item.DonGiaBanDeNghi = item.DonGiaNhap * (decimal)1.2;
                        item.SoLuong = Convert.ToInt32(Amount);
                        item.ThanhTien = item.DonGiaNhap * item.SoLuong;
                        SelectedItem = item;
                        break;
                    }
                }
                AccessoriesName = PriceInput = PriceOutputRecommended = Amount = "";
                UpdateTotalMoney();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) => 
            {
                int i = 1;
                foreach (var item in InputList)
                {
                    if (item == SelectedItem)
                    {
                        InputList.Remove(item);
                        break;
                    }
                }
                number--;
                foreach (var item in InputList)
                {
                    item.Number = i;
                    i++;
                }
                AccessoriesName = PriceInput = PriceOutputRecommended = Amount = "";
                UpdateTotalMoney();
            });

            DeSelectedItemCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                SelectedItem = null;
                AccessoriesName = PriceInput = PriceOutputRecommended = Amount = "";
            });

            AddAccessoriesList = new RelayCommand<object>((p) =>
            {
                if (InputList.Count < 1)
                    return false;
                return true;
            }, (p) =>
            {
                PHIEUNHAP phieunhap = new PHIEUNHAP() { NgayNhap = InputDate, ThanhTien = TotalMoney };
                DataProvider.Instance.DB.PHIEUNHAPs.Add(phieunhap);
                DataProvider.Instance.DB.SaveChanges();

                int id = DataProvider.Instance.DB.PHIEUNHAPs.OrderByDescending(x => x.MaPhieuNhap).Select(m => m.MaPhieuNhap).First();
                foreach (var item in InputList)
                {
                    int idVatTu;
                    var vattu = DataProvider.Instance.DB.VATTUs.Where(x => x.TenVatTu == item.TenVatTu).FirstOrDefault();
                    if (vattu != null)
                    {
                        idVatTu = vattu.MaVatTu;
                        vattu.SoLuongTon = vattu.SoLuongTon + 1;
                    }    
                    else
                    {
                        VATTU newVatTu = new VATTU() { TenVatTu = item.TenVatTu, DonGiaHienTai = item.DonGiaBanDeNghi, SoLuongTon = 1 };
                        DataProvider.Instance.DB.VATTUs.Add(newVatTu);
                        DataProvider.Instance.DB.SaveChanges();
                    }    
                    //CT_PHIEUNHAP ct_phieunhap = new CT_PHIEUNHAP() { MaPhieuNhap = id, MaVatTu = idVatTu, SoLuong = item.SoLuong, }
                }    

            });
        }

        private bool CheckInputName()
        {
            foreach (var item in InputList)
            {
                if (string.Compare(item.TenVatTu, AccessoriesName) == 0)
                    return false;
            }
            return true;
        }
        private void UpdateTotalMoney()
        {
            foreach(var item in InputList)
            {
                TotalMoney += item.ThanhTien;
            }
        }
    }
}
