using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class ForgetPasswordWindowViewModel : BaseViewModel
    {
        private string userName;
        private string gmail;

        private bool enableSendMailBtn;
        public string UserName
        {
            get { return userName; }
            set 
            { 
                userName = value;
                EnableSendMailBtnChecker();
            }
        }
        public string Gmail
        {
            get { return gmail; }
            set 
            { 
                gmail = value;
                EnableSendMailBtnChecker();
            }
        }

        public bool EnableSendMailBtn
        {
            get { return enableSendMailBtn; }
            set
            {
                enableSendMailBtn = value;
                OnPropertyChanged(nameof(EnableSendMailBtn));
            }
        }


        public ICommand CloseCommand { get; set; }

        
        public ForgetPasswordWindowViewModel()
        {
            EnableSendMailBtn = false;
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                //if (p == null)
                //    return;
                p.Close();
            });
        }

        void EnableSendMailBtnChecker()
        {
            if (string.IsNullOrEmpty(UserName) == false && string.IsNullOrEmpty(Gmail) == false)
                EnableSendMailBtn = true;
            else
                EnableSendMailBtn = false;
        }
    }
}
