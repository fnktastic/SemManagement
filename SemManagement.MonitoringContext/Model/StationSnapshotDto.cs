using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    [Table("StationSnapshot")]
    public class StationSnapshotDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int StationId { get; set; }
        public Guid StationMonitoringId { get; set; }
        public StationMonitoringDto StationMonitoring { get; set; }
        public virtual Collection<StationSnapshotPlaylistDto> SnapshotPlaylists { get; set; }
    }
}
