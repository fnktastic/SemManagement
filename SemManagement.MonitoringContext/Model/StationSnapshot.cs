using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    public class StationMonitoring
    {
        [Key]
        public Guid Id { get; set; }
        public int StationId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int RepeatInterval { get; set; }
        public virtual ICollection<StationSnapshot> Snapshots { get; set; }
    }


    public class StationSnapshot
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int StationId { get; set; }
        public virtual ICollection<StationSnapshotPlaylist> SnapshotPlaylists { get; set; }
    }

    public class StationSnapshotPlaylist
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StationSnapshot { get; set; }
        public int PlaylistId { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class PlaylistSnapshot
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PlaylistId { get; set; }
        public virtual ICollection<PlaylistSnapshotSong> SnapshotSongs { get; set; }
    }

    public class PlaylistSnapshotSong
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PlaylistSnapshotId { get; set; }
        public int SongId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
