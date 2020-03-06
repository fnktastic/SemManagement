using SemManagement.UWP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model
{
    public class Station : Notifier
    {
        public int Sid { get; set; }

        public int Uid { get; set; }

        public string Name { get; set; }

        public string Hardware_ID { get; set; }

        public DateTime Licence { get; set; }

        public int Type { get; set; }

        public int Blocked { get; set; }

        public int Soft_Installed { get; set; }

        public int Synchronized { get; set; }

        public int Autosync { get; set; }

        public DateTime last_Update_Date { get; set; }


        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged(nameof(IsSelected)); }
        }

        private bool _isAssigned;
        public bool IsAssigned
        {
            get { return _isAssigned; }
            set { _isAssigned = value; NotifyPropertyChanged(nameof(IsAssigned)); }
        }
    }
}
