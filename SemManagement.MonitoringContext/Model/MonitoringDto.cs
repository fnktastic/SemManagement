using SemManagement.MonitoringContext.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    [Table("Monitoring")]
    public class MonitoringDto
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }

        public MonitorStateEnum MonitorState { get; set; }

        public MonitorTypeEnum MonitorType { get; set; }

        public MonitoringDto()
        {

        }

        public MonitoringDto(MonitorTypeEnum monitorType, MonitorStateEnum monitorState, DateTime time)
        {
            Id = Guid.NewGuid();
            MonitorType = monitorType;
            MonitorState = monitorState;
            Timestamp = time;
        }
    }
}
