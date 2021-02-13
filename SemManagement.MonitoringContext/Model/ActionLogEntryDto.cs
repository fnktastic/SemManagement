using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.Model
{
    public class ActionLogEntryDto
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public string Action { get; set; }
        public string Parameters { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
