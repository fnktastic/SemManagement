using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class Playlist : ViewModelBase
    {
        public int Plid { get; set; }
        public string Name { get; set; }
        public int Changed { get; set; }
        public DateTime last_Update_Date { get; set; }

        private int _no;
        public int No
        {
            get { return _no; }
            set
            {
                if (_no == value) return;
                _no = value;
                RaisePropertyChanged(nameof(No));
            }
        }
    }
}
