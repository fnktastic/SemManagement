using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
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
