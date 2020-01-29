using SemManagement.MonitoringContext.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    public class MonitoringDto
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }

        public MonitorStateEnum MonitorState { get; set; }

        public MonitorTypeEnum MonitorType { get; set; }
    }
}
