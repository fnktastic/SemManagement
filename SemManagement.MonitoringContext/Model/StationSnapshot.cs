using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    public class StationSnapshot
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int StationId { get; set; }
        public Guid StationMonitoringId { get; set; }
        public StationMonitoring StationMonitoring { get; set; }
        public virtual ICollection<StationSnapshotPlaylist> SnapshotPlaylists { get; set; }
    }
}
