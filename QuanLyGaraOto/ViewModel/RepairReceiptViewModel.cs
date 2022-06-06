using QuanLyGaraOto.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using QuanLyGaraOto;

namespace QuanLyGaraOto.ViewModel
{
    public class RepairReceiptViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public RepairReceiptViewModel()
        {
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddNewVATPHAM k = new AddNewVATPHAM(this);
                Application.Current.MainWindow = k;
                Application.Current.MainWindow.Show();
            });
        }

        public void Receive()
        {
            NotifyWindow notifyWindow = new NotifyWindow("Received");
            notifyWindow.Show();
        }
    }
}
