﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemManagement.MonitoringContext.Model
{
    [Table("StationMonitoring")]
    public class StationMonitoringDto
    {
        [Key]
        public Guid Id { get; set; }
        public int WantedAmountOfUpdates { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int RepeatInterval { get; set; }
        public virtual Collection<StationSnapshotDto> Snapshots { get; set; }
    }
}
