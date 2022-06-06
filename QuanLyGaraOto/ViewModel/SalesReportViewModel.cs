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
        public ICommand MakeReportCommand { get; set; }

        public SalesReportViewModel()
        {
            MakeReportCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
        }
    }

}
