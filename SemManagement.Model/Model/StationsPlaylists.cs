using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SemManagement.SemContext.Model
{
    public class StationsPlaylists
    {
        public int Sid { get; set; }
        public int Plid { get; set; }
        public int Syncronized { get; set; }
        [Key]
        public int Spid { get; set; }
        public string Creation_Date { get; set; }
        public string Synchronized_Date { get; set; }
        public DateTime Last_Update_Date { get; set; }
    }
}
