using QuanLyGaraOto.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyGaraOto
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool LoginWasClicked = false;

        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginWindowViewModel();
            this.Closing += LoginWindow_Closing;
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWasClicked = true;
        }

        private void LoginWindow_Closing(object sender, CancelEventArgs e)
        {
            if (LoginWasClicked == true)
                LoginWasClicked = false;
            else
                Application.Current.Shutdown();

        }
    }
}
