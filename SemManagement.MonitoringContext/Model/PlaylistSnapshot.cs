using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SemManagement.MonitoringContext.Model
{
    public class PlaylistSnapshot
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PlaylistId { get; set; }
        public virtual ICollection<PlaylistSnapshotSong> SnapshotSongs { get; set; }
    }
}
