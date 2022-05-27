using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class AccountInformationViewModel : BaseViewModel
    {
        private bool enableState;

        public bool EnableState
        {
            get { return enableState; }
            set 
            { 
                enableState = value; 
                OnPropertyChanged(nameof(EnableState));
            }
        }
        public ICommand CloseCommand { get; set; }
        public ICommand AllowToChangeCommand { get; set;}
        public ICommand OpenChangePasswordWindowCommand { get; set; }
        public AccountInformationViewModel()
        {
            EnableState = false;
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            AllowToChangeCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                if (EnableState == false)
                {
                    EnableState = true;
                    p.Content = "Hủy chỉnh sửa";
                }    
                else
                {
                    EnableState = false;
                    p.Content = "Chỉnh sửa";
                }
            });

            OpenChangePasswordWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => 
            { 
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(); 
                changePasswordWindow.ShowDialog();
            });
        }
    }
}
