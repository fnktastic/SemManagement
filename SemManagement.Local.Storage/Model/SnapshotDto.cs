using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Local.Storage.Model
{
    public class SnapshotDto
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public int StationId { get; set; }
        public StationDto Station { get; set; }

        public virtual ICollection<SnapshotPlaylistDto> SnapshotPlaylists { get; set; }
    }
}
