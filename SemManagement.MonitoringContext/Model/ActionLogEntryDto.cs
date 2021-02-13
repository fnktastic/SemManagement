using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    public class ActionLogEntryDto
    {
        public string StationName { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
