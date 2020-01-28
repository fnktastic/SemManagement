using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Model
{
    [Table("Snapshot")]
    public class SnapshotDto
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public int StationId { get; set; }
        public StationDto Station { get; set; }

        public virtual Collection<SnapshotPlaylistDto> SnapshotPlaylists { get; set; }
    }
}
