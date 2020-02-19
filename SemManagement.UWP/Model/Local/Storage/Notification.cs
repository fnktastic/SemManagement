using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class Notification : ViewModelBase
    {
        public NotificationType NotificationType { get; set; }
    }

    public enum NotificationType
    {

    }
}
