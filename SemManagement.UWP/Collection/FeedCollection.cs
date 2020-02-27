using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Collection
{
    public class FeedCollection : ObservableCollection<IGrouping<DateTime, FeedItem>>
    {
        public FeedCollection(IList<IGrouping<DateTime, FeedItem>> feedItems) : base(feedItems)
        {

        }
    }
}
