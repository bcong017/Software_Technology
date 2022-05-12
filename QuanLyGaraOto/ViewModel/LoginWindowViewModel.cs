using QuanLyGaraOto.AddingClasses;
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
    public class LoginWindowViewModel : BaseViewModel
    {
        public bool isLoggedIn = false;
        public bool enableLoginBtn;
        private string userName;
        private string password;

        public string UserName
        {
            get { return userName; }
            set 
            { 
                userName = value;
                OnPropertyChanged(nameof(UserName));
                EnableLoginButtonChecker();
            }
        }

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value; 
                OnPropertyChanged(nameof(Password));
                EnableLoginButtonChecker();
            } 
        }

        public bool EnableLoginBtn 
        { 
            get { return enableLoginBtn; }
            set 
            { 
                enableLoginBtn = value;
                OnPropertyChanged(nameof(EnableLoginBtn)); 
            } 
        }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public LoginWindowViewModel()
        {
            EnableLoginBtn = false;
            UserName = "";
            Password = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            ExitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Application.Current.Shutdown(); });
        }

        void Login(Window p)
        {
            if (p == null)
                return;

            string passEncode = Encryptor.CreateMD5(Encryptor.Base64Encode(Password));

            // truy vấn dữ liệu sql ở đây, chưa có nên chưa làm lmao :v
            // chỗ này để tạm vô là login đc thôi nhá còn bao giờ truy vấn đc rồi thì cho điều kiện vào đây
            // sử dụng NotifyWindow để thông báo nếu đăng nhập chưa ok
            isLoggedIn = true;
            p.Close();

        }

        void EnableLoginButtonChecker()
        {
            if (string.IsNullOrEmpty(userName) == false && string.IsNullOrEmpty(password) == false)
                EnableLoginBtn = true;
            else
                EnableLoginBtn = false;
        }


    }
}
