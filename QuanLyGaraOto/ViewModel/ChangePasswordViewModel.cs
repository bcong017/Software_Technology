using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }

        public ChangePasswordViewModel()
        {
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }    
    }
}
