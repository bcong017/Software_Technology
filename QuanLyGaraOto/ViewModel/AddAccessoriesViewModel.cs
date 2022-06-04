using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AddAccessoriesViewModel : BaseViewModel
    {
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

        private CT_PHIEUNHAP selectedItem;
        public CT_PHIEUNHAP SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    AccessoriesName = SelectedItem.VATTU.TenVatTu;
                    PriceInput = Convert.ToInt32(SelectedItem.DonGiaNhap).ToString();
                    PriceOutputRecommended = Convert.ToInt32(SelectedItem.DonGiaBan).ToString();
                    Amount = SelectedItem.SoLuong.ToString();
                }    
            }
        }

        public ICommand ShowInputRecordCommand { get; set; }
        public AddAccessoriesViewModel()
        {
            ShowInputRecordCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ShowInputRecordWindow showInputRecordWindow = new ShowInputRecordWindow(); showInputRecordWindow.Show(); });
        }
    }
}
