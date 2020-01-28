using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Model
{
    [Table("SnapshotPlaylist")]
    public class SnapshotPlaylistDto
    {
        [Key]
        public Guid SnapshotPlaylistId { get; set; }

        public Guid SnapshotId { get; set; }
        public SnapshotDto Snapshot { get; set; }

        public int PlaylistId { get; set; }
        public PlaylistDto Playlist { get; set; }
    }
}
