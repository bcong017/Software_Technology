using QuanLyGaraOto.ViewUserControl.SwitchViewClass;
using QuanLyGaraOto.Model;
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
    public class MainViewModel : BaseViewModel
    {
        public static TAIKHOAN User;
        private Visibility updateParamVisibility;
        public Visibility UpdateParamVisibility
        {
            get { return updateParamVisibility; }
            set 
            { 
                updateParamVisibility = value; 
                OnPropertyChanged();
            }
        }
        private BaseViewModel _selectedViewModel = new SwitchViewSalesReport();
        public BaseViewModel SelectedViewModel 
        { 
            get { return _selectedViewModel; } 
            set 
            { 
                _selectedViewModel = value; 
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        
        public ICommand SelectViewCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand OpenAccountInfoCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { 
                if (User.QuyenHan == 0)
                {
                    UpdateParamVisibility = Visibility.Collapsed;
                }    
                else
                {
                    UpdateParamVisibility = Visibility.Visible;
                }    
            });
            SelectViewCommand = new RelayCommand<ListViewItem>((p) => { return true; }, (p) => { SelectView(p); });

            OpenAccountInfoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AccountInformation accountInformation = new AccountInformation();
                accountInformation.ShowDialog();
            });

            LogoutCommand = new RelayCommand<Window>
            (
                (p) => { return true; }, 
                (p) => 
                {
                    MainViewModel.User = null;
                    LoginWindow loginWindow = new LoginWindow();
                    Application.Current.MainWindow = loginWindow;
                    Application.Current.MainWindow.Show();
                    p.Close();
                }
            );

            CreateAccountCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CreateAccountWindow createAccountWindow = new CreateAccountWindow();
                createAccountWindow.ShowDialog();

            });
        }

        void SelectView(ListViewItem listViewItem)
        {
            if (listViewItem != null && listViewItem.Tag != null)
            {
                string tag = listViewItem.Tag.ToString();
                
                switch(tag)
                {
                    case "CarCheckIn":
                        { 
                            SelectedViewModel = new SwitchViewCarCheckIn();
                            break;
                        }
                    case "RepairReceipt":
                        {
                            SelectedViewModel = new SwitchViewRepairReceipt();
                            break;
                        }
                    case "Bill":
                        {
                            SelectedViewModel = new SwitchViewBill();
                            break;
                        }
                    case "Search":
                        {
                            SelectedViewModel = new SwitchViewSearch();
                            break;
                        }
                    case "SalesReport":
                        {
                            SelectedViewModel = new SwitchViewSalesReport();
                            break;
                        }
                    case "AccessoriesReport":
                        {
                            SelectedViewModel = new SwitchViewAccessoriesReport();
                            break;
                        }
                    case "AddAcessories":
                        {
                            SelectedViewModel = new SwitchViewAddAccessories();
                            break;
                        }
                    case "Update":
                        {
                            SelectedViewModel = new SwitchViewUpdateParameter();
                            break;
                        }
                    case "Show":
                        {
                            SelectedViewModel = new SwitchViewShowWageAndMaterial();
                            break;
                        }
                    default:
                        break;
                }    
                
            }    
        }
        
        //void LoginAccount(Window p)
        //{
        //    if (p != null)
        //    {
        //        p.Hide();
        //        LoginWindow loginWindow = new LoginWindow();
        //        loginWindow.ShowDialog();

        //        var loginVM = loginWindow.DataContext as LoginWindowViewModel;

        //        if (loginVM == null)
        //            return;

        //        if (loginVM.isLoggedIn)
        //        {
        //            p.Show();
        //        }
        //    }
        //}
    }
}
