using System;
using System.ComponentModel.DataAnnotations;

namespace SemManagement.MonitoringContext.Model
{
    public class StationSnapshotPlaylist
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StationSnapshotId { get; set; }
        public StationSnapshot StationSnapshot { get; set; }
        public int PlaylistId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
