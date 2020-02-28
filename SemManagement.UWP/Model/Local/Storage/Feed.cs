using SemManagement.UWP.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public class FeedItem 
    {
        public DateTime DateTime { get; private set; }
        public string Message { get; private set; }
        public MonitorTypeEnum MonitorType { get; private set; }
        public int Id {get; private set;}
        public string Parent { get; private set; }
        public SnapshotActionEnum SnapshotAction { get; private set; }
    }

    public class FeedList : List<FeedItem>
    {

    }
}
