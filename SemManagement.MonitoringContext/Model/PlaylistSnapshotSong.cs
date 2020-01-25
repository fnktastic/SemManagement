using System;
using System.ComponentModel.DataAnnotations;

namespace SemManagement.MonitoringContext.Model
{
    public class PlaylistSnapshotSong
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PlaylistSnapshotId { get; set; }
        public int SongId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
