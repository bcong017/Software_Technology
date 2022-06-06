using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.ViewModel
{
    public class UpdateParameterViewModel : BaseViewModel
    {
        private decimal percentage;
        public decimal Percentage
        {
            get { return percentage; }
            set { percentage = value; OnPropertyChanged(nameof(Percentage)); }
        }

        private int carNumber;
        public int CarNumber
        {
            get { return carNumber; }
            set { carNumber = value; OnPropertyChanged(nameof(CarNumber)); }
        }

        public UpdateParameterViewModel()
        {

        }
    }
}
