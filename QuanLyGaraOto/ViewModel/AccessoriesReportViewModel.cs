using QuanLyGaraOto.Model;
using QuanLyGaraOto.AddingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AccessoriesReportViewModel : BaseViewModel
    {
        private MonthList months = new MonthList();
        public MonthList Months
        {
            get { return months; }
            set { months = value; }
        }

        private Month selectedItem;
        public Month SelectedItem
        {
            get { return selectedItem; }
            set 
            {
                selectedItem = value; 
                OnPropertyChanged(nameof(SelectedItem)); 
                if (SelectedItem != null)
                    Number = SelectedItem.Number; 
            }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private string year;
        public string Year
        { 
            get { return year; }
            set { year = value; }
        }
        public ICommand MakeReportCommand { get; set; }
        public AccessoriesReportViewModel()
        {
            MakeReportCommand = new RelayCommand<object>((p) => 
            {
                if (SelectedItem == null || string.IsNullOrEmpty(Year) || Int32.TryParse(Year, out _) == false)
                    return false;
                return true;
            }, (p) => 
            { 

            });
        }
    }
}
