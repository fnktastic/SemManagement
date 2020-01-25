using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
}
