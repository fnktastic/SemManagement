using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("PlaylistSnapshot")]
    public class PlaylistSnapshotDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string PlaylistName { get; set; }
        public int PlaylistId { get; set; }
        public string PlaylistMonitoringId { get; set; }
        public PlaylistMonitoringDto PlaylistMonitoring { get; set; }
        public virtual Collection<PlaylistSnapshotSongDto> SnapshotSongs { get; set; }
    }
}
