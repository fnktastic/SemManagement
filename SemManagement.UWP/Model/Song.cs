using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class Song : ViewModelBase
    {
        public int Sgid { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Filename { get; set; }
        public string file_MD5 { get; set; }
        public string file_MD5_Gained { get; set; }
        public int Uploaded { get; set; }

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
