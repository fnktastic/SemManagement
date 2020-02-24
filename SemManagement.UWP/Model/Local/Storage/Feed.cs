using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Model.Local.Storage
{
    public interface IFeedItem
    {
        DateTime DateTime { get; set; }
        string Message { get; set; }
        MonitorTypeEnum MonitorType { get; set; }
        int Id { get; }
    }

    public class FeedItem : IFeedItem
    {
        public DateTime DateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MonitorTypeEnum MonitorType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id => throw new NotImplementedException();
    }

    public class FeedList : List<IFeedItem>
    {

    }

    public enum MonitorTypeEnum
    {
        Stations,
        Playlists,
        Rules,
        PlayerState
    }
}
