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
        string Parent { get; set; }
        MonitorTypeEnum MonitorType { get; set; }
        SnapshotActionEnum SnapshotAction { get; set; }
        int Id { get; }
    }

    public class FeedList : List<IFeedItem>
    {

    }


    public class PalylistFeedItem : IFeedItem
    {
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public MonitorTypeEnum MonitorType { get; set; }
        public int Id { get => Plid; }
        public int Plid { get; set; }
        public string Parent { get; set; }
        public SnapshotActionEnum SnapshotAction { get; set; }
    }

    public class SongFeedItem : IFeedItem
    {
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public MonitorTypeEnum MonitorType { get; set; }
        public int Id { get => Sgid; }
        public int Sgid { get; set; }
        public string Parent { get; set; }
        public SnapshotActionEnum SnapshotAction { get; set; }
    }

    public class StationPlaylistFeedItem : IFeedItem
    {
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public MonitorTypeEnum MonitorType { get; set; }
        public int Id { get => Plid; }
        public int Plid { get; set; }
        public string Parent { get; set; }
        public SnapshotActionEnum SnapshotAction { get; set; }
    }
}
