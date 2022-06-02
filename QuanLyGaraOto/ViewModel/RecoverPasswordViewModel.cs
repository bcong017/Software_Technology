using QuanLyGaraOto.AddingClasses;
using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGaraOto.ViewModel
{
    public class RecoverPasswordViewModel : BaseViewModel
    {
        private int verifiedNumber;
        private string password;
        private string configuration;

        public int VerifiedNumber
        { 
            get { return verifiedNumber; } 
            set { verifiedNumber = value; } 
        }

        public string Password 
        { 
            get { return password; } 
            set { password = value; }
        }

        public string Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public ICommand PasswordChanged { get; set; }
        public ICommand ConfigurationChanged { get; set; }
        public ICommand ResendEmailCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public RecoverPasswordViewModel()
        {
            PasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            ConfigurationChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Configuration = p.Password; });

            ResendEmailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string fromEmail = ConfigurationManager.AppSettings.Get("EmailAddress");
                string fromPassword = ConfigurationManager.AppSettings.Get("GeneratedPassword");

                EmailSender.Instance.SendEmail(fromEmail, fromPassword);
                NotificationWindow.Notify("Đã gửi lại mã xác nhận!");

            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => 
            { 
                LoginWindow loginWindow = new LoginWindow();
                Application.Current.MainWindow = loginWindow;
                Application.Current.MainWindow.Show();
                p.Close(); 
            });
            RecoverPasswordCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(Configuration) && VerifiedNumber == 0)
                    return false;
                if (Password != Configuration)
                    return false;
                return true;
            }, (p) =>
            {
                if (VerifiedNumber == EmailSender.Instance.VerifiedNumber)
                {
                    var user = DataProvider.Instance.DB.TAIKHOANs.Where(x => x.TenTaiKhoan == EmailSender.Instance.UserName).FirstOrDefault();
                    if (user != null)
                    {
                        user.MatKhau = Encryptor.CreateMD5(Encryptor.CreateMD5(Encryptor.Base64Encode(Password)));
                        DataProvider.Instance.DB.SaveChanges();
                        NotificationWindow.Notify("Cập nhật mật khẩu thành công!");
                        p.Close();
                    }
                }
                else
                    NotificationWindow.Notify("Mã xác nhận không đúng!");
            });
            BackCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ForgetPasswordWindow forgetPasswordWindow = new ForgetPasswordWindow();
                Application.Current.MainWindow = forgetPasswordWindow;
                Application.Current.MainWindow.Show();
                p.Close();
            });
        }
    }
}
