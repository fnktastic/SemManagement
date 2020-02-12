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
        public Guid Id { get; set; }
        public int PlaylistId { get; set; }
        public int PlaylistName { get; set; }
        public int? TargetStationId { get; set; }
        public virtual Collection<PlaylistSnapshotDto> Snapshots { get; set; }
    }
}
