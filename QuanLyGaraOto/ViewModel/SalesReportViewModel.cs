using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class SalesReportViewModel : BaseViewModel
    {
        private MonthList _monthList;
        public MonthList MonthList { get { return _monthList; } set { _monthList = value;  OnPropertyChanged(); } }
        public List<MonthList> Month { get; set; }

        public ICommand MakeReportCommand { get; set; }

        public SalesReportViewModel()
        {
            Month = new List<MonthList>();
            Month.Add(new MonthList(1, "Một"));

            MakeReportCommand = new RelayCommand<object>((p) => { return true; }, (p) => { MessageBox.Show(MonthList.Month.ToString()); });
        }
    }
    public class MonthList
    { 
        public int Month { get; set; }
        public string Name { get; set; }
        public MonthList(int month, string name)
        {
            Month = month;
            Name = name;
        }
    }

}
