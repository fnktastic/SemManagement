using SemManagement.MonitoringContext.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.BusinessLogic
{
    public interface IFeedItem
    {
        DateTime DateTime { get; set; }
        string Message { get; set; }
        MonitorTypeEnum MonitorType { get; set; }
        int Id { get; set; }
    }

    public class FeedList : List<IFeedItem>
    {

    }


    public class PalylistFeedItem : IFeedItem
    {
        public DateTime DateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MonitorTypeEnum MonitorType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class SongFeedItem : IFeedItem
    {
        public DateTime DateTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MonitorTypeEnum MonitorType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
