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
        private List<XE> carList;
        public List<XE> CarList
        {
            get { return carList; }
            set { carList = value; OnPropertyChanged(); }
        }    
        
        private List<TIENCONG> wagesList;
        public List<TIENCONG> WagesList
        {
            get { return wagesList; }
            set { wagesList = value; OnPropertyChanged(); }
        }

        private List<VATTU> itemList;
        public List<VATTU> ItemList
        {
            get { return itemList; }
            set { itemList = value; OnPropertyChanged(); }
        }

        private Decimal totalMoney;
        public Decimal TotalMoney
        {
            get { return totalMoney; }
            set { totalMoney = value; OnPropertyChanged(); }
        }

        private DateTime? selectedDate;
        public DateTime? SelectedDate
        { 
            get { return selectedDate; }
            set { selectedDate = value; OnPropertyChanged(); }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        private string times;
        public string Times
        {
            get { return times; }
            set { times = value; OnPropertyChanged(); }
        }

        private TIENCONG selectedWage;
        public TIENCONG SelectedWage
        {
            get { return selectedWage; }
            set { selectedWage = value; OnPropertyChanged(); }
        }

        private VATTU accessoriesName;
        public VATTU AccessoriesName
        {
            get { return accessoriesName; }
            set { accessoriesName = value; OnPropertyChanged(); }
        }
        private string amount;
        public string Amount
        { 
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }

        private ContentNumbericalOrder selectedContent;
        public ContentNumbericalOrder SelectedContent
        {
            get { return selectedContent; }
            set 
            { 
                selectedContent = value; 
                OnPropertyChanged();
                if (SelectedContent != null)
                {
                    Content = SelectedContent.NoiDung;
                    Times = SelectedContent.SoLan.ToString();
                    SelectedWage = DataProvider.Instance.DB.TIENCONGs.Single(x => x.MaTienCong == SelectedContent.MaTienCong);
                }    
                    
            }
        }

        

        private ObservableCollection<ContentNumbericalOrder> contentList;
        public ObservableCollection<ContentNumbericalOrder> ContentList
        {
            get { return contentList; }
            set { contentList = value; OnPropertyChanged(); }
        }

        public ICommand AddContentCommand { get; set; }
        public ICommand EditContentCommand { get; set; }
        public ICommand DeleteContentCommand { get; set; }
        public ICommand DeSelectedContentCommand { get; set; }
        public ICommand AddAccessoriesCommand { get; set; }
        public ICommand DeleteAccessoriesCommand { get; set; }
        public ICommand MakeReceipt { get; set; }
        public ICommand Refresh { get; set; }
        public ICommand ShowReceiptCommand { get; set; }

        public RepairReceiptViewModel()
        {
            Load();

            AddContentCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Content) || string.IsNullOrEmpty(Times) || SelectedWage == null)
                    return false;
                return true;
            }, (p) =>
            {
                int repeatTimes = Convert.ToInt32(Times);
                ContentNumbericalOrder content = new ContentNumbericalOrder()
                {
                    Number = ContentNumbericalOrder.orderNumber,
                    NoiDung = Content,
                    SoLan = repeatTimes,
                    TenTienCong = SelectedWage.TenTienCong,
                    MaTienCong = SelectedWage.MaTienCong,
                    ThanhTien = SelectedWage.GiaTienCong ?? 0
                };
                ContentList.Add(content);
                Content = Times =  null; SelectedWage = null;
                ContentNumbericalOrder.orderNumber++;
            });

            EditContentCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContent == null)
                    return false;
                if (string.IsNullOrEmpty(Content) || SelectedWage == null)
                    return false;
                return true;
            }, (p) =>
            {
                foreach (var content in ContentList)
                {
                    if (content == SelectedContent)
                    {
                        content.NoiDung = Content;
                        content.SoLan = Convert.ToInt32(Times);
                        content.TenTienCong = SelectedWage.TenTienCong;
                        content.MaTienCong = SelectedWage.MaTienCong;
                        content.ThanhTien = SelectedWage.GiaTienCong ?? 0;

                        SelectedContent = content;
                        break;
                    }
                }
                Content = Times = null; SelectedWage = null; SelectedContent = null; 
            });

            DeleteContentCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContent == null)
                    return false;
                return true;
            }, (p) =>
            {
                int i = 1;
                foreach (var content in ContentList)
                {
                    if (content == SelectedContent)
                    {
                        ContentList.Remove(content);
                        break;
                    }
                }
                ContentNumbericalOrder.orderNumber--;

                foreach (var content in ContentList)
                {
                    content.Number = i;
                    i++;
                }
                Content = Times = null; SelectedWage = null; SelectedContent = null;
            });

            DeSelectedContentCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContent == null)
                    return false;
                return true;
            }, (p) =>
            {
                Content = Times = null; SelectedWage = null; SelectedContent = null;
            });
        }











        private void Load()
        {
            SelectedDate = DateTime.Now;
            CarList = DataProvider.Instance.DB.XEs.ToList();
            WagesList = DataProvider.Instance.DB.TIENCONGs.ToList();
            ItemList = DataProvider.Instance.DB.VATTUs.ToList();
            ContentList = new ObservableCollection<ContentNumbericalOrder>();
        }

        
    }
}
