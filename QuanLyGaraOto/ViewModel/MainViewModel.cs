﻿using QuanLyGaraOto.ViewUserControl.SwitchViewClass;
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

        public MainViewModel()
        {
            SelectViewCommand = new RelayCommand<ListViewItem>((p) => { return true; }, (p) => { SelectView(p); });
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
                            break;
                        }
                    case "Bill":
                        {
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

                    default:
                        break;
                }    
                
            }    
        }
    }
}
