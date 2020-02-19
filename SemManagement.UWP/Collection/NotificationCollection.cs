using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Collection
{
    public class NotificationCollection : ObservableCollection<Notification>
    {
        public NotificationCollection(IEnumerable<Notification> notifications) : base(notifications)
        {

        }
    }
}
