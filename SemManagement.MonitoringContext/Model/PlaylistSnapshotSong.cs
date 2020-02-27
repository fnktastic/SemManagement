using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("PlaylistSnapshotSong")]
    public class PlaylistSnapshotSongDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PlaylistSnapshotId { get; set; }
        public PlaylistSnapshotDto PlaylistSnapshot { get; set; }
        public int SongId { get; set; }
        public DateTime DateTime { get; set; }
        public string SongName { get; set; }
    }
}
