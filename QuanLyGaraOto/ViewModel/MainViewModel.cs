using QuanLyGaraOto.ViewUserControl.SwitchViewClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyGaraOto.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new SwitchViewCarCheckIn();

        public BaseViewModel SelectedViewModel 
        { 
            get { return _selectedViewModel; } 
            set 
            { 
                _selectedViewModel = value; 
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public MainViewModel()
        {
            
        }
    }
}
