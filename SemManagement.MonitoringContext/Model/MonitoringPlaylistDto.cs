using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("PlaylistMonitoring")]
    public class PlaylistMonitoringDto
    {
        [Key]
        public string Id { get; set; }
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public int? TargetStationId { get; set; }
        public virtual Collection<PlaylistSnapshotDto> Snapshots { get; set; }
    }
}
