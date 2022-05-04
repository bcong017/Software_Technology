using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class SalesReportViewModel : BaseViewModel
    {
        public List<int> Month { get; set; }
        public SalesReportViewModel()
        {
            Month = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        }
    }
}
