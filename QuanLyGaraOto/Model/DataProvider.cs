using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGaraOto.Model
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Instance 
        { 
            get 
            {
                if (_instance == null)
                    _instance = new DataProvider();
                return _instance;
            } 
            set
            {
                _instance = value;
            }
        }

        // biến database ở đây

        #region Constructor
        private DataProvider()
        {
            // khởi tạo biến db ở đây
        }
        #endregion

    }
}
