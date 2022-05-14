using QuanLyGaraOto.AddingClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public ICommand SendMailCommand { get; set; }
        
        public ForgetPasswordWindowViewModel()
        {
            EnableSendMailBtn = false;
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                //if (p == null)
                //    return;
                p.Close();
            });

            SendMailCommand = new RelayCommand<object>((p) => { return true; }, ((p) => { SendMail(); }));
        }

        void EnableSendMailBtnChecker()
        {
            if (string.IsNullOrEmpty(UserName) == false && string.IsNullOrEmpty(Gmail) == false)
                EnableSendMailBtn = true;
            else
                EnableSendMailBtn = false;
        }

        async void SendMail()
        {
            string password;
            // vô sql kiểm tra xem có tên tài khoản nào giống thế không
            // nếu không có thì dùng hàm "NotificationWindow.Notify" để thông báo ra nhá
            // ở đây chỉ là giả sử có tài khoản như thế (mốt có sql thì chỉnh cái này lại)
            if (CheckValidUsername(out password) && ValidateEmail.EmailIsValid(Gmail))
            {
                string fromEmail = ConfigurationManager.AppSettings.Get("EmailAddress");
                string fromPassword = ConfigurationManager.AppSettings.Get("GeneratedPassword");


                var fromAddress = new MailAddress(fromEmail, "Quản lý Gara Oto");
                var toAddress = new MailAddress(Gmail);
                
                string subject = "Email tự động";
                string body = "Password của bạn là: " + password;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(message);
                }
                NotificationWindow.Notify("Gửi mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Gửi mật khẩu thất bại!");
            }    

        }

        bool CheckValidUsername(out string password)
        {
            // hàm check tài khoản nè và lấy cái password đó ra luôn
            password = "";
            return true;
        }

    }
}
