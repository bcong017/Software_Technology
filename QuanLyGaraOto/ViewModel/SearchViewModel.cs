using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        public List<string> Option { get; set; }
        public SearchViewModel()
        {
            this.Option = new List<string>() { "Biển số", "Hiệu xe", "Chủ xe", "Tiền nợ"};
        }
    }
}
