using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class ScheduledStation
    {
        public int Scid { get; set; }
        public int? Scpid { get; set; }
        public int? Sid { get; set; }
        public int? Uid { get; set; }
        public int Plid { get; set; }
        [Key]
        public int Scevid { get; set; }
        public int Spid { get; set; }
        public string Name { get; set; }
        public string ScheduleName { get; set; }
        public int Synced { get; set; }
        public int Exported { get; set; }
        public string Changed_Date { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public string Weekday { get; set; }
        public int Playmode { get; set; }
        public int StopPlayback { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
