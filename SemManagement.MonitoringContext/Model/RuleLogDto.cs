using SemManagement.MonitoringContext.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    public class RuleLogDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RuleId { get; set; }
        public RuleDto Rule { get; set; }

        public DateTime Timestamp { get; set; }
        public virtual Collection<RuleLogStationDto> FiredRuleLogStation { get; set; }

        [NotMapped]
        public virtual Collection<StationDto> FiredStations => FiredRuleLogStation.Select(x => x.Station).ToCollection();
    }

    public class RuleLogStationDto
    {
        [Key]
        public Guid RuleLogStationId { get; set; }

        public Guid RuleLogId { get; set; }
        public RuleLogDto RuleLog { get; set; }

        public int StationSid { get; set; }
        public StationDto Station { get; set; }
    }
}
