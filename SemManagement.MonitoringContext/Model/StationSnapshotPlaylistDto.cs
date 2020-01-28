using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("StationSnapshotPlaylist")]
    public class StationSnapshotPlaylistDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StationSnapshotId { get; set; }
        public StationSnapshotDto StationSnapshot { get; set; }
        public int PlaylistId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
